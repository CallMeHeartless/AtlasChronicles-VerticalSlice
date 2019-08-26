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
    public string m_strEndText = "Congrats! You obtained all seven map fragments. Return with them to the start to leave the world.";
    private GameObject m_rGuidePanel;
    private TextMeshProUGUI m_rText;
    [SerializeField]
    private GameObject m_rPortalParticles = null;
    [SerializeField]
    private GameObject m_rInfo = null; //Just ui telling player the portal is open
    [SerializeField]
    private TutorialCollider m_rNPCCompleteMsg = null; 
    private bool m_bIsActive = false;

    public static int s_iMinimumCrystals = 100;
    public static int s_iMinimumMaps = 5;
    public static bool s_bHasEnoughMaps = false;
       
    void Awake()
    {
        // Find instance
        if (!m_sInstance) {
            m_sInstance = this;
            // Initialise static variables
            s_iMinimumCrystals = m_iCrystalsNeeded;
            s_iMinimumMaps = m_iMapsNeeded;
        }
        m_rGuidePanel = GameObject.FindWithTag("Guide");
        if (m_rGuidePanel) {
            m_rText = m_rGuidePanel.GetComponentInChildren<TextMeshProUGUI>();
        }
        else {
            Debug.LogError("ERROR: Could not find guide panel (GameEndController reference). Check that it has been added to the scene.");
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
        if(GameStats.s_iMapsBoard[GameStats.s_iLevelIndex] >= s_iMinimumMaps) { //&& GameStats.s_iCollectableBoard[GameStats.s_iLevelIndex] >= s_iMinimumCrystals
            //instance.TogglePortal(true);
            s_bHasEnoughMaps = true;
        } else {
            // instance.TogglePortal(false);
            s_bHasEnoughMaps = false;
        }
    }

    // Activates / deactivates the portal
    private void SetPortalState(bool _bState) {
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
            if(m_rNPCCompleteMsg)
            {
                m_rNPCCompleteMsg.SetText("Congrats Kid! Enter the portal to leave the world or stay behind and adventure some more. (Wait.. those Pechapples are for me.. right?)");
                Invoke("HideMessage", 5.0f);
            }
        }

        // Display text
        m_rGuidePanel.SetActive(_bState);
        if (_bState) {
            m_rText.SetText(m_strEndText);
        } else {
            m_rText.SetText("");
        }
    }

    public void HideMessage()
    {
        m_rNPCCompleteMsg.transform.parent.gameObject.SetActive(false);
    }

    // Return the player to the main menu upon completion
    public void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player") && m_bIsActive) {
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

        Debug.Log("Depos ready");
    }
}
