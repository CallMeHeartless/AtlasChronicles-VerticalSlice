using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Records : MonoBehaviour
{
    static private int[] m_SpeedRunRecords_AllItems = new int[4] { 25, 20, 15, 10 };// min min,second second
    static public int m_CurrentPlace =3;

    static public bool check(int CurrentTime,GameState.SpeedRunMode mode)
    {
       
        if (CurrentTime<= 0)
        {
            return false;
        }
       // Debug.Log(mode);
        switch (mode)
        {
            case GameState.SpeedRunMode.Expore:
                break;
            case GameState.SpeedRunMode.SpeedRun:
                break;
            case GameState.SpeedRunMode.EveryThing:
                if (m_CurrentPlace < 0)
                {
                    return false;
                }

                if (CurrentTime == m_SpeedRunRecords_AllItems[m_CurrentPlace])
                {
                   
                    m_CurrentPlace--;
                    Debug.Log("hit Everythingh");
                    return true;
                }
                break;
            case GameState.SpeedRunMode.Finished:
                break;
            default:
                break;
        }
        return false;
    }
}