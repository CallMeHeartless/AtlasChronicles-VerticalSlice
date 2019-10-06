using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Records : MonoBehaviour
{
    //changing m_SpeedRunRecords changes that runs speed modes trophie freeshwhole
    static private int[] m_SpeedRunRecords_SpeedRun = new int[3] { 2500, 1700, 1000 };// min min,second second
    static private int[] m_SpeedRunRecords_AllItems = new int[3] { 2500, 1700, 1000 };// min min,second second
    static private int[] m_SpeedRunRecords_Rush = new int[3] { 2500, 1700, 1000 };// min min,second second
    static private int[] m_SpeedRunRecords_MapRun = new int[3] { 2500, 1700, 1000 };// min min,second second
    static public int m_CurrentPlace =3;

    /*___________________________________________________
  * Job: degrade the current rank trophie
  * Ceratior: Nicholas
  ______________________________________________________*/
    static public bool check(int CurrentTime,GameState.GameplayMode mode)
    {
       
        if ((CurrentTime<= 0)||( m_CurrentPlace < 1))
        {
            return false;
        }
       // Debug.Log(mode);
        switch (mode)
        {
            case GameState.GameplayMode.Adventure:
                break;
            case GameState.GameplayMode.SpeedRun:

                if (CurrentTime == m_SpeedRunRecords_SpeedRun[m_CurrentPlace-1])
                {

                    m_CurrentPlace--;
                    return true;
                }
                break;
            case GameState.GameplayMode.Everything:
 
                if (CurrentTime == m_SpeedRunRecords_AllItems[m_CurrentPlace-1])
                {
                    m_CurrentPlace--;
                    return true;
                }
                break;
            case GameState.GameplayMode.Rush:

                if (CurrentTime == m_SpeedRunRecords_Rush[m_CurrentPlace - 1])
                {
                    m_CurrentPlace--;
                    return true;
                }
                break;
            case GameState.GameplayMode.ForTheMaps:

                if (CurrentTime == m_SpeedRunRecords_MapRun[m_CurrentPlace - 1])
                {
                    m_CurrentPlace--;
                    return true;
                }
                break;
            default:
                break;
        }
        return false;
    }
}