using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameEndController : MonoBehaviour
{
    // Static instance
    private static GameEndController m_sInstance = null;
    public static GameEndController instance { get { return m_sInstance; } }

    [Header("Collection variables")]
    [SerializeField] private int m_iCrystalsNeeded = 200;
    [SerializeField] private int m_iMapsNeeded = 5;

    // Properties / references
    [Header("References")]
    public string m_strEndText = "Congrats! You obtained all 6 map fragments. Return with them to the start to leave the world.";
    private TextMeshProUGUI m_rText;
    [SerializeField] private GameObject m_rPortalParticles = null;
    [SerializeField] private GameObject m_rInfo = null; //Just ui telling player the portal is open
    [SerializeField] private bool m_bIsActive = false;
    [SerializeField] private GameObject m_rPlayer = null;
    private LoadingScreen m_rLoadingScreen;

    private bool m_bGameComplete = false;
    private Animator m_rAnimator;
    private TimerUpdate m_rTimerUpdate;

    public int[] m_iMinimumCrystalsModes;
    public int[] m_iMinimumMapsModes;
    
    public static int m_iMinimumCrystals = 100;
    public static int m_iMinimumMaps = 5;
    public static bool m_bHasEnoughMaps = false;
    public bool m_bIsTriggeredOnce = false;
    private bool m_bTimeResultsActivated = false;


    void Awake()
    {
        // Find instance
        if (!m_sInstance) {
            m_sInstance = this;
            // Initialise static variables
        }

        // Set crystal and map requirements based on the selected speed run mode
        if (GameState.GetGameplayMode() == GameState.GameplayMode.Everything)
        {
            m_iCrystalsNeeded = GameObject.FindGameObjectsWithTag("SecondaryPickup").Length + (GameObject.FindGameObjectsWithTag("Box").Length*5);
        }
        else
        {
            m_iCrystalsNeeded = m_iMinimumCrystalsModes[(int)GameState.GetGameplayMode()];
        }
       
        m_iMapsNeeded = m_iMinimumMapsModes[(int)GameState.GetGameplayMode()];

        // Turn off the 'portal is open' UI
        if (m_rInfo)
        {
            m_rInfo.SetActive(false);
        }

        // Obtain Animation component
        m_rAnimator = transform.GetChild(7).GetComponent<Animator>();

        //Obtain timer update component
        m_rTimerUpdate = GameObject.FindGameObjectWithTag("TextUI").GetComponent<TimerUpdate>();
        m_rLoadingScreen = FindObjectOfType<LoadingScreen>();

        // Initialise crystal depo children
        InitialiseCrystalDepos();

        m_bGameComplete = false;

        if (GameState.GetGameplayMode() == GameState.GameplayMode.Rush)
        {
            SetPortalState(true);
        }

        GameState.SetFirstTimeGameAccessed(true);
    }

    private void Update()
    {
        //If in Results page
        if (m_bTimeResultsActivated)
        {
            if (Input.GetAxis("XBoxXButton") != 0 || Input.GetAxis("Jump") != 0)
            {
                ExitLevel();
            }
        }

        //Viv Hacks
        //if (Input.GetKeyDown(KeyCode.Alpha9))
        //{
        //    m_bIsActive = true;
        //    m_iCrystalsNeeded = 0;
        //    m_iMapsNeeded = 0;
        //    m_rPlayer.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5.0f);
        //}

        if (!m_bGameComplete)
            return;

        if (GameState.GetGameplayMode() == GameState.GameplayMode.ForTheMaps)
        {
           if( GameStats.s_iMapsBoard[GameStats.s_iLevelIndex] == m_iMinimumMaps)
            {
                SetPortalState(true);
            }
        }
    }

    // Called when the player collects a map fragment to check if they have them all (or when they have lost one)
    public static void CheckMapCollection() {
        if (!instance) {
            Debug.LogError("ERROR: GameEndController instance does not exist. Check that it has been added to the scene.");
            return;
        }
        // If MAPS_COLLECTED >= MAPS_TOTAL
        if(GameStats.s_iMapsBoard[GameStats.s_iLevelIndex] >= m_iMinimumMaps) { //&& GameStats.s_iCollectableBoard[GameStats.s_iLevelIndex] >= s_iMinimumCrystals
            //instance.TogglePortal(true);
            m_bHasEnoughMaps = true;
        } else {
            // instance.TogglePortal(false);
            m_bHasEnoughMaps = false;
        }
    }

    // Activates / deactivates the portal
    public void SetPortalState(bool _bState) {
        // Avoid repeated activation
        if (_bState == m_bIsActive) return;

        // Change portal state
        m_bIsActive = _bState;

        // Trigger the appropriate animation (Kerry)
        if (m_bIsActive) {
            m_rAnimator.SetTrigger("Rebuild"); // Assemble the ruins
        } else {
            m_rAnimator.SetTrigger("Reset(TestTrigger)"); // Collapse the ruins
        }

        // Handle particles 
        if (m_rPortalParticles) {
            m_rPortalParticles.SetActive(_bState);

            // Handle info message
            if(m_rInfo){
                m_rInfo.SetActive(true);
            }
        }
    }

    // Return the player to the main menu upon completion
    public void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player") && m_bIsActive && !m_bGameComplete)
        {
            m_bIsTriggeredOnce = true;
            
            GameState.SetTimerFlag(true);
            m_bGameComplete = true;
            if (GameState.GetGameplayMode() != GameState.GameplayMode.Adventure)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                m_rTimerUpdate.StopTimer();
                GameObject Object = GameObject.FindGameObjectWithTag("TimeRecords");
                if (Object.GetComponent<DontDestory>())
                {
                    Object.GetComponent<DontDestory>().SetNewSpeedMode((int)GameState.GetGameplayMode(),
                        m_rTimerUpdate.GetFinalTime(),
                        Records.m_CurrentPlace);

                    PlayerPrefs.SetInt("TimeAttackCurrentPlace", Records.m_CurrentPlace);
                    m_rTimerUpdate.DisplayTimeAttackResults();
                    m_bTimeResultsActivated = true;
                }
            }
            else
            {
                ExitLevel();
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_bIsTriggeredOnce = false;
        }
    }

    /// <summary>
    /// Return to main menu after 5 seconds
    /// </summary>
    /// <returns></returns>
    void ExitLevel()
    {
        print("exiting level");
        //Reset the level before loading main menu
        Zone.ClearZones();

        if(GameState.GetMainMenuAccessed())
        {
            //If game was run by starting through main menu, allow loading screens
            AsyncOperation s_asyncLoad = SceneManager.LoadSceneAsync(0);
            m_rLoadingScreen.ActivateLoadingScreen(s_asyncLoad);
        }
        else
        {
            //If game was not started through main menu, load scenes without loading screens
            SceneManager.LoadScene(0);
        }
    }


    /// <summary>
    /// Used upon initialisation to set how many crystals each depo needs.
    /// </summary>
    private void InitialiseCrystalDepos() { // Kerry
        // Get an array of all child components
        CrystalDepoController[] depos = GetComponentsInChildren<CrystalDepoController>();
        if(depos.Length == 0 || depos == null) {
            Debug.LogError("ERROR: No Crystal Depos found. Check that they have been added to the scene as children of this object: " + name);
        }

        // Calculate how much each depo needs
        int iNeededCrystals = m_iCrystalsNeeded / depos.Length;
        // Consider adding rounding here

        // Set each depo to require the correct crystals
        foreach(CrystalDepoController depo in depos) {
            depo.iNeededCrystals = iNeededCrystals;
        }
    }
}
