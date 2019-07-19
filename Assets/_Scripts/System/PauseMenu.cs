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
    [SerializeField] GameObject m_pausePanel;
    [SerializeField] GameObject m_settingsPanel;
    //[SerializeField]
    CinemachineFreeLook m_cineCamera;
    [SerializeField] AudioSource m_rButtonClick;

    // Start is called before the first frame update
    void Start()
    {
        m_pausePanel.SetActive(false);
        m_settingsPanel.SetActive(false);
        m_cineCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineFreeLook>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Pausing
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("XBoxStart")) 
            && !m_pausePanel.activeSelf && !m_settingsPanel.activeSelf)
        {
            m_rButtonClick.Play();
            m_pausePanel.SetActive(true);
            //Pausing
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (m_cineCamera != null)
                m_cineCamera.enabled = false;
            GameState.SetPauseFlag(true);
        }
        else if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("XBoxStart") || (Input.GetButtonDown("BButton"))) 
            && (m_pausePanel.activeSelf || m_settingsPanel.activeSelf))
        {
            print("UnPause on button pressed");
            m_pausePanel.SetActive(false);
            m_settingsPanel.SetActive(false);
        }

        if (!m_pausePanel.activeSelf && !m_settingsPanel.activeSelf)
        {
            GameState.SetPauseFlag(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (m_cineCamera != null)
            {
                m_cineCamera.enabled = true;
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
