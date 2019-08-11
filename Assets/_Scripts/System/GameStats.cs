using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats {

    public static int[] s_iMapsTotal = new int[] { 0, 0, 0, 0 };//4
    public static int[] s_iCollectableTotal= new int[] { 0, 0, 0, 0 };//4

    public static int[] s_iMapsBoard = new int[] { 0, 0, 0, 0};//4
    public static int[] s_iCollectableBoard = new int[] { 0, 0, 0, 0};//4
    public static int s_iLevelIndex = 0;

    // Ease of access variables
    public static int s_iSecondaryCollected { get { return s_iCollectableBoard[s_iLevelIndex]; } }
    public static int s_iSecondaryTotal { get { return s_iCollectableTotal[s_iLevelIndex]; } }

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
