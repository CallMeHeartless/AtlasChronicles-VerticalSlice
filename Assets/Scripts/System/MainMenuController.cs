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
    [SerializeField]
    private Button PlayButton;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayButton) {
            PlayButton.Select();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit() {
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
