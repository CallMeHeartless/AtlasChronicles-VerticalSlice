using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    static List<Zone> s_Zones = null;

    [Header("Properties")]
    [SerializeField][Tooltip("This ID is used to differentiate zones and must be unique. It should match the map fragment assigned to it.")]
    private int m_iZoneID = 0;
    private bool m_bMapFragmentCollected = false;
    private int m_iCollectableCount = 0;
    private int m_iChestCount = 0;

    // Generate a list of zones for future efficiency
    private void Awake()
    {
        // Create new list if it does not exist
        if (s_Zones == null)
        {
            s_Zones = new List<Zone>();
        }

        // Add this zone list
        s_Zones.Add(this);
    }

    public bool DoesIDMatchZone(int _uiID) {
        return _uiID == m_iZoneID;
    }

    // Checks if the map fragment has been taken
    public bool GetIsMapFragmentCollected() {
        return m_bMapFragmentCollected;
    }

    // Set whether the zone's map fragment has been collected or not (will almost always be set to true, but you never know)
    public void SetMapFragmentStatus(bool _bState) {
        m_bMapFragmentCollected = _bState;
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

    public int GetZoneID()
    {
        return m_iZoneID;
    }

    public static List<Zone> GetZoneList()
    {
        return s_Zones;
    }
}
