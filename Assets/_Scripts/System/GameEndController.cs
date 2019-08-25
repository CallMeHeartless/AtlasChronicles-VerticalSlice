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

    // Properties / references
    private GameObject m_rGuidePanel;
    private TextMeshProUGUI m_rText;

    public string m_strEndText = "Congrats! You obtained all seven map fragments. Return with them to the start to leave the world.";
    [SerializeField]
    private GameObject m_rPortalParticles = null;
    [SerializeField]
    private GameObject m_rInfo = null; //Just ui telling player the portal is open
    [SerializeField]
    private TutorialCollider m_rNPCCompleteMsg = null; 
    private bool m_bIsActive = false;

    static float m_fMinimumCrystals = 100;
    static float m_fMinimumMaps = 5;



    void Awake()
    {
        if (!m_sInstance) {
            m_sInstance = this;
        }
        m_rGuidePanel = GameObject.FindWithTag("Guide");
        if (m_rGuidePanel) {
            m_rText = m_rGuidePanel.GetComponentInChildren<TextMeshProUGUI>();
        }
        else {
            Debug.LogError("ERROR: Could not find guide panel (GameEndController reference)");
        }
    }

    // Called when the player collects a map fragment to check if they have them all (or when they have lost one)
    public static void CheckMapCollection() {
        if (!instance) {
            //Debug.LogError("ERROR: GameEndController instance does not exist. Check that it has been added to the scene.");
            return;
        }
        // If MAPS_COLLECTED >= MAPS_TOTAL
        if(GameStats.s_iMapsBoard[GameStats.s_iLevelIndex] >= m_fMinimumMaps
            && GameStats.s_iCollectableBoard[GameStats.s_iLevelIndex] >= m_fMinimumCrystals) {
            instance.TogglePortal(true);
        } else {
            instance.TogglePortal(false);
        }
    }

    // Activates / deactivates the portal
    private void TogglePortal(bool _bState) {
        if (_bState == m_bIsActive) return;
        m_bIsActive = _bState;
        if (m_rPortalParticles) {
            m_rPortalParticles.SetActive(_bState);
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
            Zone.ClearZones();
            SceneManager.LoadScene(0);
        }
    }
}
