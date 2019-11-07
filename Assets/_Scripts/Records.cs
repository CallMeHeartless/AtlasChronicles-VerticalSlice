using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Records : MonoBehaviour
{
    //changing m_SpeedRunRecords changes that runs speed modes trophie freeshwhole
    static private int[] m_SpeedRunRecords_TimeAttack = new int[3] { 2000, 1400, 830 };// min min,second second
    static private int[] m_SpeedRunRecords_Hoarder = new int[3] { 2500, 2000, 1200 };// min min,second second
    static private int[] m_SpeedRunRecords_Rush = new int[3] { 230, 130, 110 };// min min,second second
    static private int[] m_SpeedRunRecords_MapRun = new int[3] { 150, 100, 50 };// min min,second second
    //static public int currentPlace = 3;

    static private string m_strNoRecordFlavourText = "BETTER LUCK NEXT TIME";

    static private string m_strGoldRecordFlavourText = "--INVALID GOLD TEXT--";
    static private string m_strSilverRecordFlavourText = "--INVALID SILVER TEXT--";
    static private string m_strBronzeRecordFlavourText = "--INVALID BRONZE TEXT--";
    static private int[] currentRecordCheck = m_SpeedRunRecords_TimeAttack;

    /// <summary>
    /// Flavour text based on if in menu, which mode and which placement the player is
    /// </summary>
    /// <param name="_mainMenu"></param>
    /// <param name="_currentMode"></param>
    /// <param name="_currentPlace"></param>
    /// <returns></returns>
    static public string RetrieveFlavourText(bool _mainMenu, GameState.GameplayMode _currentMode, int _currentPlace)
    {
        if (_currentMode == GameState.GameplayMode.Adventure)
        {
            return "Adventure Mode";
        }

        //Make sure code is accessing the correct records for the given mode
        SetCurrentGameModeSelected(_currentMode);

        switch (_currentMode)
        {
            case GameState.GameplayMode.SpeedRun:
            {
                m_strNoRecordFlavourText = (_mainMenu ? "TAKE ON A SPEEDY CHALLENGE" : "YOU NEED TO BE FASTER... F A S T E R!!!");
                m_strGoldRecordFlavourText = "AN ABSOLUTE LEGEND";
                m_strSilverRecordFlavourText = "A SPEEDY SOUL";
                m_strBronzeRecordFlavourText = "A QUICK INDIVIDUAL";
                break;
            }
            case GameState.GameplayMode.Hoarder:
            {
                m_strNoRecordFlavourText = (_mainMenu ? "READY TO HOARD?" : "WHAT KIND OF HOARDER ARE YOU?!");
                m_strGoldRecordFlavourText = "AN ABSOLUTE HOARDER";
                m_strSilverRecordFlavourText = "A COLLECTOR";
                m_strBronzeRecordFlavourText = "A SIMPLE GATHERER";
                break;
            }
            case GameState.GameplayMode.Rush:
            {
                m_strNoRecordFlavourText = (_mainMenu ? "TO THE TEMPLE WE GO!" : "RUSH FASTER");
                m_strGoldRecordFlavourText = "THE RUSHIEST OF ALL RUSHERS";
                m_strSilverRecordFlavourText = "AN HONOURABLE RUSHER";
                m_strBronzeRecordFlavourText = "A DECENT RUSHER";
                break;
            }
            case GameState.GameplayMode.MapHunt:
            {
                m_strNoRecordFlavourText = (_mainMenu ? "WHO NEEDS CRYSTALS? GO MAPS!" : "OI! THATS WAY TOO MANY CRYSTALS!");
                m_strGoldRecordFlavourText = "MAP CONNOISSEUR";
                m_strSilverRecordFlavourText = "MAP COLLECTOR";
                m_strBronzeRecordFlavourText = "GOTTA LOVE MAPS A BIT MORE";
                break;
            }
            default:
                break;
        }
        string determinedFlavourText = "";

        //Determines which flavour text should be displayed based on the placement
        if (_currentPlace == 0)
            determinedFlavourText = m_strNoRecordFlavourText;
        else if (_currentPlace == 1)
            determinedFlavourText = m_strBronzeRecordFlavourText;
        else if (_currentPlace == 2)
            determinedFlavourText = m_strSilverRecordFlavourText;
        else if (_currentPlace == 3)
            determinedFlavourText = m_strGoldRecordFlavourText;

        return determinedFlavourText;
    }

    static public int CheckCurrentPlace(GameState.GameplayMode _currentMode, int _currentTime)
    {
        int currentPlace = 0;
        SetCurrentGameModeSelected(_currentMode);

        //Determine the current trophy placement with the given time
        for (int i = 0; i < 3; ++i)
        {
            if (_currentTime > currentRecordCheck[i])
            {
                currentPlace = i; //0 or 1 or 2//none or bronze or silver
                return currentPlace;
            }
            if (_currentTime < currentRecordCheck[2])
            {
                currentPlace = 3;
            }
        }
        return currentPlace;
    }
    
    static public void SetCurrentGameModeSelected(GameState.GameplayMode _currentMode)
    {
        switch (_currentMode)
        {
            case GameState.GameplayMode.SpeedRun:
            {
            currentRecordCheck = m_SpeedRunRecords_TimeAttack;
            break;
            }
            case GameState.GameplayMode.Hoarder:
            {
                currentRecordCheck = m_SpeedRunRecords_Hoarder;
                break;
            }
            case GameState.GameplayMode.Rush:
            {
                currentRecordCheck = m_SpeedRunRecords_Rush;
                break;
            }
            case GameState.GameplayMode.MapHunt:
            {
                currentRecordCheck = m_SpeedRunRecords_MapRun;
                break;
            }
            default:
            break;
        }
    }

    static public string GetGoalScore(GameState.GameplayMode _mode, int _place)
    {
        SetCurrentGameModeSelected(_mode);
        if (_mode == GameState.GameplayMode.MapHunt)
        {
            return currentRecordCheck[_place - 1].ToString();
        }
        //Following same array logic- but -1 as 0 isnt counted. 
        //  1 = bronze, 2 = silver, 3 = gold
        return ConvertRecordToString(currentRecordCheck[_place-1]); 
    }

    /// <summary>
    /// Converts stored records into strings. Not reusable.
    /// </summary>
    /// <param name="_record">record retrieved from score array</param>
    /// <returns>a formatted string version of goal (incl colons)</returns>
    static public string ConvertRecordToString(int _record)
    {
        string recordStr = _record.ToString();
        string recordMins = "";
        string recordSecs = "";

        if (recordStr.Length == 3)
        {
            recordMins = '0' + recordStr[0].ToString();
        }
        else if (recordStr.Length == 4)
        {
            recordMins = recordStr[0].ToString() + recordStr[1].ToString();
        }
        recordSecs += recordStr[recordStr.Length - 2].ToString() + recordStr[recordStr.Length - 1].ToString();

        recordStr = "00:" + recordMins + ":" + recordSecs;

        return recordStr;
    }


    /// <summary>
    /// Retrieves the string values to access PlayerPrefs for 'current place' and 'best time' based on the given mode.
    /// </summary>
    /// <param name="_mode"> Mode to retrieve place and time values for</param>
    /// <param name="_playerBestPlace">The player's best place value</param>
    /// <param name="_playerBestStrTime">The player's Best Time value</param>
    static public void PlayerPrefModeRetriever(GameState.GameplayMode _mode, ref string _playerBestPlace, ref string _playerBestStrTime, ref string _playerBestIntTime)
    {
        switch (_mode)
        {
            case GameState.GameplayMode.SpeedRun:
            {
                _playerBestPlace = "PP_TimeAttackCurrentPlace";
                _playerBestStrTime = "PP_TimeAttackTimeString";
                _playerBestIntTime = "PP_TimeAttackTimeInt";
                break;
            }
            case GameState.GameplayMode.Hoarder:
            {
                _playerBestPlace = "PP_HoarderCurrentPlace";
                _playerBestStrTime = "PP_HoarderTimeString";
                _playerBestIntTime = "PP_HoarderTimeInt";
                break;
            }
            case GameState.GameplayMode.Rush:
            {
                _playerBestPlace = "PP_RushCurrentPlace";
                _playerBestStrTime = "PP_RushTimeString";
                _playerBestIntTime = "PP_RushTimeInt";
                break;
            }
            case GameState.GameplayMode.MapHunt:
            {
                _playerBestPlace = "PP_MapHuntCurrentPlace";
                _playerBestStrTime = "PP_MapHuntTimeString";
                _playerBestIntTime = "PP_MapHuntTimeInt";
                break;
            }
            default:
            {
                _playerBestPlace = "NODATA";
                _playerBestStrTime = "NODATA";
                _playerBestStrTime = "NODATA";
                break;
            }
        }
    }
}