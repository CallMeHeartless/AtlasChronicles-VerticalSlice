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
    [SerializeField]
    private int m_iCrystalsNeeded = 200;
    [SerializeField]
    private int m_iMapsNeeded = 5;

    // Properties / references
    [Header("References")]
    public string m_strEndText = "Congrats! You obtained all 6 map fragments. Return with them to the start to leave the world.";
    private TextMeshProUGUI m_rText;
    [SerializeField]
    private GameObject m_rPortalParticles = null;
    [SerializeField]
    private GameObject m_rInfo = null; //Just ui telling player the portal is open
    [SerializeField]
    private bool m_bIsActive = false;

    public int[] m_iMinimumCrystalsModes;
    public int[] m_iMinimumMapsModes;
    

    public static int m_iMinimumCrystals = 100;
    public static int m_iMinimumMaps = 5;
    public static bool m_bHasEnoughMaps = false;
       
    void Awake()
    {
        // Find instance
        if (!m_sInstance) {
            m_sInstance = this;
            // Initialise static variables
           
        }

        //set crysal and maps to so that they are for the right speed run mode
        if (GameState.GetIsSpeedRunning() == GameState.SpeedRunMode.Everything)
        {
            m_iCrystalsNeeded = GameObject.FindGameObjectsWithTag("SecondaryPickup").Length + (GameObject.FindGameObjectsWithTag("Box").Length*5);
            Debug.Log("number is: " + m_iCrystalsNeeded);
        }
        else
        {
            m_iCrystalsNeeded = m_iMinimumCrystalsModes[(int)GameState.GetIsSpeedRunning()];
        }
       
        m_iMapsNeeded = m_iMinimumMapsModes[(int)GameState.GetIsSpeedRunning()];
        //Debug.Log((int)GameState.GetSpeedRunning());

        if (m_rInfo)
        {
            m_rInfo.SetActive(false);
        }

        // Initialise crystal depo children
        InitialiseCrystalDepos();
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

        // Handle particles 
        if (m_rPortalParticles) {
            m_rPortalParticles.SetActive(_bState);

            // Handle info message
            if(m_rInfo)
            {
                m_rInfo.SetActive(true);
            }
        }
    }

    // Return the player to the main menu upon completion
    public void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player") && m_bIsActive) {

            if (GameState.GetIsSpeedRunning() != GameState.SpeedRunMode.Adventure)
            {
              
                GameObject.FindGameObjectWithTag("TextUI").GetComponent<TimerUpdate>().StopTimer();


                GameObject Object = GameObject.FindGameObjectWithTag("TimeRecords");
                if (Object.GetComponent<DontDestory>())
                {

                    Debug.Log("we got here"+ (int)GameState.GetIsSpeedRunning());
                    Object.GetComponent<DontDestory>().SetNewSpeedMode((int)GameState.GetIsSpeedRunning(),
                        GameObject.FindGameObjectWithTag("TextUI").GetComponent<TimerUpdate>().GetFinalTime(),
                        Records.m_CurrentPlace);
                    Debug.Log("we got pushed");
                }
            }
            GameState.SetTimerFlag(true);
            StartCoroutine("ExitLevel");

        }
    }
    IEnumerator ExitLevel()
    {
        yield return new WaitForSeconds(5f);

        Zone.ClearZones();
        SceneManager.LoadScene(0);
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
