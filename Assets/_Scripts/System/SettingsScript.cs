using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using MessageSystem;
using Cinemachine;

public class SettingsScript : MonoBehaviour
{
    private CinemachineFreeLook m_rCineCamera;
    private PlayerPrefsManager m_rPrefs;
    private int m_currentGroup = -1;

    [SerializeField] Image[] m_rTabs;
    [SerializeField] GameObject[] m_rGroups;
    [SerializeField] GameObject m_rSettings;

    [SerializeField] AudioSource m_rButtonMove;
    [SerializeField] Color m_inactiveColour;
    [SerializeField] Color m_highlightedColour;
    [SerializeField] Toggle m_camToggleX, m_camToggleY;
    [SerializeField] AudioMixer m_rMixer;

    public UnityEvent OnBPressed;

    private void Start()
    {
        //Make sure the settings panel is hidden
        m_rSettings.SetActive(false);

        // Get the references for necessary components
        m_rPrefs = PlayerPrefsManager.GetInstance();
        m_rCineCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineFreeLook>();

        if (m_rCineCamera) 
        {
            //Assign player-stored camera values into cinemachine
            m_rCineCamera.m_XAxis.m_InvertInput = m_rPrefs.RetrieveCamX();
            m_rCineCamera.m_YAxis.m_InvertInput = m_rPrefs.RetrieveCamY();
        }

        //Set toggle buttons within settings based on player preferences
        if (m_camToggleX && m_camToggleY)
        {
            m_camToggleX.isOn = m_rPrefs.RetrieveCamX();
            m_camToggleY.isOn = m_rPrefs.RetrieveCamY();
        }

        //Set mixer values based on what is stored in player preferences
        if (m_rMixer)
        {
            m_rMixer.SetFloat("BGMVol", Mathf.Log10(m_rPrefs.RetrieveAudioBGM()) * 20);
            m_rMixer.SetFloat("SFXVol", Mathf.Log10(m_rPrefs.RetrieveAudioVFX()) * 20);
        }
    }

    private void Awake()
    {
        // If Cinemachine exists in this context, 
        // set the current camera settings to the cinemachine camera.
        if (m_rCineCamera)
        {
            m_rCineCamera.m_XAxis.m_InvertInput = m_rPrefs.RetrieveCamX();
            m_rCineCamera.m_YAxis.m_InvertInput = m_rPrefs.RetrieveCamY();
        }

        if (m_rTabs != null && m_rGroups != null)
        {
            // Check for active tab
            for (int i = 0; i < m_rTabs.Length; ++i)
            {
                if (m_rGroups[i].activeSelf)
                {
                    m_currentGroup = i;
                    m_rTabs[m_currentGroup].color = m_highlightedColour;
                    m_rGroups[m_currentGroup].SetActive(true);
                }
            }

            // If no tabs are active set the first tab as the default ta
            if (m_currentGroup == -1)
            {
                m_currentGroup = 0;
                m_rTabs[m_currentGroup].color = m_highlightedColour;
                m_rGroups[m_currentGroup].SetActive(true);
                return;
            }

            // Deactivate any other active tabs // Just in case multiple are active
            for (int i = 0; i < m_rTabs.Length; ++i)
            {
                if (m_rGroups[i].activeSelf && i != m_currentGroup)
                {
                    m_rTabs[m_currentGroup].color = m_inactiveColour;
                    m_rGroups[m_currentGroup].SetActive(false);
                }
            }
        }
    }

    private void Update()
    {
        //If there are no tabs or groups to navigate, return.
        if (!m_rSettings.activeSelf || m_rTabs == null || m_rGroups == null)
        {
            return;
        }

        //Controller input to move between tabs in UI
        if (Input.GetButtonDown("L1"))
        {
            MoveTabLeft(true);
        }
        else if (Input.GetButtonDown("R1"))
        {
            MoveTabLeft(false);
        }
        else if (Input.GetButtonDown("BButton")) //Back button
        {
            //REMEMBER: BUTTON MAPPING
            OnBPressed.Invoke(); //Back
        }
    }

    public void PlayMoveAudio()
    {
        //Play button move audio if enabled
        if(m_rButtonMove.enabled && m_rSettings)
        {
            m_rButtonMove.Play();
        }
    }

    public void MoveTabLeft(bool _moveLeft)
    {
        if (!m_rSettings.activeSelf || m_rTabs == null || m_rGroups == null)
        {
            return;
        }

        if (_moveLeft)
        {
            if (m_currentGroup > 0)
            {
                //Hide current group
                m_rTabs[m_currentGroup].color = m_inactiveColour;
                m_rGroups[m_currentGroup].SetActive(false);

                --m_currentGroup;

                //Activate previous group
                m_rTabs[m_currentGroup].color = m_highlightedColour;
                m_rGroups[m_currentGroup].SetActive(true);
            }
        }
        else
        {
            if (m_currentGroup < m_rTabs.Length - 1)
            {
                //Hide current group
                m_rTabs[m_currentGroup].color = m_inactiveColour;
                m_rGroups[m_currentGroup].SetActive(false);

                ++m_currentGroup;

                //Activate previous group
                m_rTabs[m_currentGroup].color = m_highlightedColour;
                m_rGroups[m_currentGroup].SetActive(true);
            }
        }
        m_rButtonMove.Play();
    }

    public void ToggleCameraX(bool _invert)
    {
        if (m_rCineCamera) //If cinemachine exists
        {
            //Set the x axis toggle value
            m_rCineCamera.m_XAxis.m_InvertInput = _invert;
        }
        //Store value in the player prefs
        m_rPrefs.StoreCamX(_invert);
    }

    public void ToggleCameraY(bool _invert)
    {
        if (m_rCineCamera) //If cinemachine exists
        {
            //Set the y axis toggle value
            m_rCineCamera.m_YAxis.m_InvertInput = _invert;
        }
        //Store value in the player prefs
        m_rPrefs.StoreCamY(_invert);
    }
}
