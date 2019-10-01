﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button m_playButton;
    [SerializeField] private GameObject m_rMenuButtons;
    [SerializeField] private GameObject m_rModeSelection;
    [SerializeField] private Animator m_rNLOnPillar;
    [SerializeField] private Animator m_rNovemberLonesome;
    [SerializeField] private AudioSource m_rButtonClick;
    [SerializeField] private GameObject m_rAdventureModePanel;
    [SerializeField] private GameObject m_rTimeAttackModePanel;
    [SerializeField] private GameObject m_rLeftModeButton;
    [SerializeField] private GameObject m_rRightModeButton;
    [SerializeField] private GameObject m_rLoadingPanel;
    [SerializeField] private TextMeshProUGUI m_rModeTitleText;

    private Canvas m_rCanvas;

    //Values showing november lonesome on screen behind or infront of menus
    [SerializeField] private int m_iHiddenPlaneDist = 1;
    [SerializeField] private int m_iRevealedPlaneDist = 20;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        GameStats.s_iMapsBoard[0] = 0;
        GameStats.s_iCollectableBoard[0] = 0;
        if (m_playButton) {
            m_playButton.Select();
        }
        m_rCanvas = GetComponent<Canvas>();
        m_rLoadingPanel.SetActive(false);
        NavigateModeLeft(true);
    }

    public void ActivateMenu(bool _activate)
    {
        m_rMenuButtons.SetActive(_activate);
        if (_activate)
        {
            m_rCanvas.planeDistance = m_iRevealedPlaneDist;
        }
    }

    public void ActivateModeSelection(bool _activate)
    {
        m_rModeSelection.SetActive(_activate);
        if (_activate)
        {
            ActivateMenu(false);
            m_rCanvas.planeDistance = m_iRevealedPlaneDist;
            m_rNLOnPillar.SetBool("ShowMode", true);
        }
        else
        {
            ActivateMenu(true);
            m_rNLOnPillar.SetBool("ShowMode", false);
        }
    }

    /// <summary>
    /// Goes to mode select screen
    /// </summary>
    public void StartModeSelect()
    {
        m_rButtonClick.Play();
        ActivateModeSelection(true);
    }

    /// <summary>
    /// Starts game in adventure mode
    /// </summary>
    public void StartAdventure()
    {
        StartGame(GameState.SpeedRunMode.Adventure);
    }

    /// <summary>
    /// Starts game in adventure mode
    /// </summary>
    public void StartTimeAttack()
    {
        StartGame(GameState.SpeedRunMode.SpeedRun);
    }

    public void StartGame(GameState.SpeedRunMode _mode)
    {
        m_rButtonClick.Play();
        //m_rCanvas.planeDistance = m_iHiddenPlaneDist; //Hide November Lonesome

        //Set the game mode and allow player to run free
        GameState.SetSpeedRunning(_mode);
        GameState.SetPlayerFree();
        m_rNLOnPillar.SetBool("ShowMode", false);
        //m_rNLOnPillar.transform.GetChild(0).GetComponent<Animator>().SetTrigger("PopIn");

        //Activate loading panel and start game scene
        m_rLoadingPanel.SetActive(true);
        StartCoroutine(GameState.LoadingScene(SceneManager.GetActiveScene().buildIndex + 1));
        m_rNLOnPillar.SetTrigger("Action");
    }

    public void Settings()
    {
        m_rButtonClick.Play();
        //Hide november lonesome when settings are open
        m_rCanvas.planeDistance = m_iHiddenPlaneDist;
    }

    public void Quit() {
        m_rButtonClick.Play();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }

    public void ToggleItem(GameObject _item) {
        bool bNewState = !_item.activeSelf;
        _item.SetActive(bNewState);
    }

    public void MakeButtonSelected(Button _button) {
        _button.Select();
    }
    
    /// <summary>
    /// Sets a mode panel active depending on which side has been specified in the parameter
    /// </summary>
    /// <param name="_left">Whether the left mode (adventure) or time attack mode (right) is active</param>
    public void NavigateModeLeft(bool _left)
    {
        m_rLeftModeButton.SetActive((_left ? false : true));
        m_rRightModeButton.SetActive((_left ? true : false));
        m_rAdventureModePanel.SetActive((_left ? true : false));
        m_rTimeAttackModePanel.SetActive((_left ? false : true));
        m_rModeTitleText.text = (_left ? "ADVENTURE MODE" : "TIME ATTACK MODE");
    }
}
