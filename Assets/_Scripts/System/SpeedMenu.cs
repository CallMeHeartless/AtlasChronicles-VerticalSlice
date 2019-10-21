using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SpeedMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_rRecordFlavourTxt;
    [SerializeField] private TextMeshProUGUI m_rCurrentRecordTxt;
    [SerializeField] private Image m_rCurrentCup;
    [SerializeField] private Sprite m_rHiddenCupSpr;
    [SerializeField] private Sprite m_rBronzeCupSpr;
    [SerializeField] private Sprite m_rSilverCupSpr;
    [SerializeField] private Sprite m_rGoldCupSpr;

    private int HighlightedMode;
    private float m_fTime;
    private int m_Trophie = 0;
    private string m_timeString;
    private int m_iCurrentPlace = 0;
    private int m_iBestScore = 0;

    //call this when you are change which which mode you highlighted 
    //change the time and trophie so they are of the new mode
    public void UpdateMenu(int _HighlightedMode)
    {
        HighlightedMode = _HighlightedMode;
        GameObject Object = GameObject.FindGameObjectWithTag("TimeRecords");

        Object.GetComponent<DontDestory>().GetSpeedMode(HighlightedMode, out m_fTime, out m_Trophie);
        
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
                string m_timeString = Nest[1].ToString();
                for (int i = 0; i < Nest.Length; i++)
                {
                    if (Nest.Length - 5 == i)
                    {
                        m_timeString = m_timeString + " : ";
                    }
                    m_timeString = m_timeString + Nest[i].ToString();
                }
                Nest = m_timeString;
            }

            if (Nest.Length < 9)
            {
                //add in :
                string m_timeString = Nest[1].ToString();
                for (int i = 0; i < Nest.Length; i++)
                {
                    if (Nest.Length - 8 == i)
                    {
                        m_timeString = m_timeString + " : ";
                    }
                    m_timeString = m_timeString + Nest[i].ToString();
                }
                Nest = m_timeString;
            }
        }
        transform.GetChild(3).GetChild(0).gameObject.GetComponent<Text>().text = Nest;
        m_timeString = Nest;

    }
    //pressing xboxA should trigger this
    //start with the current mode
    public void StartGame()
    {
        GameState.SetGameplayMode((GameState.GameplayMode)HighlightedMode);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public string GetTimeFlag()
    {
        return m_timeString;
    }
    public int GettrophieFlag()
    {
        return m_Trophie;
    }
    
    public void ResetScores()
    {
        PlayerPrefs.SetInt("TimeAttackCurrentPlace", 0);
        PlayerPrefs.SetString("TimeAttackTimeString", "--:--:--");
        UpdateTimerPanelValues();
    }

    /// <summary>
    /// Sets the cup sprite and the flavour text depending on what the current score is
    /// </summary>
    /// <param name="_currentPlace">The placement/cup prize depending on the time attack score</param>
    public void UpdateTimerPanelValues()
    {
        int currentPlace = PlayerPrefs.GetInt("TimeAttackCurrentPlace", 0);
        m_rCurrentRecordTxt.text = PlayerPrefs.GetString("TimeAttackTimeString", "--:--:--");

        switch (currentPlace)
        {
            case 0:
            {
                //If NO of the records were beaten, a hidden cup is displayed
                m_rCurrentCup.sprite = m_rHiddenCupSpr;
                m_rRecordFlavourTxt.text = "TAKE ON A SPEEDY CHALLENGE";
                break;
            }
            case 3:
            {
                //If the time beats the Gold cup record
                m_rCurrentCup.sprite = m_rGoldCupSpr;
                m_rRecordFlavourTxt.text = "LEGENDARY";
                break;
            }
            case 2:
            {
                //If the time beats the Silver cup record
                m_rCurrentCup.sprite = m_rSilverCupSpr;
                m_rRecordFlavourTxt.text = "GREAT";
                break;
            }
            case 1:
            {
                //If the time beats the Bronze cup record
                m_rCurrentCup.sprite = m_rBronzeCupSpr;
                m_rRecordFlavourTxt.text = "YOU TRIED";
                break;
            }
            default:
            {
                m_rCurrentCup.sprite = m_rHiddenCupSpr;
                m_rRecordFlavourTxt.text = "TAKE ON A SPEEDY CHALLENGE";
                break;
            }
        }
    }

}