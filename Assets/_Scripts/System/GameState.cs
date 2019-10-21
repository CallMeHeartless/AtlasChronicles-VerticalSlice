using System.Collections;
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
    private static bool s_bGetIsSpeedRunning = false;
    private static bool s_bMainMenuAccessed = false;
    private static bool s_bFirstTimeGameAccessed = false;
    private static AsyncOperation s_asyncLoad;

    public enum GameplayMode
    {
        Adventure,//standend amount of crysials and map
        SpeedRun,//standend amount of crysials and map with a count up timer 
        Everything,//all maps and cystals required
        Rush,//this is used when the speed on is over and all mode should be place above this
        ForTheMaps, //all maps low gems and out 
    }

    private static GameplayMode m_eGameplayMode = GameplayMode.Adventure;
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
    public static void SetGameplayMode(GameplayMode _SpeedRunState)
    {
        m_eGameplayMode = _SpeedRunState;
    }
    public static GameplayMode GetGameplayMode()
    {
       return m_eGameplayMode;
    }

    public static bool GetIsSpeedRunning()
    {
        return s_bGetIsSpeedRunning;
    }

    public static void SetIsSpeedRunning()
    {
        s_bGetIsSpeedRunning = (m_eGameplayMode == GameplayMode.SpeedRun ? true : false);
    }

    public static void SetTimerFlag(bool _Timer)
    {
        s_bTimerTings = _Timer;
    }

    public static bool GetTimer()
    {
        return s_bTimerTings;
    }

    public static void SetMainMenuAccessed(bool _accessed)
    {
        if(s_bFirstTimeGameAccessed)
        {
            s_bMainMenuAccessed = false;
        }
        else
        {
            s_bMainMenuAccessed = _accessed;
        }
    }

    public static bool GetMainMenuAccessed()
    {
        return s_bMainMenuAccessed;
    }

    public static void SetFirstTimeGameAccessed(bool _accessed)
    {
        s_bFirstTimeGameAccessed = _accessed;
    }

    public static bool GetFirstTimeGameAccessed()
    {
        return s_bFirstTimeGameAccessed;
    }

    //all the thing stoping the play move set bacm to false
    public static void SetPlayerFree()
    {
       s_bIsPaused = false;
       s_bInCinematic = false;
       s_bIsPlayerTeleporting = false;
       s_bTimerTings = false;
    }

    public static IEnumerator LoadScene(int _sceneIndex)
    {
        s_asyncLoad = SceneManager.LoadSceneAsync(_sceneIndex);
        //asyncOperation.allowSceneActivation = false;

        // Wait until the asynchronous scene fully loads
        while (!s_asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
