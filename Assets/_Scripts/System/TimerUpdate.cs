using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Cinemachine;

#pragma warning disable CS0649

public class TimerUpdate : MonoBehaviour
{
    bool m_EndTimer = true;
    float m_Seconds = 0;
    int m_Minutes = 0;
    int m_Hours = 0;

    bool m_bResultsChecked = false;
    [SerializeField] private GameObject m_timerUIPanel;
    [SerializeField] private GameObject m_TimeAttackResultsPanel;
    [SerializeField] private CinemachineFreeLook m_rCamera; 

    [SerializeField] private TextMeshProUGUI m_TextUI;
    [SerializeField] private TextMeshProUGUI m_TypeUI;
    [SerializeField] private Trophies m_Trophy;

    [SerializeField] private TextMeshProUGUI m_rPanelTitle;
    [SerializeField] private TextMeshProUGUI m_rGoldTime;
    [SerializeField] private TextMeshProUGUI m_rSilverTime;
    [SerializeField] private TextMeshProUGUI m_rBronzeTime;
    [SerializeField] private TextMeshProUGUI[] m_rGoalFormats;

    [SerializeField] private TextMeshProUGUI m_rFlavourText;
    [SerializeField] private TextMeshProUGUI m_rCurrentRecordTime;
    [SerializeField] private Image m_rCurrentTrophy;

    [SerializeField] private Sprite m_rHiddenTrophy;
    [SerializeField] private Sprite m_rBronzeTrophy;
    [SerializeField] private Sprite m_rSilverTrophy;
    [SerializeField] private Sprite m_rGoldTrophy;
    private string m_strFlavourText = "--INVALID--";
    private int m_iTrophyPlacement = 0;

    private string m_strPlayerBestPlace = "PP_TimeAttackCurrentPlace";
    private string m_strPlayerBestTimeStr = "PP_TimeAttackTimeString";
    private string m_strPlayerBestTimeInt = "PP_TimeAttackTimeInt";

    static private float m_AddedTime =0;
    // Start is called before the first frame update
    void Start()
    {
        SetRecordChecked(false);

        if(!m_rCamera)
        {
            m_rCamera = GameObject.Find("Camera").transform.parent.GetComponent<CinemachineFreeLook>();
        }

        //set UI to for the speed run
        switch (GameState.GetGameplayMode())
        {
            case GameState.GameplayMode.Adventure:
            {
                m_TypeUI.text = "Adventure Mode";
                m_timerUIPanel.SetActive(false);
                break;
            }
            case GameState.GameplayMode.SpeedRun:
            {
                m_TypeUI.text = "Time Attack: COLLECT 160 GEMS AND 5 MAPS BEFORE HEADING TO THE EXIT";
               // m_timerUIPanel.SetActive(true);

                m_rCurrentRecordTime.text = "--:--:--";
                m_rFlavourText.text = "GOOD JOB";
                m_rCurrentTrophy.sprite = m_rHiddenTrophy;
                break;
            }
            case GameState.GameplayMode.Hoarder:
            {
                m_TypeUI.text = "Get All";
                break;
            }
            case GameState.GameplayMode.Rush:
                {
                    m_TypeUI.text = "Get to the end as fast as possable";
                    break;
                }
            case GameState.GameplayMode.MapHunt:
                {
                    m_TypeUI.text = "get all map fragment and don't get as little amount of crystals";
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

        Records.PlayerPrefModeRetriever(GameState.GetGameplayMode(), ref m_strPlayerBestPlace, ref m_strPlayerBestTimeStr, ref m_strPlayerBestTimeInt);
    }
    /*___________________________________________________
  * Job: Timer which looks like a speedrunners Timer
  * Ceratior: Nicholas
  ______________________________________________________*/
    void Update()
    {


//<<<<<<< HEAD
//        if (AddedTime != 0)
//        {
//            m_Seconds += AddedTime;
//        }
//        m_Seconds += Time.deltaTime;
//        if (m_Seconds >= 60)
//=======

        if ((!GameState.GetPauseFlag()) && (!GameState.GetCinematicFlag()))//pause the game
        {
            if (m_AddedTime != 0)
            {
                m_Seconds += m_AddedTime;
                m_AddedTime = 0;
            }

            if (GameState.GetGameplayMode() != GameState.GameplayMode.MapHunt)
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

                    //check to see if trophy needS to be changed
                    m_iTrophyPlacement = Records.CheckCurrentPlace(GameState.GetGameplayMode(), (m_Hours * 10000) + (m_Minutes * 100) + (int)m_Seconds);
                }
            }
        }
    }

