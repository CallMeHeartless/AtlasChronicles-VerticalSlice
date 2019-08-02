﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    [SerializeField]
    private float m_fGaugeLimitValue = 1.0f; // The current limit, influenced by the number of crystals collected
    [SerializeField][Tooltip("The time in seconds it takes for the ink gauge to refill.")]
    private float m_fRefillTime = 5.0f;
    [SerializeField][Tooltip("The delay before the ink gauge begins to refill after being used.")]
    private float m_fRecoveryTime = 5.0f;
    private float m_fRecoveryCounter = 0.0f;
    [SerializeField]  private EInkGaugeState m_eGaugeState = EInkGaugeState.eIdle;
    private Image m_rInkFill;
    [SerializeField]
    private TextMeshProUGUI m_rDebugText;

    private void Awake() {
        if (!s_rInstance) {
            s_rInstance = this;
        } else if (s_rInstance != this) {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start(){
        m_rInkFill = GetComponent<Image>();
    }

    void Update(){
        // Determine what should happen this frame
        switch (m_eGaugeState) {
            // The ink gauge is depleting until it is empty
            case EInkGaugeState.eDraining: {
                m_fValue -= Time.deltaTime;
                UpdateInkGauge();
                break;
            }

            // A cooldown period before the ink gauge starts refilling
            case EInkGaugeState.eWaiting: {
                ProcessRecovery();
                break;
            }

            // The ink gauge refills 
            case EInkGaugeState.eRefilling: {
                m_fValue += Time.deltaTime;
                UpdateInkGauge();
                break;
            }

            // The ink gauge is full and not in a state of transition 
            case EInkGaugeState.eIdle: {
                break;
            }

            default:break;
        }
    }

    // Set Slider Value and checks for state change
    private void UpdateInkGauge() {
        m_fValue = Mathf.Clamp(m_fValue, 0.0f, m_fGaugeLimitValue);
        // UI change (Change image fill amount based on values)
        m_rInkFill.fillAmount = m_fValue / m_fGaugeLimitValue;
        m_rDebugText.text = "Fill Amount: \n" + m_rInkFill.fillAmount;
        // Check for a state change
        if (m_fValue == 0.0f) {
            m_eGaugeState = EInkGaugeState.eWaiting;
            // End any active map vision state
            Zone.ToggleMapVision(false);
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
        if(m_eGaugeState == EInkGaugeState.eIdle) { // Increase level now if the gauge is resting
            m_fValue = m_fGaugeLimitValue;
        }
        UpdateInkGauge(); // Adjust UI
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

    // Switches map vision based on current ink values
    public void HandleMapVision() {
        switch (m_eGaugeState) {
            case EInkGaugeState.eDraining: {
                // Map vision off
                Zone.ToggleMapVision(false);
                m_eGaugeState = EInkGaugeState.eWaiting;
                break;
            }

            case EInkGaugeState.eWaiting: {
                // Do nothing
                break;
            }

            case EInkGaugeState.eRefilling: {
                // Do nothing
                //Zone.ToggleMapVision(true);
                //m_eGaugeState = EInkGaugeState.eDraining;
                break;
            }

            case EInkGaugeState.eIdle: {
                // Map vision on
                Zone.ToggleMapVision(true);
                m_eGaugeState = EInkGaugeState.eDraining;
                break;
            }

            default: break;
        }
    }
    
}
