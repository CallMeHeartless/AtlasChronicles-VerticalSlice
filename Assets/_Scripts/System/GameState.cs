using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    // Boolean controls
    private static bool s_bIsPaused = false;
    private static bool s_bInCinematic = false;
    private static bool s_bIsPlayerTeleporting = false;

    public enum SpeedRunMode
    {
        Expore,//standend amount of crysials and map
        SpeedRun,//standend amount of crysials and map with a count up timer 
        EveryThing,//all maps and cystals required
        Finished,//this is used when the speed on is over and all mode should be place above this
        ForTheMaps, //all maps low gems and out
        ToTheTop// get to the top of the temple
    }

    private static SpeedRunMode SpeedRunning = SpeedRunMode.Expore;
  // private static bool SpeedRunning = false;
    // Toggles the pause flag
    public static void SetPauseFlag(bool _bState) {
        s_bIsPaused = _bState;
    }
    public static bool GetPauseFlag()
    {
        return s_bIsPaused;
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
        return !(s_bIsPaused || s_bInCinematic || s_bIsPlayerTeleporting); // Add here accordingly
    }
    public static void SetSpeedRunning(SpeedRunMode _SpeedRunState)
    {
        SpeedRunning = _SpeedRunState;
    }
    public static SpeedRunMode GetSpeedRunning()
    {
       return SpeedRunning;
    }
}