    /// <summary>
    /// Activates the TimeAttackResults panel and updates all ui elements with time records
    /// </summary>
    public void DisplayEndResultsPanel()
    {
        DetermineFinalTime();
        SetRecordChecked(true);

        m_TimeAttackResultsPanel.SetActive(true);
        GameState.SetPauseFlag(true);
        m_rCamera.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        //Retrieve time record values required to be set into ui view
        string currentScoreString = PlayerPrefs.GetString(m_strPlayerBestTimeStr, "--:--:--");
        int currentScoreInt = PlayerPrefs.GetInt(m_strPlayerBestTimeInt, 111111);
        int currentPlace = Records.CheckCurrentPlace(GameState.GetGameplayMode(), currentScoreInt);

        //Convert scores to strings
        m_rCurrentRecordTime.text = currentScoreString;
        m_rFlavourText.text = Records.RetrieveFlavourText(false, GameState.GetGameplayMode(), currentPlace);

        SetModeSettings(GameState.GetGameplayMode());

        switch (currentPlace)
        {
            case 3:
            {
                //If the time beats the Gold cup record
                m_rCurrentTrophy.sprite = m_rGoldTrophy;
                break;
            }
            case 2:
            {
                //If the time beats the Silver cup record
                m_rCurrentTrophy.sprite = m_rSilverTrophy;
                break;
            }
            case 1:
            {
                //If the time beats the Bronze cup record
                m_rCurrentTrophy.sprite = m_rBronzeTrophy;
                break;
            }
            case 0:
            {
                m_rCurrentTrophy.sprite = m_rHiddenTrophy;
                break;
            }
            default:
            {
                break;
            }
        }
    }

    void SetModeSettings(GameState.GameplayMode _mode)
    {
        string goalFormat = "<br><br><br>H : M : S";

        switch (_mode)
        {
            case GameState.GameplayMode.SpeedRun:
            {
                m_rPanelTitle.text = "Time Attack Results";
                break;
            }
            case GameState.GameplayMode.Hoarder:
            {
                m_rPanelTitle.text = "Hoarder Results";
                break;
            }
            case GameState.GameplayMode.Rush:
            {
                m_rPanelTitle.text = "Rush Results";
                break;
            }
            case GameState.GameplayMode.MapHunt:
            {
                m_rPanelTitle.text = "Map Hunt Results";
                goalFormat = "LESS THAN<br><br><br>CRYSTALS";
                break;
            }
            default:
            {
                m_rPanelTitle.text = "Adventure Results";
                goalFormat = "ADVENTUREMODESHOULDNOTHAVEAFORMAT!";
                break;
            }
        }

        //Set the goal format depending on the current mode
        for (int i = 0; i < m_rGoalFormats.Length; i++)
        {
            if (i == 3 && _mode == GameState.GameplayMode.MapHunt) //If it is the player's record
            {
                m_rGoalFormats[3].text = "<br><br><br>CRYSTALS";
                break;
            }

            m_rGoalFormats[i].text = goalFormat;
        }

        //Update the goal scores depending on the current mode
        m_rGoldTime.text = Records.GetGoalScore(_mode, 3);
        m_rSilverTime.text = Records.GetGoalScore(_mode, 2);
        m_rBronzeTime.text = Records.GetGoalScore(_mode, 1);
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

    public void SetCheatFinalTime(int _hours, int _mins, float _secs)
    {
        m_Seconds = _secs;
        m_Minutes = _mins;
        m_Hours = _hours;
    }

    public void DetermineFinalTime()
    {
        string totalTimeString = "";
        int finalTimeInteger = 111111;

        int roundedSeconds = Mathf.RoundToInt(m_Seconds);

        if(GameState.GetGameplayMode() != GameState.GameplayMode.MapHunt)
        {
            string secs = (roundedSeconds >= 10 ? roundedSeconds.ToString() : "0" + roundedSeconds.ToString());
            string minutes = (m_Minutes >= 10 ? m_Minutes.ToString() : "0" + m_Minutes.ToString());
            string hours = (m_Hours >= 10 ? m_Hours.ToString() : "0" + m_Hours.ToString());

            totalTimeString = hours + ":" + minutes + ":" + secs;
            finalTimeInteger = ConvertHMSToInteger(m_Hours, m_Minutes, roundedSeconds);
        }
        else
        {
            totalTimeString = roundedSeconds.ToString();
            finalTimeInteger = ConvertHMSToInteger(m_Hours, m_Minutes, roundedSeconds);
        }

        //SETTING FINAL TIME SCORE
        PlayerPrefs.SetString(m_strPlayerBestTimeStr, totalTimeString);
        PlayerPrefs.SetInt(m_strPlayerBestTimeInt, finalTimeInteger);
    }

    int ConvertHMSToInteger(int _hours, int _mins, int _secs)
    {
        return (m_Hours * 10000) + (m_Minutes * 100) + _secs;
    }

   static public void CystalCollection()
    {
        m_AddedTime++;
    }
}
#pragma warning restore CS0649