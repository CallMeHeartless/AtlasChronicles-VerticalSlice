using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimerUpdate : MonoBehaviour
{
    bool m_bTimerOn=false;
    float m_Seconds = 0;
    int m_Minutes = 0;
    int m_Hours = 0;
    private Text m_TextUI;
    private Text m_TypeUI;
    private Trophies m_Trophy;
    // Start is called before the first frame update
    void Start()
    {
        m_TextUI = GetComponent<Text>();
        m_Trophy = transform.parent.GetChild(1).gameObject.GetComponent<Trophies>();
        m_TypeUI = transform.parent.GetChild(2).gameObject.GetComponent<Text>();


        switch (GameState.GetSpeedRunning())
        {
            case GameState.SpeedRunMode.Expore:
                break;
            case GameState.SpeedRunMode.SpeedRun:
                m_TypeUI.text = "200 gems, 5 map and out";
                break;
            case GameState.SpeedRunMode.EveryThing:
                m_TypeUI.text = "Get All";
                break;
            case GameState.SpeedRunMode.Finished:
               
                break;
            default:
                break;
        }

      
        if (GameState.GetSpeedRunning() == GameState.SpeedRunMode.Expore)
        {
            transform.parent.gameObject.SetActive(false);
        }
        else
        {
            m_bTimerOn = true;
        }
    }
    /*___________________________________________________
  * Job: Timer which looks like a speedrunners
  * Ceratior: Nicholas
  ______________________________________________________*/
    // Update is called once per frame
    void Update()
    {
        if (m_bTimerOn)
        {
            m_Seconds += Time.deltaTime;
            if (m_Seconds >= 60)
            {
                m_Minutes++;
                m_Seconds -= 60;
                if (m_Minutes >= 60)
                {
                    m_Hours++;
                    m_Minutes -= 60;
                }
            }
            // UI.text = Hours.ToString("000") + " : " + Minutes.ToString("00") + " : ";

            m_TextUI.text = null;

            if (m_Hours >= 1)
            {
                m_TextUI.text += m_Hours.ToString("0") + ":";
            }

            if (m_Minutes >= 1)
            {
                m_TextUI.text += m_Minutes.ToString("0") + ":";
            }

            if (m_Seconds < 10)
            {
                m_TextUI.text += "0";
            }
            m_TextUI.text += m_Seconds.ToString("F2");

            //check to see if troiphy need to be changed
           if( Records.check((m_Minutes * 100)+ (int)m_Seconds, GameState.GetSpeedRunning())){
                Debug.Log("call");
                m_Trophy.DecreaseTrophie();
            }
        }

    }
    public void StartTimer(){
        m_bTimerOn = true;
    }
    public void StopTimer()
    {
        m_bTimerOn = false;
        m_TextUI.fontSize = 50;
    }
}
