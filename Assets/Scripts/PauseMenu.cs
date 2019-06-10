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
    [SerializeField] CinemachineFreeLook m_cineCamera;

    // Start is called before the first frame update
    void Start()
    {
        m_pausePanel.SetActive(false);
        m_settingsPanel.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown("Pause"))
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("XBoxStart") && !m_pausePanel.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            m_pausePanel.SetActive(true);
            if(m_cineCamera != null)
                m_cineCamera.enabled = false;
            GameState.SetPauseFlag(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("XBoxStart") && m_pausePanel.activeSelf)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            m_pausePanel.SetActive(false);
            m_settingsPanel.SetActive(false);
            if (m_cineCamera != null)
                m_cineCamera.enabled = true;
            GameState.SetPauseFlag(false);
        }

        if (!m_pausePanel.activeSelf && !m_settingsPanel.activeSelf)
        {
            GameState.SetPauseFlag(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (m_cineCamera != null)
                m_cineCamera.enabled = true;
        }

    }

    public void ToggleCameraX(bool _invert)
    {
        m_cineCamera.m_XAxis.m_InvertInput = _invert;
    }

    public void ToggleCameraY(bool _invert)
    {
        m_cineCamera.m_YAxis.m_InvertInput = _invert;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu_Main");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
		    Application.Quit();
#endif
    }
}
