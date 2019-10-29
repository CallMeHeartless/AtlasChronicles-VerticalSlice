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
        Records.PlayerPrefModeRetriever(m_levelMode, ref m_strPlayerBestPlace, ref m_strPlayerBestTimeStr, ref m_strPlayerBestTimeInt);
    }

    //call this when you are change which which mode you highlighted 
    //change the time and trophie so they are of the new mode
    //public void UpdateMenu(int _HighlightedMode)
    //{
    //    m_iHighlightedMode = _HighlightedMode;
    //    GameObject Object = GameObject.FindGameObjectWithTag("TimeRecords");

    //    Object.GetComponent<DontDestory>().GetSpeedMode(m_iHighlightedMode, out m_fTime, out m_Trophie);

    //    string Nest;
    //    if ((m_iHighlightedMode == 0)|| m_fTime== 0)
    //    {
    //         Nest = "-- : -- : --";
    //    }
    //    else
    //    {
    //        Nest = m_fTime.ToString();
    //        if (Nest.Length < 6)
    //        {
    //            //add in :
    //            string m_timeString = Nest[1].ToString();
    //            for (int i = 0; i < Nest.Length; i++)
    //            {
    //                if (Nest.Length - 5 == i)
    //                {
    //                    m_timeString = m_timeString + " : ";
    //                }
    //                m_timeString = m_timeString + Nest[i].ToString();
    //            }
    //            Nest = m_timeString;
    //        }

    //        if (Nest.Length < 9)
    //        {
    //            //add in :
    //            string m_timeString = Nest[1].ToString();
    //            for (int i = 0; i < Nest.Length; i++)
    //            {
    //                if (Nest.Length - 8 == i)
    //                {
    //                    m_timeString = m_timeString + " : ";
    //                }
    //                m_timeString = m_timeString + Nest[i].ToString();
    //            }
    //            Nest = m_timeString;
    //        }
    //    }
    //    transform.GetChild(3).GetChild(0).gameObject.GetComponent<Text>().text = Nest;
    //    m_timeString = Nest;

    //}
    //pressing xboxA should trigger this
    //start with the current mode
    //public string GetTimeFlag()
    //{
    //    return m_timeString;
    //}
    //public int GettrophieFlag()
    //{
    //    return m_Trophie;
    //}
    
    public void ResetScores()
    {
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

        //int currentPlace = PlayerPrefs.GetInt(m_strPlayerBestPlace, 0);
        int currentIntTime = PlayerPrefs.GetInt(m_strPlayerBestTimeInt, 111111);
        string strTimeString = PlayerPrefs.GetString(m_strPlayerBestTimeStr, "--:--:--");
        int currentPlace = Records.CheckCurrentPlace(m_levelMode, currentIntTime);
        m_rCurrentRecordTxt.text = strTimeString;
        m_rRecordFlavourTxt.text = Records.RetrieveFlavourText(true, m_levelMode, currentPlace);

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