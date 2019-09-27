using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    // Boolean controls
    private static bool s_bIsPaused = false;
    private static bool s_bInCinematic = false;
    private static bool s_bIsPlayerTeleporting = false;
    private static bool s_bTimerTings = false;

    public enum SpeedRunMode
    {
        Explore,//standend amount of crysials and map
        SpeedRun,//standend amount of crysials and map with a count up timer 
        Everything,//all maps and cystals required
        Finished,//this is used when the speed on is over and all mode should be place above this
        ForTheMaps, //all maps low gems and out
        ToTheTop// get to the top of the temple
    }

    private static SpeedRunMode SpeedRunning = SpeedRunMode.Explore;
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
        SpeedRunning = _SpeedRunState;
    }
    public static SpeedRunMode GetSpeedRunning()
    {
       return SpeedRunning;
    }

    public static void SetTimerFlag(bool _Timer)
    {
        s_bTimerTings = _Timer;
    }
    public static bool GetTimer()
    {
        return s_bTimerTings;
    }
}
