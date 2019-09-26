using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Records : MonoBehaviour
{
    //changing m_SpeedRunRecords changes that runs speed modes trophie freeshwhole
    static private int[] m_SpeedRunRecords_SpeedRun = new int[3] { 2500, 1700, 1000 };// min min,second second
    static private int[] m_SpeedRunRecords_AllItems = new int[3] { 2500, 1700, 1000 };// min min,second second
    static private int[] m_SpeedRunRecords_other = new int[3] { 2500, 1700, 1000 };// min min,second second
    static public int m_CurrentPlace =2;

    /*___________________________________________________
  * Job: degrade the current rank trophie
  * Ceratior: Nicholas
  ______________________________________________________*/
    static public bool check(int CurrentTime,GameState.SpeedRunMode mode)
    {
       
        if ((CurrentTime<= 0)||( m_CurrentPlace < 0))
        {
            return false;
        }
       // Debug.Log(mode);
        switch (mode)
        {
            case GameState.SpeedRunMode.Expore:
                break;
            case GameState.SpeedRunMode.SpeedRun:

                if (CurrentTime == m_SpeedRunRecords_SpeedRun[m_CurrentPlace])
                {

                    m_CurrentPlace--;
                    return true;
                }
                break;
            case GameState.SpeedRunMode.EveryThing:
 
                if (CurrentTime == m_SpeedRunRecords_AllItems[m_CurrentPlace])
                {
                   
                    m_CurrentPlace--;
                    Debug.Log("hit Everythingh");
                    return true;
                }
                break;
         
            default:
                break;
        }
        return false;
    }
}