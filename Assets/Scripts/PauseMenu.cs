using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject m_pausePanel;
    [SerializeField] GameObject m_settingsPanel;

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
        if (Input.GetKeyDown(KeyCode.Escape) && !m_pausePanel.activeSelf)
        {
            m_pausePanel.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && m_pausePanel.activeSelf)
        {
            m_pausePanel.SetActive(false);
            m_settingsPanel.SetActive(false);
        }

    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu_Main");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
