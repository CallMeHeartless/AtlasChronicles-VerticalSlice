using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpeedMenu : MonoBehaviour
{
    private int HighlightedMode;
    private float m_fTime;
    private int m_Trophie;
    // Start is called before the first frame update


//change the time and trophie so they are of the new mode
    public void UpdateMenu(int _HighlightedMode)
    {
        HighlightedMode = _HighlightedMode;
        GameObject Object = GameObject.FindGameObjectWithTag("TimeRecords");

        Object.GetComponent<DontDestoryRecords>().GetSpeedMod(HighlightedMode, out m_fTime, out m_Trophie);

        transform.GetChild(1).gameObject.GetComponent<TrophieUI>().setSprite(m_Trophie);
        transform.GetChild(2).gameObject.GetComponent<TrophieUI>().setSprite(HighlightedMode);


        string Nest;
        if ((HighlightedMode == 0)|| m_fTime== 0)
        {
             Nest = "-- : -- : --";
        }
        else
        {
            Nest = m_fTime.ToString();
            if (Nest.Length < 6)
            {
                //add in :
                string dummy = Nest[1].ToString();
                for (int i = 0; i < Nest.Length; i++)
                {
                    if (Nest.Length - 5 == i)
                    {
                        dummy = dummy + " : ";
                    }
                    dummy = dummy + Nest[i].ToString();
                }
                Nest = dummy;
            }

            if (Nest.Length < 9)
            {
                //add in :
                string dummy = Nest[1].ToString();
                for (int i = 0; i < Nest.Length; i++)
                {
                    if (Nest.Length - 8 == i)
                    {
                        dummy = dummy + " : ";
                    }
                    dummy = dummy + Nest[i].ToString();
                }
                Nest = dummy;
            }
        }
        transform.GetChild(3).GetChild(0).gameObject.GetComponent<Text>().text = Nest;
    }
    //start with the current mode
    public void StartGame()
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