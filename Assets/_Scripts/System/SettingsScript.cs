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
    [SerializeField] private Image[] m_rTabs;           // The tab array containing settings tab images
    [SerializeField] private GameObject[] m_rGroups;    // The group aray containing each settings tag group 
    [SerializeField] private GameObject m_rSettings;    // The Settings gameObject
    [SerializeField] private AudioSource m_rButtonMove; //The audio to play when moving across buttons
    [SerializeField] private Color m_inactiveColour;    //The button's inactive colour
    [SerializeField] private Color m_highlightedColour; //The button's highlighted colour
    [SerializeField] private Toggle m_camToggleX, m_camToggleY; //The toggle UI components
    [SerializeField] private AudioMixer m_rMixer;       //The Audio Mixer for the game
    [SerializeField] private GameObject m_rInputManagerPrefab; //The Prefab for the input manager (used to instantiate if it doesnt exist in current scene)

    private CinemachineFreeLook m_rCineCamera;  //The camera reference
    private int m_currentGroup = -1;            //The currently selected tab group
    
    //Create a UnityEvent for the script
    public UnityEvent OnBPressed;

    private void Start()
    {
        //Make sure the settings panel is hidden
        m_rSettings.SetActive(false);

        // Get the references for necessary components
        m_rCineCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineFreeLook>();

        if (m_rCineCamera) 
        {
            //Assign player-stored camera values into cinemachine
            m_rCineCamera.m_XAxis.m_InvertInput = PlayerPrefsManager.RetrieveCamX();
            m_rCineCamera.m_YAxis.m_InvertInput = PlayerPrefsManager.RetrieveCamY();
        }

        //Set toggle buttons within settings based on player preferences
        if (m_camToggleX && m_camToggleY)
        {
            m_camToggleX.isOn = PlayerPrefsManager.RetrieveCamX();
            m_camToggleY.isOn = PlayerPrefsManager.RetrieveCamY();
        }

        //Set mixer values based on what is stored in player preferences
        if (m_rMixer)
        {
            m_rMixer.SetFloat("BGMVol", Mathf.Log10(PlayerPrefsManager.RetrieveAudioBGM()) * 20);
            m_rMixer.SetFloat("SFXVol", Mathf.Log10(PlayerPrefsManager.RetrieveAudioVFX()) * 20);
        }

        if(InputManager.m_instance == null)
        {
            Instantiate(m_rInputManagerPrefab);
        }
    }

    private void Awake()
    {
        // If Cinemachine exists in this context, 
        // set the current camera settings to the cinemachine camera.
        if (m_rCineCamera)
        {
            m_rCineCamera.m_XAxis.m_InvertInput = PlayerPrefsManager.RetrieveCamX();
            m_rCineCamera.m_YAxis.m_InvertInput = PlayerPrefsManager.RetrieveCamY();
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
        //Don't access function if the settings panel is inactive and the tabs and groups do not exist
        if (!m_rSettings.activeSelf || m_rTabs == null || m_rGroups == null)
        {
            return;
        }

        //Switch tab groups by shifting to the left
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
            //Switch tab groups by shifting to the right
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

    public void SetCurrentGroup(int _i)
    {
        m_currentGroup = _i;
        m_rTabs[_i].color = m_highlightedColour;
        m_rGroups[_i].SetActive(true);
    }

    public void HideInactiveGroups()
    {
        for (int i = 0; i < m_rTabs.Length; ++i)
        {
            //Hide current group
            m_rTabs[i].color = m_inactiveColour;
            m_rGroups[i].SetActive(false);
        }
    }

    public void ToggleCameraX(bool _invert)
    {
        if (m_rCineCamera) //If cinemachine exists
        {
            //Set the x axis toggle value
            m_rCineCamera.m_XAxis.m_InvertInput = _invert;
        }
        //Store value in the player prefs
        PlayerPrefsManager.StoreCamX(_invert);
    }

    public void ToggleCameraY(bool _invert)
    {
        if (m_rCineCamera) //If cinemachine exists
        {
            //Set the y axis toggle value
            m_rCineCamera.m_YAxis.m_InvertInput = _invert;
        }
        //Store value in the player prefs
        PlayerPrefsManager.StoreCamY(_invert);
    }
}
