using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimerUpdate : MonoBehaviour
{
    bool TimerOn=false;
    float Seconds = 0;
    int Minutes = 0;
    int Hours = 0;
    private Text UI;
    // Start is called before the first frame update
    void Start()
    {
        UI = GetComponent<Text>();
        if (GameState.GetSpeedRunning() == GameState.SpeedRunMode.Expore)
        {
            transform.parent.gameObject.SetActive(false);
        }
        else
        {
            TimerOn = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerOn)
        {
            Seconds += Time.deltaTime;
            if (Seconds>= 60)
            {
                Minutes++;
                Seconds-= 60;
                if (Minutes >= 60)
                {
                    Hours++;
                    Minutes-= 60;
                }
            }
           // UI.text = Hours.ToString("000") + " : " + Minutes.ToString("00") + " : ";

            UI.text = null;

            if (Hours >= 1)
            {
                UI.text += Hours.ToString("0") + " : ";
            }

            if (Minutes >= 1)
            {
                UI.text += Minutes.ToString("00") + " : ";
            }

            if (Seconds<10)
            {
                UI.text += "0";
            }
            UI.text += Seconds.ToString("F2");
        }

    }
    public void StartTimer(){
        TimerOn = true;
    }
    public void StopTimer()
    {
        TimerOn = false;
        UI.fontSize = 50;
    }
}
