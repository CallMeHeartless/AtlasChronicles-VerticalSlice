using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    static List<Zone> s_Zones = null;

    [Header("Properties")]
    [SerializeField][Tooltip("This ID is used to differentiate zones and must be unique. It should match the map fragment assigned to it.")]
    private int m_iZoneID;
    private bool m_bMapFragmentTaken = false;

    // Generate a list of zones for future efficiency
    void Start(){
        // Create new list if it does not exist
        if (s_Zones == null) {
            s_Zones = new List<Zone>();
        }

        // Add this zone to the list
        s_Zones.Add(this);
    }

    public bool DoesIDMatchZone(int _uiID) {
        return _uiID == m_iZoneID;
    }

    // Checks if the map fragment has been taken
    public bool IsMapFragmentTaken() {
        return m_bMapFragmentTaken;
    }

    // Set whether the zone's map fragment has been collected or not (will almost always be set to true, but you never know)
    public void SetMapFragmentStatus(bool _bState) {
        m_bMapFragmentTaken = _bState;
    }

    // Static function tick off a zone when its map fragment is collected
    public static void CollectMapFragment(int _uiFragmentID) {
        if(s_Zones == null) {
            return;
        }
        // Iteration - we cannot assume the list is ordered
        foreach (Zone zone in s_Zones) {
            // Check if the fragment ID matches the zone ID
            if (zone.DoesIDMatchZone(_uiFragmentID)) {
                zone.SetMapFragmentStatus(true);
                Debug.Log("Collected map for zone " + _uiFragmentID);
            }
        }
    }
}
