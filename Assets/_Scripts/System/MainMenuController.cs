using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button m_playButton;
    [SerializeField] private AudioSource m_rButtonClick;

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
    }
    
    public void PlayGame() {
        m_rButtonClick.Play();
        //this line changes the speed mode, currenly set to no speed run
        GameState.SetSpeedRunning(GameState.SpeedRunMode.SpeedRun);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Settings()
    {
        m_rButtonClick.Play();
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
}
