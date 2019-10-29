using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Records : MonoBehaviour
{
    //changing m_SpeedRunRecords changes that runs speed modes trophie freeshwhole
    static private int[] m_SpeedRunRecords_SpeedRun = new int[3] { 2500, 1700, 1000 };// min min,second second
    static private int[] m_SpeedRunRecords_AllItems = new int[3] { 2500, 1700, 1000 };// min min,second second
    static private int[] m_SpeedRunRecords_Rush = new int[3] { 0250, 0130, 0100 };// min min,second second
    static private int[] m_SpeedRunRecords_MapRun = new int[3] { 150, 100, 50 };// min min,second second
    static public int m_CurrentPlace = 3;

    static public string m_strNoRecordFlavourText = "BETTER LUCK NEXT TIME";

    static public string m_strGoldRecordFlavourText = "--INVALID GOLD TEXT--";
    static public string m_strSilverRecordFlavourText = "--INVALID SILVER TEXT--";
    static public string m_strBronzeRecordFlavourText = "--INVALID BRONZE TEXT--";

    /*___________________________________________________
  * Job: degrade the current rank trophie
  * Ceratior: Nicholas
  ______________________________________________________*/
    static public int CheckCurrentPlace(bool _mainMenu, int _currentTime, GameState.GameplayMode _currentMode, ref string _placementText)
    {
        if (_currentTime<= 0 || _currentMode == GameState.GameplayMode.Adventure)
        {
            return 0;
        }

        int[] currentRecordCheck = m_SpeedRunRecords_SpeedRun;

        switch (_currentMode)
        {
            case GameState.GameplayMode.SpeedRun:
            {
                currentRecordCheck = m_SpeedRunRecords_SpeedRun;
                m_strNoRecordFlavourText = (_mainMenu ? "TAKE ON A SPEEDY CHALLENGE" : "BETTER LUCK NEXT TIME!");
                m_strGoldRecordFlavourText = "YOU ARE LEGENDARY";
                m_strSilverRecordFlavourText = "AWESOME";
                m_strBronzeRecordFlavourText = "GOOD JOB";
                break;
            }
            case GameState.GameplayMode.Hoarder:
            {
                currentRecordCheck = m_SpeedRunRecords_AllItems;
                m_strNoRecordFlavourText = (_mainMenu ? "READY TO HOARD?" : "YOU DIDN'T HOARD ENOUGH!");
                m_strGoldRecordFlavourText = "YOU ARE LEGENDARY";
                m_strSilverRecordFlavourText = "AWESOME";
                m_strBronzeRecordFlavourText = "GOOD JOB";
                break;
            }
            case GameState.GameplayMode.Rush:
            {
                currentRecordCheck = m_SpeedRunRecords_Rush;
                m_strNoRecordFlavourText = (_mainMenu ? "TO THE TEMPLE WE GO!" : "NOT FAST ENOUGH!");
                m_strGoldRecordFlavourText = "THE RUSHIEST OF ALL RUSHERS";
                m_strSilverRecordFlavourText = "YOU'RE GETTING THERE!";
                m_strBronzeRecordFlavourText = "YOU JUST MADE IT!";
                break;
            }
            case GameState.GameplayMode.MapHunt:
            {
                currentRecordCheck = m_SpeedRunRecords_MapRun;
                m_strNoRecordFlavourText = (_mainMenu ? "WHO NEEDS CRYSTALS? GO MAPS!" : "EUGH! THATS WAY TOO MANY CRYSTALS!");
                m_strGoldRecordFlavourText = "A MAP-ONLY LOVER";
                m_strSilverRecordFlavourText = "GOTTA LOVE MAPS A BIT MORE";
                m_strBronzeRecordFlavourText = "GOTTA LOVE MAPS A BIT MORE";
                break;
            }
            default:
                break;
        }

        //Determine the current trophy placement with the given time
        for (int i = 0; i < 3; ++i)
        {
            if (_currentTime >= currentRecordCheck[i])
            {
                m_CurrentPlace = i;
                
                return 1;
            }
        }
        return 0;
    }

    /// <summary>
    /// Retrieves the string values to access PlayerPrefs for 'current place' and 'best time' based on the given mode.
    /// </summary>
    /// <param name="_mode"> Mode to retrieve place and time values for</param>
    /// <param name="_playerBestPlace">The player's best place value</param>
    /// <param name="_playerbestTime">The player's Best Time value</param>
    static public void PlayerPrefModeRetriever(GameState.GameplayMode _mode, ref string _playerBestPlace, ref string _playerbestTime)
    {
        switch (_mode)
        {
            case GameState.GameplayMode.SpeedRun:
            {
                _playerBestPlace = "PP_TimeAttackCurrentPlace";
                _playerbestTime = "PP_TimeAttackTimeString";
                break;
            }
            case GameState.GameplayMode.Hoarder:
            {
                _playerBestPlace = "PP_HoarderCurrentPlace";
                _playerbestTime = "PP_HoarderTimeString";
                break;
            }
            case GameState.GameplayMode.Rush:
            {
                _playerBestPlace = "PP_RushCurrentPlace";
                _playerbestTime = "PP_RushTimeString";
                break;
            }
            case GameState.GameplayMode.MapHunt:
            {
                _playerBestPlace = "PP_MapHuntCurrentPlace";
                _playerbestTime = "PP_MapHuntTimeString";
                break;
            }
            default:
                _playerBestPlace = "NODATA";
                _playerbestTime = "NODATA";
                break;
        }
    }
}