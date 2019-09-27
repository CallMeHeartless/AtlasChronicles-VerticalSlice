using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpeedMenu : MonoBehaviour
{
   
    private float m_fTime;
    private int m_Trophie;
    // Start is called before the first frame update
   
    void UpdateMenu(int HighlightedMode)
    {
        GameObject Object = GameObject.FindGameObjectWithTag("TimeRecords");

        Object.GetComponent<DontDestoryRecords>().GetSpeedMod(HighlightedMode, out m_fTime, out m_Trophie);
    }
    void StartGame(int HighlightedMode)
    {
        GameState.SetSpeedRunning((GameState.SpeedRunMode)HighlightedMode);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public float GetTimeFlag()
    {
        return m_fTime;
    }
    public int GettrophieFlag()
    {
        return m_Trophie;
    }
}
