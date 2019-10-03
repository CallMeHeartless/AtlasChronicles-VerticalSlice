using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerUpdate : MonoBehaviour
{
    bool m_EndTimer = true;
    float m_Seconds = 0;
    int m_Minutes = 0;
    int m_Hours = 0;

    bool m_bResultsChecked = false;
    [SerializeField] private GameObject m_timerUIPanel;
    [SerializeField] private GameObject m_TimeAttackResultsPanel;

    [SerializeField] private TextMeshProUGUI m_TextUI;
    [SerializeField] private TextMeshProUGUI m_TypeUI;
    [SerializeField] private Trophies m_Trophy;

    [SerializeField] private TextMeshProUGUI m_rFlavourText;
    [SerializeField] private TextMeshProUGUI m_rCurrentRecordTime;
    [SerializeField] private TextMeshProUGUI m_rBestRecordTime;

    [SerializeField] private Image m_rCurrentTrophy;
    [SerializeField] private Image m_rBestTrophy;

    [SerializeField] private Sprite m_rHiddenTrophy;
    [SerializeField] private Sprite m_rBronzeTrophy;
    [SerializeField] private Sprite m_rSilverTrophy;
    [SerializeField] private Sprite m_rGoldTrophy;

    // Start is called before the first frame update
    void Start()
    {
        SetRecordChecked(false);

        //set UI to for the speed run
        switch (GameState.GetGameplayMode())
        {
            case GameState.GameplayMode.Adventure:
            {
                m_TypeUI.text = "Adventure Mode";
                m_timerUIPanel.SetActive(true);
                break;
            }
            case GameState.GameplayMode.SpeedRun:
            {
                m_TypeUI.text = "Time Attack: 160 gems, 5 map and out";
                m_timerUIPanel.SetActive(true);

                m_rCurrentRecordTime.text = "--:--:--";
                m_rBestRecordTime.text = "--:--:--";
                m_rFlavourText.text = "GOOD JOB";
                m_rCurrentTrophy.sprite = m_rHiddenTrophy;
                m_rBestTrophy.sprite = m_rHiddenTrophy;
                break;
            }
            case GameState.GameplayMode.Everything:
            {
                m_TypeUI.text = "Get All";
                break;
            }
            default:
                break;
        }

        if (GameState.GetGameplayMode() == GameState.GameplayMode.Adventure)
        {
            //this is not going to be a speed run
            m_TimeAttackResultsPanel.SetActive(false);
        } 
    }
    /*___________________________________________________
  * Job: Timer which looks like a speedrunners Timer
  * Ceratior: Nicholas
  ______________________________________________________*/
    void Update()
    {
        //If in Results page
        if(m_TimeAttackResultsPanel.activeSelf)
        {
            if (Input.GetAxis("Jump") != 0)
            {
                Zone.ClearZones();
                SceneManager.LoadScene(0);
            }
        }

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
                m_TextUI.text = "";

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
                if (Records.check((m_Hours*10000)+(m_Minutes * 100) + (int)m_Seconds, GameState.GetGameplayMode()))
                {
                    m_Trophy.DecreaseTrophie();
                }
            }
        }
    }

    /// <summary>
    /// Activates the TimeAttackResults panel and updates all ui elements with time records
    /// </summary>
    public void DisplayTimeAttackResults()
    {
        SetRecordChecked(false);

        m_TimeAttackResultsPanel.SetActive(true);

        //Retrieve time record values required to be set into ui view
        int currentPlace = PlayerPrefs.GetInt("TimeAttackCurrentPlace", 0);
        float currentScore = PlayerPrefs.GetFloat("TimeAttackCurrentTimeScore", 0);
        float bestScore = PlayerPrefs.GetFloat("TimeAttackGoldTimeScore", 0);
        string currentScoreString = PlayerPrefs.GetString("TimeAttackTimeString", "--:--:--");

        //Convert scores to strings
        m_rCurrentRecordTime.text = currentScoreString;
        //m_rBestRecordTime.text = 

        switch (currentPlace)
        {
            case 3:
            {
                //If the time beats the Gold cup record
                m_rCurrentTrophy.sprite = m_rGoldTrophy;
                m_rFlavourText.text = "LEGENDARY";
                break;
            }
            case 2:
            {
                //If the time beats the Silver cup record
                m_rCurrentTrophy.sprite = m_rSilverTrophy;
                m_rFlavourText.text = "NICE!!";
                break;
            }
            case 1:
            {
                //If the time beats the Bronze cup record
                m_rCurrentTrophy.sprite = m_rBronzeTrophy;
                m_rFlavourText.text = "GOOD JOB";
                break;
            }
            default:
            {
                m_rCurrentTrophy.sprite = m_rHiddenTrophy;
                m_rFlavourText.text = "TRY AGAIN";
                break;
            }
        }
    }

    public string AddZeroBeforeSingleDigit(string _singleDigit)
    {
        return "0" + _singleDigit;
    }

    /// <summary>
    /// Converts a float version of the time into a string with added semicolons.
    /// </summary>
    /// <param name="_time">A float of a given time</param>
    /// <returns></returns>
    public string FloatTimeToString(string _time)
    {
        string newTimeString = "";

        for (int i = 0; i < _time.Length; i++)
        {
            string newPair = "";
            newPair += _time[i];

            //Add a semicolon between the hrs, mins and seconds.
            if (_time[i] == ':' && newPair.Length == 1)
            {
                newPair = AddZeroBeforeSingleDigit(newPair);
                newTimeString += newPair;
            }
        }
        return newTimeString;
    }

    public void SetRecordChecked(bool _checked)
    {
        m_bResultsChecked = _checked;
    }

    public bool GetRecordChecked()
    {
        return m_bResultsChecked;
    }

    public void StartTimer(){
        m_EndTimer = true;
    }
    public void StopTimer()
    {
        m_EndTimer = false;
    }

    public float GetFinalTime()
    {
        string totalTimeString = "";

        int roundedSeconds = Mathf.RoundToInt(m_Seconds);

        string secs = (roundedSeconds > 10 ? roundedSeconds.ToString() : "0" + roundedSeconds.ToString());
        string minutes = (m_Minutes > 10 ? m_Minutes.ToString() : "0" + m_Minutes.ToString());
        string hours = (m_Hours > 10 ? m_Hours.ToString() : "0" + m_Hours.ToString());
        totalTimeString = hours + ":" + minutes + ":" + secs;

        PlayerPrefs.SetString("TimeAttackTimeString", totalTimeString);

        return (m_Hours * 10000) + (m_Minutes * 100) + (int)m_Seconds;
    }
}
