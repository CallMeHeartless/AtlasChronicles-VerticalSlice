using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ModeMenu : MonoBehaviour
{
    [SerializeField] private GameState.GameplayMode m_levelMode;

    [SerializeField] private TextMeshProUGUI m_rRecordFlavourTxt;
    [SerializeField] private TextMeshProUGUI m_rCurrentRecordTxt;

    [SerializeField] private Image m_rCurrentCup;
    [SerializeField] private Sprite m_rHiddenCupSpr;
    [SerializeField] private Sprite m_rBronzeCupSpr;
    [SerializeField] private Sprite m_rSilverCupSpr;
    [SerializeField] private Sprite m_rGoldCupSpr;

    [SerializeField] private TextMeshProUGUI m_rGoldTime;
    [SerializeField] private TextMeshProUGUI m_rSilverTime;
    [SerializeField] private TextMeshProUGUI m_rBronzeTime;

    [SerializeField] private Button m_rStartButton;

    private string m_strPlayerBestPlace = "PP_TimeAttackCurrentPlace";
    private string m_strPlayerBestTimeStr = "PP_TimeAttackTimeString";
    private string m_strPlayerBestTimeInt = "PP_TimeAttackTimeInt";

    private float m_fTime;
    private int m_iHighlightedMode;
    private int m_Trophie = 0;           

    private int m_iCurrentPlace = 0;
    private int m_iBestScore = 0;
    private int m_iBestTime = 0;

    private void Start()
    {
        //Establish unique strings/codes used to access scores from player prefs depending on which mode is selected
        Records.PlayerPrefModeRetriever(m_levelMode, ref m_strPlayerBestPlace, ref m_strPlayerBestTimeStr, ref m_strPlayerBestTimeInt);

        //Check if button exists in children
        Button resetButton = GetComponentInChildren<Button>();
        if (resetButton)
        {
            resetButton.onClick.AddListener(delegate { ResetScores(); });
        }
    }
    
    private void Update()
    {
        m_rStartButton.Select();
        m_rStartButton.OnSelect(null);

        if (Input.GetAxis("YButton") != 0)
        {
            ResetScores();
        }
    }

    public void ResetScores()
    {
        //Resets scores by resetting player pref values and then updating current ui once
        PlayerPrefs.SetInt(m_strPlayerBestPlace, 0);
        PlayerPrefs.SetString(m_strPlayerBestTimeStr, "--:--:--");
        PlayerPrefs.SetInt(m_strPlayerBestTimeInt, 111111);
        UpdateTimerPanelValues();
    }

    /// <summary>
    /// Sets the cup sprite and the flavour text depending on what the current score is
    /// </summary>
    /// <param name="_currentPlace">The placement/cup prize depending on the time attack score</param>
    public void UpdateTimerPanelValues()
    {
        Records.PlayerPrefModeRetriever(m_levelMode, ref m_strPlayerBestPlace, ref m_strPlayerBestTimeStr, ref m_strPlayerBestTimeInt);

        //Retrieve scores from player prefs
        string strTimeString = PlayerPrefs.GetString(m_strPlayerBestTimeStr, "--:--:--");
        int currentPlace = PlayerPrefs.GetInt(m_strPlayerBestPlace, 0);

        //Update score text ui
        m_rCurrentRecordTxt.text = strTimeString;
        m_rRecordFlavourTxt.text = Records.RetrieveFlavourText(true, m_levelMode, currentPlace);
        
        m_rGoldTime.text = Records.GetGoalScore(m_levelMode, 3);
        m_rSilverTime.text = Records.GetGoalScore(m_levelMode, 2);
        m_rBronzeTime.text = Records.GetGoalScore(m_levelMode, 1);

        switch (currentPlace)
        {
            case 3:
            {
                //If the time beats the Gold cup record
                m_rCurrentCup.sprite = m_rGoldCupSpr;
                break;
            }
            case 2:
            {
                //If the time beats the Silver cup record
                m_rCurrentCup.sprite = m_rSilverCupSpr;
                break;
            }
            case 1:
            {
                //If the time beats the Bronze cup record
                m_rCurrentCup.sprite = m_rBronzeCupSpr;
                break;
            }
            default: //Case 0
            {
                //If NONE of the records were beaten, a hidden cup is displayed
                m_rCurrentCup.sprite = m_rHiddenCupSpr;
                break;
            }
        }
    }

}