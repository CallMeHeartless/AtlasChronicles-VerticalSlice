using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimerUpdate : MonoBehaviour
{
    bool m_EndTimer = true;
    float m_Seconds = 0;
    int m_Minutes = 0;
    int m_Hours = 0;
    private Text m_TextUI;
    private Text m_TypeUI;
    private Trophies m_Trophy;
    // Start is called before the first frame update
    void Start()
    {
        //set UI to for the speed run
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
           
              
            default:
                break;
        }

      
        if (GameState.GetSpeedRunning() == GameState.SpeedRunMode.Expore)
        {
            //this is not going to be a speed run
            transform.parent.gameObject.SetActive(false);
        } 
    }
    /*___________________________________________________
  * Job: Timer which looks like a speedrunners Timer
  * Ceratior: Nicholas
  ______________________________________________________*/
    void Update()
    {
        
        if ((!GameState.GetPauseFlag()) &&(!GameState.GetCinematicFlag()))//pause the game
        {
            if (m_EndTimer)
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
                if (Records.check((m_Hours*10000)+(m_Minutes * 100) + (int)m_Seconds, GameState.GetSpeedRunning()))
                {
                    Debug.Log("call");
                    m_Trophy.DecreaseTrophie();
                }
            }
        }
    }
    public void StartTimer(){
        m_EndTimer = true;
    }
    public void StopTimer()
    {
        m_EndTimer = false;
        m_TextUI.fontSize = 50;
    }

    public float GetFinalTime()
    {
        return (m_Hours * 10000) + (m_Minutes * 100) + (int)m_Seconds;
    }
}
