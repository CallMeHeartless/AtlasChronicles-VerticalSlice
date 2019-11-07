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
    [Header("Game Object References")]
    [SerializeField] private GameObject m_rTimerUIPanel;
    [SerializeField] private GameObject m_rTimeAttackResultsPanel;
    [SerializeField] private GameObject m_rPressToContinue;

    [Header("Camera Reference")]
    [SerializeField] private CinemachineFreeLook m_rCamera;

    [Header("Corner Information")]
    [SerializeField] private TextMeshProUGUI m_rTimeCornerTextUI;
    [SerializeField] private TextMeshProUGUI m_rModeTypeCornerTextUI;
    private string m_rModeDescription;
    [SerializeField] private Trophies m_rCornerTrophy;

    [Header("Result Panel Information")]
    [SerializeField] private TextMeshProUGUI m_rPanelTitle;
    [SerializeField] private TextMeshProUGUI m_rGoldTime;
    [SerializeField] private TextMeshProUGUI m_rSilverTime;
    [SerializeField] private TextMeshProUGUI m_rBronzeTime;
    [SerializeField] private TextMeshProUGUI[] m_rGoalFormats;
    [SerializeField] private TextMeshProUGUI m_rFlavourText;
    [SerializeField] private TextMeshProUGUI m_rCurrentRecordTime;

    [Header("Trophy references")]
    [SerializeField] private Image m_rCurrentTrophy;
    [SerializeField] private Sprite m_rHiddenTrophy;
    [SerializeField] private Sprite m_rBronzeTrophy;
    [SerializeField] private Sprite m_rSilverTrophy;
    [SerializeField] private Sprite m_rGoldTrophy;

    private DisplayStat m_rDisplayStat;

    private string m_strFlavourText = "--INVALID--";
    private int m_iTrophyPlacement = 0;

    private string m_strPlayerBestPlace = "PP_TimeAttackCurrentPlace";
    private string m_strPlayerBestTimeStr = "PP_TimeAttackTimeString";
    private string m_strPlayerBestTimeInt = "PP_TimeAttackTimeInt";

    static private float m_AddedTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        SetRecordChecked(false);

        if (!m_rCamera)
        {
            m_rCamera = GameObject.Find("Camera").transform.parent.GetComponent<CinemachineFreeLook>();
        }

        //set UI to for the speed run
        switch (GameState.GetGameplayMode())
        {
            case GameState.GameplayMode.Adventure:
            {
                m_rModeTypeCornerTextUI.text = "Adventure Mode";
                m_rTimerUIPanel.SetActive(false);
                m_rModeDescription = "Collect all Maps and Ink Crystals to restore the teleportation hub!";
                break;
            }
            case GameState.GameplayMode.SpeedRun:
            {
                m_rModeTypeCornerTextUI.text = "Time Attack Mode";
                m_rModeDescription = "Time Attack: COLLECT 160 GEMS AND 5 MAPS BEFORE HEADING TO THE EXIT";


                break;
            }
            case GameState.GameplayMode.Hoarder:
            {
                m_rModeTypeCornerTextUI.text = "Hoarder Mode";
                m_rModeDescription = "Collect all Maps, Ink Crystals and complete the level with the fastest time!";
                break;
            }
            case GameState.GameplayMode.Rush:
            {
                m_rModeTypeCornerTextUI.text = "Rush Mode";
                m_rModeDescription = "Get to the top of the temple as fast as you can!";
                break;
            }
            case GameState.GameplayMode.MapHunt:
            {
                m_rModeTypeCornerTextUI.text = "Map Hunt Mode";
                m_rModeDescription = "Collect all MAPS while collecting the least amount of Ink Crystals as possible";
                break;
            }
            default:
            break;
        }

        if (GameState.GetGameplayMode() == GameState.GameplayMode.Adventure)
        {
            //this is not going to be a speed run
            m_rTimeAttackResultsPanel.SetActive(false);
        }

        m_rPressToContinue.SetActive(false);
        m_rDisplayStat = transform.parent.GetComponent<DisplayStat>();
        if(m_rDisplayStat)
        {
            m_rDisplayStat.SetModeDescTxt(m_rModeDescription);
        }
        
        Records.PlayerPrefModeRetriever(GameState.GetGameplayMode(), ref m_strPlayerBestPlace, ref m_strPlayerBestTimeStr, ref m_strPlayerBestTimeInt);
    }

    /*___________________________________________________
  * Job: Timer which looks like a speedrunners Timer
  * Ceratior: Nicholas
  ______________________________________________________*/
    void Update()
    {
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
                    m_rTimeCornerTextUI.text = "";

                    if (m_Hours >= 1)
                    {
                        m_rTimeCornerTextUI.text += m_Hours.ToString("0") + ":";
                    }

                    if (m_Minutes >= 1)
                    {
                        m_rTimeCornerTextUI.text += m_Minutes.ToString("0") + ":";
                    }

                    if (m_Seconds < 10)
                    {
                        m_rTimeCornerTextUI.text += "0";
                    }
                    m_rTimeCornerTextUI.text += m_Seconds.ToString("F2");

                    //check to see if trophy needs to be changed
                    m_iTrophyPlacement = Records.CheckCurrentPlace(GameState.GetGameplayMode(), (m_Hours * 10000) + (m_Minutes * 100) + (int)m_Seconds);
                }
            }
            else
            {
                //Map mode
                m_rTimeCornerTextUI.text = GameStats.s_iCollectableBoard[GameStats.s_iLevelIndex].ToString();
            }
        }
    }

    /// <summary> Viv
    /// Activates the TimeAttackResults panel and updates all ui elements with time records
    /// </summary>
    public void DisplayEndResultsPanel()
    {
        DetermineFinalTime();
        SetRecordChecked(true);

        m_rTimeAttackResultsPanel.SetActive(true);
        GameState.SetPauseFlag(true);
        m_rCamera.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //Retrieve time record values required to be set into ui view
        string currentScoreString = PlayerPrefs.GetString(m_strPlayerBestTimeStr, "--:--:--");
        int currentScoreInt = PlayerPrefs.GetInt(m_strPlayerBestTimeInt, 111111);
        int currentPlace = Records.CheckCurrentPlace(GameState.GetGameplayMode(), currentScoreInt);
        PlayerPrefs.SetInt(m_strPlayerBestPlace, currentPlace);

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

    /// <summary> Viv
    /// Sets up the mode settings for whichever mode is being played
    /// </summary>
    /// <param name="_mode"></param>
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

    /// <summary> Viv
    /// Show press to continue text (used from game end controller)
    /// </summary>
    public void AllowPressToContinue()
    {
        m_rPressToContinue.SetActive(true);
    }

    /// <summary> Viv
    /// Checks if press to continue is currently active
    /// </summary>
    /// <returns></returns>
    public bool GetIsPressToContinueActive()
    {
        return m_rPressToContinue.activeSelf;
    }

    /// <summary> Viv
    /// Adds a 0 before a single digit
    /// </summary>
    /// <param name="_singleDigit"></param>
    /// <returns></returns>
    public string AddZeroBeforeSingleDigit(string _singleDigit)
    {
        return "0" + _singleDigit;
    }

    /// <summary> Viv
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

    /// <summary> Viv
    /// Setter for whether results have been activated/checked yet or not
    /// </summary>
    /// <param name="_checked"></param>
    public void SetRecordChecked(bool _checked)
    {
        m_bResultsChecked = _checked;
    }

    /// <summary> Viv
    /// Gets whether results have been activated/checked yet
    /// </summary>
    /// <returns></returns>
    public bool GetRecordChecked()
    {
        return m_bResultsChecked;
    }

    public void StartTimer() {
        m_EndTimer = true;
    }
    public void StopTimer()
    {
        m_EndTimer = false;
    }

    /// <summary> Viv
    /// Updates final time with cheat times //Not in build
    /// </summary>
    public void SetCheatFinalTime(int _hours, int _mins, float _secs)
    {
        m_Seconds = _secs;
        m_Minutes = _mins;
        m_Hours = _hours;
    }

    /// <summary> Viv
    /// Converts the final time into integer and string format to be stored
    /// </summary>
    public void DetermineFinalTime()
    {
        string totalTimeString = "";
        int finalTimeInteger = 111111;

        int roundedSeconds = Mathf.RoundToInt(m_Seconds);

        if (GameState.GetGameplayMode() != GameState.GameplayMode.MapHunt)
        {
            //Determine time taken to complete level if not map hunt mode
            //Manually converting niks way of storing time into string
            string secs = (roundedSeconds >= 10 ? roundedSeconds.ToString() : "0" + roundedSeconds.ToString());
            string minutes = (m_Minutes >= 10 ? m_Minutes.ToString() : "0" + m_Minutes.ToString());
            string hours = (m_Hours >= 10 ? m_Hours.ToString() : "0" + m_Hours.ToString());

            totalTimeString = hours + ":" + minutes + ":" + secs;
            finalTimeInteger = ConvertHMSToInteger(m_Hours, m_Minutes, roundedSeconds);
        }
        else
        {
            totalTimeString = roundedSeconds.ToString();
            finalTimeInteger = roundedSeconds;
        }

        //SETTING FINAL TIME SCORE
        PlayerPrefs.SetString(m_strPlayerBestTimeStr, totalTimeString);
        PlayerPrefs.SetInt(m_strPlayerBestTimeInt, finalTimeInteger);
    }

    /// <summary>
    /// Niks original way of converting his seconds mins and hours to time format to be stored
    /// </summary>
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