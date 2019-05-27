using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats {
    public static int[] s_iMapsBoard = new int[] { 0, 0, 0, 0};//4
    public static int[] s_iNoteBoard = new int[] { 0, 0, 0, 0};//4
    public static int s_iLevelIndex = 0;
    
    // Steals a map fragment. Returns false if unable to do so
    public static bool StealMapFragment() {
        if(s_iMapsBoard[s_iLevelIndex] <= 0) {
            return false;
        }
        --s_iMapsBoard[s_iLevelIndex];
        return true;
    }
    // Serialisation functions go here

}
