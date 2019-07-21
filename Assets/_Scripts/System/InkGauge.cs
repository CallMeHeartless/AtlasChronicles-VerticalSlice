using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InkGauge : MonoBehaviour
{
    // Enumerations to describe the state of the ink gauge
    public enum EInkGaugeState {
        eIdle = 0,
        eDraining,
        eWaiting,
        eRefilling
    }

    // Internal Variables
    private static InkGauge s_rInstance = null;
    [Header("Properties")]
    [SerializeField]
    private float m_fValue = 0.0f;
    [SerializeField][Tooltip("The time in seconds that a full ink gauge will last for.")]
    private float m_fMaxValue = 30.0f;
    private float m_fGaugeLimitValue; // The current limit, influenced by the number of crystals collected
    [SerializeField][Tooltip("The time in seconds it takes for the ink gauge to refill.")]
    private float m_fRefillTime = 5.0f;
    [SerializeField][Tooltip("The delay before the ink gauge begins to refill after being used.")]
    private float m_fRecoveryTime = 5.0f;
    private float m_fRecoveryCounter = 0.0f;
    private EInkGaugeState m_eGaugeState = EInkGaugeState.eIdle;
    [SerializeField]
    private Slider m_rInkSlider;

    private void Awake() {
        if (!s_rInstance) {
            s_rInstance = this;
        } else if (s_rInstance != this) {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start(){
        m_rInkSlider = GetComponent<Slider>();
    }

    void Update(){
        // Determine what should happen this frame
        switch (m_eGaugeState) {
            case EInkGaugeState.eDraining: {
                m_fValue -= Time.deltaTime;
                UpdateInkGauge();
                break;
            }

            case EInkGaugeState.eWaiting: {
                ProcessRecovery();
                break;
            }

            case EInkGaugeState.eRefilling: {
                m_fValue += Time.deltaTime;
                UpdateInkGauge();
                break;
            }

            case EInkGaugeState.eIdle: {
                break;
            }

            default:break;
        }
    }

    // Set Slider Value and checks for state change
    private void UpdateInkGauge() {
        m_fValue = Mathf.Clamp(m_fValue, 0.0f, m_fGaugeLimitValue);
        // UI change
        m_rInkSlider.value = m_fValue;

        // Check for a state change
        if(m_fValue == 0.0f) {
            m_eGaugeState = EInkGaugeState.eWaiting;
        }else if(m_fValue == m_fGaugeLimitValue) {
            m_eGaugeState = EInkGaugeState.eIdle;
        }
    }

    // Externally sets what the ink gauge should be doing
    public void SetGaugeState(EInkGaugeState _eState) {
        m_eGaugeState = _eState;
    }

    // A static instance is used for ease of access when pickups are collected
    public static InkGauge GetInstance() {
        return s_rInstance;
    }

    // Increments the current limit that the ink gauge can go to (based on secondary collectibles)
    public void IncrementGaugeLimit() {
        m_fGaugeLimitValue = Mathf.Lerp(0, m_fMaxValue, (float)GameStats.s_iSecondaryCollected / (float)GameStats.s_iSecondaryTotal);
    }

    // Process the recovery time after an ability has been used
    private void ProcessRecovery() {
        // Increment timer
        m_fRecoveryCounter += Time.deltaTime;
        // Check for end condition
        if(m_fRecoveryCounter >= m_fRecoveryTime) {
            // Reset and transition to refilling state
            m_fRecoveryCounter = 0.0f;
            m_eGaugeState = EInkGaugeState.eRefilling;
        }
    }
    
}
