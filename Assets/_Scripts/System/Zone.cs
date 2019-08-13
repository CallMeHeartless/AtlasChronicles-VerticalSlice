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
    private List<MapVisionComponent> m_MapVisionComponents = null; // A list of all objects within the zone that can be shown through map vision

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
        if (m_MapVisionComponents == null) {
            m_MapVisionComponents = new List<MapVisionComponent>();
        }
    }

    //Checks if the zones passed through param matches this objects zone ID
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

    //Returns zone ID
    public int GetZoneID()
    {
        return m_iZoneID;
    }

    //Returns a list of zones
    public static List<Zone> GetZoneList()
    {
        if(s_Zones != null)
        {
            return s_Zones;
        }
        else
        {
            Debug.Log("NONEXISTENT ?");
            return null;
        }
    }

    // Sets the state of all map vision objects within the zone
    public void SetMapVisionState(bool _bOn) {

        foreach(MapVisionComponent mapVision in m_MapVisionComponents) {
            if (mapVision) { // Check that the reference is good
                mapVision.ToggleMapVision(_bOn); // Set the material to be on/off
            }
        }
    }

    // Toggles map vision for all zones with collected map fragments
    public static void ToggleMapVision(bool _bOn) {
        foreach(Zone zone in s_Zones) {
            if (zone.GetIsMapFragmentCollected()) {
                zone.SetMapVisionState(_bOn);
            }
        }
    }

    // Allow a map vision component to add itself to the zone's list. Children should call this at runtime
    public void AddToMapVisionList(MapVisionComponent _rMapVisionObject) {
        m_MapVisionComponents.Add(_rMapVisionObject);
    }

    // Allow a map vision component to remove itself from the zone's list. Called when the child object is being destroyed.
    public void RemoveFromMapVisionList(MapVisionComponent _rMapVisionObject) {
        m_MapVisionComponents.Remove(_rMapVisionObject);
    }
}
