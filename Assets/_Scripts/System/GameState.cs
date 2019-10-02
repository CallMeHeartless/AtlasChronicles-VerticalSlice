﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    // Boolean controls
    private static bool s_bIsPaused = false;
    private static bool s_bInCinematic = false;
    private static bool s_bIsPlayerTeleporting = false;
    private static bool s_bTimerTings = false;
    private static AsyncOperation s_asyncLoad;

    public enum SpeedRunMode
    {
        Adventure,//standend amount of crysials and map
        SpeedRun,//standend amount of crysials and map with a count up timer 
        Everything,//all maps and cystals required
        Finished,//this is used when the speed on is over and all mode should be place above this
        ForTheMaps, //all maps low gems and out
        ToTheTop// get to the top of the temple
    }

    private static SpeedRunMode m_eSpeedRunning = SpeedRunMode.Adventure;
  // private static bool SpeedRunning = false;
    // Toggles the pause flag
    public static void SetPauseFlag(bool _bState) {
        s_bIsPaused = _bState;
    }
    public static bool GetPauseFlag()
    {
        return s_bIsPaused;
    }
    public static bool GetCinematicFlag()
    {
        return s_bInCinematic;
    }

    public static AsyncOperation GetAsync()
    {
        return s_asyncLoad;
    }

    // Toggles the cinematic flag
    public static void SetCinematicFlag(bool _bState) {
        s_bInCinematic = _bState;
    }

    // Toggles the player control flag
    public static void SetPlayerTeleportingFlag(bool _bState) {
        s_bIsPlayerTeleporting = _bState;
    }

    public static bool DoesPlayerHaveControl() {
        return !(s_bIsPaused || s_bInCinematic || s_bIsPlayerTeleporting||s_bTimerTings); // Add here accordingly
    }
    public static void SetSpeedRunning(SpeedRunMode _SpeedRunState)
    {
        m_eSpeedRunning = _SpeedRunState;
    }
    public static SpeedRunMode GetIsSpeedRunMode()
    {
       return m_eSpeedRunning;
    }

    public static void SetTimerFlag(bool _Timer)
    {
        s_bTimerTings = _Timer;
    }
    public static bool GetTimer()
    {
        return s_bTimerTings;
    }
    //all the thing stoping the play move set bacm to false
    public static void SetPlayerFree()
    {
       s_bIsPaused = false;
       s_bInCinematic = false;
       s_bIsPlayerTeleporting = false;
       s_bTimerTings = false;
    }

    public static IEnumerator LoadingScene(int _sceneIndex)
    {
        s_asyncLoad = SceneManager.LoadSceneAsync(_sceneIndex);
        //asyncOperation.allowSceneActivation = false;

        // Wait until the asynchronous scene fully loads
        while (!s_asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public static SpeedRunMode GetSpeedRunning() {
        return m_eSpeedRunning;
    }
    
}
