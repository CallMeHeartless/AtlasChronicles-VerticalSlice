﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    static List<Zone> s_Zones = null;

    [Header("Properties")]
    [SerializeField][Tooltip("This ID is used to differentiate zones and must be unique. It should match the map fragment assigned to it.")]
    private int m_iZoneID = 0;
    private bool m_bMapFragmentCollected = false;
    private int m_iTotalCollectableCount = 0;
    private List<MapVisionComponent> m_MapVisionComponents = null; // A list of all objects within the zone that can be shown through map vision
    private List<GameObject> m_Chests = null; // A list of map fragments in zone
    private List<GameObject> m_Collectables = null; // A list of crystals in zone

    private List<LeylineController> m_LeylineComponents = null;    // A list of all leylines in the zone

    // Generate a list of zones for future efficiency
    private void Awake()
    {
        // Create new list if it does not exist
        if (s_Zones == null) {
            s_Zones = new List<Zone>();
        }

        // Add this zone list
        s_Zones.Add(this);

        // Ensure component/gameobject lists exist
        if (m_MapVisionComponents == null) {
            m_MapVisionComponents = new List<MapVisionComponent>();
        }

        if (m_Chests == null) {
            m_Chests = new List<GameObject>();
        }

        if (m_Collectables == null) {
            m_Collectables = new List<GameObject>();
		}
		
        if(m_LeylineComponents == null) {
            m_LeylineComponents = new List<LeylineController>();
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

        // Enable all leylines
        foreach(LeylineController leyline in m_LeylineComponents) {
            leyline.gameObject.SetActive(true);
            leyline.CheckForAutoActivation();
        }
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
                //Debug.Log("Collected map for zone " + _uiFragmentID);
            }
        }
    }

    //Returns zone ID
    public int GetZoneID() {
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
            Debug.Log("NONEXISTENT ZONE?");
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

    // Toggles map vision for all zones with collected map fragments // Legacy
    public static void ToggleMapVision(bool _bOn) {
        foreach(Zone zone in s_Zones) {
            if (zone.GetIsMapFragmentCollected()) {
                zone.SetMapVisionState(_bOn);
            }
        }
    }

    // Add a map vision component to the zones list
    public void AddToMapVisionList(MapVisionComponent _rMapVisionObject)
    {
        m_MapVisionComponents.Add(_rMapVisionObject);
    }

    // Allow a map vision component to remove itself from the zone's list. Called when the child object is being destroyed.
    public void RemoveFromMapVisionList(MapVisionComponent _rMapVisionObject) {
        m_MapVisionComponents.Remove(_rMapVisionObject);
    }

    // Allow a map vision component to add itself to the zone's list. Children should call this at runtime
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_rZoneObject"></param>
    public void AddToZone(GameObject _rZoneObject)
    {
        switch (_rZoneObject.tag)
        {
            case "Box":
            {
                m_Chests.Add(_rZoneObject);
                break;
            }
            case "SecondaryPickup":
            {
                m_Collectables.Add(_rZoneObject);
                break;
            }
            default:
            {
                break;
            }
        }
    }

    /// <summary>
    /// Get the total number of chests from the zone
    /// </summary>
    /// <returns>Total num chests</returns>
    /// <author>Vivian</author>
    public int GetTotalChestCount() {
        return m_Chests.Count;
    }

    /// <summary>
    /// Get the total number of collectables from the zone
    /// </summary>
    /// <returns>Total collectables</returns>
    /// <author>Vivian</author>
    public int GetTotalCollectableCount() {
        return m_Collectables.Count;
    }

    /// <summary>
    /// Get the current amount of collected collectables from the zone
    /// </summary>
    /// <returns>Current num collectables</returns>
    /// <author>Vivian</author>
    public int GetCurrentCollectableCount() {
        int collectableCount = 0;
        foreach (GameObject item in m_Collectables) {
            if (item.GetComponent<Pickup>().GetCollected()) {
                ++collectableCount;
            }
        }
        return collectableCount;
    }

    /// <summary>
    /// Get the current amount of chests found from the zone
    /// </summary>
    /// <returns>Current num chests</returns>
    /// <author>Vivian</author>
    public int GetCurrentChestCount() {
        int chestCount = 0;
        foreach (GameObject item in m_Chests) {
            if(item.GetComponent<BreakableObject>().GetIsBroken()) {
                ++chestCount;
            }
        }
        return chestCount;
    }

    /// <summary>
    /// Increase the total amount of collectables in a zone.
    /// Function used by BreakableObject (Chest script) as crystals 
    ///     are not initialised when they havent yet spawn from a chest yet
    /// </summary>
    /// <param name="_num">Number of collectables to inc the total by</param>
    /// <author>Vivian</author>
    public void IncreaseCollectableCount(int _num) {
        m_iTotalCollectableCount += _num;
    }
    /// <summary>
    /// Registers a leyline with the zone
    /// </summary>
    /// <param name="_leyline"></param>
    public void AddToLeylineList(LeylineController _leyline) {
        m_LeylineComponents.Add(_leyline);
    }

    public static void ClearZones()
    {
        s_Zones = new List<Zone>();
    }
}
