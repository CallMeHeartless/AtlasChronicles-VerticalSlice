using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject m_rPausePanel;      //Panel containing pause menu elements
    [SerializeField] GameObject m_rPauseSection;    //Pause menu UI
    [SerializeField] GameObject m_rSettingsPanel;   //Settings panel
    [SerializeField] GameObject m_rMapPanel;        // Map panel containing map elements
    [SerializeField] GameObject m_rGuidePanel;      // Guide panel containing tutorial elements

    CinemachineFreeLook m_rCineCamera;
    [SerializeField] AudioSource m_rButtonClick;

    private bool m_bIsPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        m_rPausePanel.SetActive(false);
        m_rSettingsPanel.SetActive(false);
        m_rMapPanel.SetActive(false);
        m_rCineCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineFreeLook>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // Pausing
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("XBoxStart")) 
            && !m_rPausePanel.activeSelf && !m_rSettingsPanel.activeSelf && !m_rMapPanel.activeSelf)
        {
            m_rButtonClick.Play();
            m_rPausePanel.SetActive(true);
            m_rPauseSection.SetActive(true); //Enable pause UI (note: pause UI was initially hidden when other panels were active)

            m_rGuidePanel.SetActive(false);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (m_rCineCamera != null)
                m_rCineCamera.enabled = false;
            GameState.SetPauseFlag(true);
        }
        // Resume gameplay
        else if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("XBoxStart") || (Input.GetButtonDown("BButton"))) 
            && (m_rPausePanel.activeSelf || m_rSettingsPanel.activeSelf || m_rMapPanel.activeSelf))
        {
            print("Resume on button pressed");
            m_rPausePanel.SetActive(false);
            m_rSettingsPanel.SetActive(false);
            m_rMapPanel.SetActive(false);
            m_rPauseSection.SetActive(true); //Enable pause UI (note: pause UI was initially hidden when other panels were active)
            GameState.SetPauseFlag(false);

        }

        //Make sure cursor is hidden on resume
        if (GameState.GetPauseFlag() && !m_rPausePanel.activeSelf && !m_rSettingsPanel.activeSelf && !m_rMapPanel.activeSelf)
        {
            GameState.SetPauseFlag(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (m_rCineCamera != null)
            {
                m_rCineCamera.enabled = true;
            }
        }
    }

    public void Click()
    {
        //Plays button click audio
        m_rButtonClick.Play();
    }

    public void MainMenu()
    {
        //Loads the main menu after playing the click audio
        Click();
        SceneManager.LoadScene("Menu_Main");
    }

    public void Map()
    {
        //Loads the main menu after playing the click audio
        Click();
    }

    public void ExitGame()
    {
        Click();

        //If user is in the Unity editor, quit application.
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
		    Application.Quit();
#endif
    }
}
