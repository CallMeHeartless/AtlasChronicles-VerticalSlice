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
    private bool m_bIsActive = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!m_sInstance) {
            m_sInstance = this;
        }
        m_rGuidePanel = GameObject.Find("Guide");
        if (m_rGuidePanel) {
            m_rText = m_rGuidePanel.GetComponentInChildren<TextMeshProUGUI>();
        } else {
            Debug.LogError("ERROR: Could not find guide panel (GameEndController reference)");
        }

    }

    // Called when the player collects a map fragment to check if they have them all (or when they have lost one)
    public static void CheckMapCollection() {
        if (!instance) {
            Debug.LogError("ERROR: GameEndController instance does not exist. Check that it has been added to the scene.");
        }
        // If MAPS_COLLECTED >= MAPS_TOTAL
        Debug.Log("Collected: " + GameStats.s_iMapsBoard[GameStats.s_iLevelIndex] + "  portal is: " + instance.m_bIsActive);
        if(GameStats.s_iMapsBoard[GameStats.s_iLevelIndex] >= GameStats.s_iMapsTotal[GameStats.s_iLevelIndex]) {
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
        }
        // Display text
        m_rGuidePanel.SetActive(_bState);
        if (_bState) {
            m_rText.SetText(m_strEndText);
        } else {
            m_rText.SetText("");
        }
    }

    // Return the player to the main menu upon completion
    public void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player") && m_bIsActive) {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
