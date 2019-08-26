using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalDepoController : MonoBehaviour
{
    // Internal variables
    [SerializeField]
    private LeylineController m_rLeyLine;
    private Material m_FillMaterial; // Material used to illustrate how the portal is filling
    private DisplayStat m_rDisplayStat; // UI Reference
    // Variable set properties
    private int m_iCurrentCrystals = 0;
    private int m_iNeededCrystals;
    public int iNeededCrystals { set { m_iNeededCrystals = value;} } // Used for initialisation
    private string m_strFillButton = "BButton";
    // Internal timer
    private float m_fFillTimer = 0.0f;
    [SerializeField]
    private float m_fMinimumFillTime = 0.05f;
    [SerializeField]
    private float m_fDefaultFillTime = 0.7f;
    private float m_fCurrentFillTime;

    // Internal flags
    private bool m_bIsFilling = false;
    private bool m_bIsPlayerInRange = false;
    private bool m_bIsFinished = false;

    // Start is called before the first frame update
    void Start(){
        // Set current fill time
        m_fCurrentFillTime = m_fDefaultFillTime;

        // Find reference to display UI
        GameObject UI = GameObject.Find("GameUI");
        if (UI) {
            m_rDisplayStat = UI.GetComponent<DisplayStat>();
        } else {
            Debug.LogError("ERROR: GameUI not found. Display stat reference not set.");
        }

        // DEBUG
        m_FillMaterial = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update(){
        // Only make changes to the Depo if the player is close enough
        if (m_bIsPlayerInRange && !m_bIsFinished) {

            // Check if the player is filling up the depo
            if (Input.GetButton(m_strFillButton)) {
                // Update crystal count
                UpdateCrystalCount();

                // Update visuals
                //UpdateStatus();
            }
        }
    }

    // Handle the player becoming in range of the depo
    private void OnTriggerEnter(Collider other) {
        // Ensure that the player is in range and that we are still able to affect the depo
        if (other.CompareTag("Player") && !m_bIsFinished) {
            SetPlayerWithinRange(true);
        }
    }

    // Handle the player leaving the range of the depo
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            SetPlayerWithinRange(false);
        }
    }

    /// <summary>
    /// Instructs the depo that the player is within / not within range, changing variables accordingly
    /// </summary>
    /// <param name="_bState">true if the player is now within range, false otherwise</param>
    private void SetPlayerWithinRange(bool _bState) {
        // Prevent repetition
        if(m_bIsPlayerInRange = _bState) {
            return;
        }

        // Update flag
        m_bIsPlayerInRange = _bState;
    }

    /// <summary>
    /// Update the material of the depo, and turn the leyline on if needed
    /// </summary>
    private void UpdateStatus() {
        // Don't update beyond what is possible
        //if(m_iCurrentCrystals >= m_iNeededCrystals) {
        //    return;
        //}

        // Update material
        //m_FillMaterial.color = 1
        m_FillMaterial.SetFloat("_Metallic", (float)m_iCurrentCrystals / (float)m_iNeededCrystals);

        // Activate leyline if filled
        if (m_iCurrentCrystals >= m_iNeededCrystals && m_rLeyLine) {
            m_rLeyLine.ActivateLeyline(true);
            m_bIsFinished = true;
        }
    }

    /// <summary>
    /// Removes crystals from the player's collection and adds them to the depo (if possible)
    /// </summary>
    private void UpdateCrystalCount() {
        // Stop if the player does not have any crystals
        if(GameStats.s_iSecondaryCollected == 0) {
            return;
        }

        // Increment fill timer
        m_fFillTimer += Time.deltaTime;

        // Check if it is time to fill the depo
        if(m_fFillTimer >= m_fCurrentFillTime) {
            // Reset the timer
            m_fFillTimer = 0.0f;

            // Reduce overall time it takes
            float fInterpolation = ((float)m_iCurrentCrystals / (float)m_iNeededCrystals);
            m_fCurrentFillTime = Mathf.Lerp(m_fDefaultFillTime, m_fMinimumFillTime, fInterpolation);

            // Take a crystal from the player and add it to the depo
            --GameStats.s_iCollectableBoard[GameStats.s_iLevelIndex];
            ++m_iCurrentCrystals;

            // Animate / VFX
            m_rDisplayStat.HideUIGamePanel(false);
            UpdateStatus();

        }
    }
}
