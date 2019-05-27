using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene("Tristan_Test");
    }

    public void Quit() {
        Application.Quit();
    }

    public void ToggleItem(GameObject _item) {
        bool bNewState = !_item.activeSelf;
        _item.SetActive(bNewState);
    }

    public void MakeButtonSelected(Button _button) {
        _button.Select();
    }
}
