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
    [SerializeField] GameObject m_rUIPanel;         // UI panel containing gameplay elements
    [SerializeField] AudioSource m_rButtonClick;    //Reference to click audio

    DisplayStat m_rDisplayStat;
    ChildActivator m_rScriptActivator;
    CinemachineFreeLook m_rCineCamera;              //Reference to main camera
    //CinematicManager m_rCineManager;                //Reference to cinematics manager that holds all cinematics

    private bool m_bIsPaused = false;               //Local pause variable

    // Start is called before the first frame update
    void Start()
    {
        //Reset pause menu 
        m_rPausePanel.SetActive(false);
        m_rSettingsPanel.SetActive(false);
        m_rMapPanel.SetActive(false);
        m_rUIPanel.SetActive(true);

        //Hide cursor on start
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameState.SetPauseFlag(false);

        //Find the camera
        m_rCineCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineFreeLook>();
        //Find player
        m_rScriptActivator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<ChildActivator>();
        //Find display stat class
        m_rDisplayStat = GetComponent<DisplayStat>();

        //Activate camera if component is not null
        if (m_rCineCamera != null)
            m_rCineCamera.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Pausing
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("XBoxStart"))
            && !m_rPausePanel.activeSelf && !m_rSettingsPanel.activeSelf && !m_rMapPanel.activeSelf)
        {
            m_rButtonClick.Play();          // Play a click sound when player accesses the pause menu
            m_rPausePanel.SetActive(true);  //Activate the pause menu
            m_rPauseSection.SetActive(true);//Activate the pause section
            m_rGuidePanel.SetActive(false); // Disable the guide panel when paused

            //Unlock cursor so player can use the mouse when the game is paused
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            //Disable camera usage when paused
            if (m_rCineCamera != null)
                m_rCineCamera.enabled = false;

            //Disable cinematic functionality when paused
            m_rScriptActivator.SetChildrenActive(false);
            //CinematicManager.ActivateCinematics(false);
            CinematicManager.PauseAllCinematics(true);

            // Set game as paused 
            GameState.SetPauseFlag(true);
            m_bIsPaused = true;

            m_rDisplayStat.HideUIGamePanel(false);
        }
        // Resume gameplay
        else if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("XBoxStart") || (Input.GetButtonDown("BButton")))
            && (m_rPausePanel.activeSelf || m_rSettingsPanel.activeSelf || m_rMapPanel.activeSelf))
        {
            m_rButtonClick.Play();              // Play a clicking sound to resume gameplay
            m_rSettingsPanel.SetActive(false);  // Hide settings if it is not already hidden
            m_rMapPanel.SetActive(false);       //Hide map settings if it is not already hidden

            //Enable pause UI (note: pause UI is initially hidden when other panels were active)
            m_rPausePanel.SetActive(false);  //Pause panel contains pause section and can contain a script in the future
            m_rPauseSection.SetActive(true); //Container for the pause buttons
            m_rUIPanel.SetActive(true);      //Re-activate the panel containing all the counters

            //Set gamestate pause to false
            GameState.SetPauseFlag(false);
            CinematicManager.PauseAllCinematics(false);

        }

        //Make sure cursor is hidden on resume
        if (m_bIsPaused && !m_rPausePanel.activeSelf && !m_rSettingsPanel.activeSelf && !m_rMapPanel.activeSelf)
        {
            //Set game as not paused
            m_bIsPaused = false;
            GameState.SetPauseFlag(false);
            CinematicManager.PauseAllCinematics(false);

            //Lock cursor when resumed
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            //Enable camera usage when resumed
            if (m_rCineCamera != null)
                m_rCineCamera.enabled = true;

            m_rDisplayStat.HideUIGamePanel(true);

            //Enable cinematic functionality when resumed
            //CinematicManager.ActivateCinematics(true);

            //NOTE: PAUSE AND RESUME CUTSCENES AFTER RESUMING
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
        Zone.ClearZones();

        SceneManager.LoadScene("Menu_Main");
    }

    public void ExitGame()
    {
        //Invoke an application exit function after 0.1f seconds when player presses exit game
        //In order to play a clear button click sound before exiting
        // If user is in the Unity editor, stop game mode
        // Otherwise, quit.
        Invoke("ChooseExitType", 0.1f);
    }

    public void ChooseExitType()
    {
        //If using the editor, switch off via variable, else, quit.
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
