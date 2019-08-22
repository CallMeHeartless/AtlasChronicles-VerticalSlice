using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Button[] m_rMapRegions;    // Array of map region buttons
    [SerializeField] private Button[] m_rMapNumRegions; // Array of map number buttons

    [SerializeField] TextMeshProUGUI m_rMapDetailTitle;     // The details title text
    [SerializeField] TextMeshProUGUI m_rMapDetailMapCount;       // The gameobject containing the image UI and text to display a map count
    [SerializeField] TextMeshProUGUI m_rMapDetailChestCount;     // The gameobject containing the image UI and text to display a chest count
    [SerializeField] TextMeshProUGUI m_rMapDetailCollectableCount;   // The gameobject containing the image UI and text to display a collectable count
    [SerializeField] GameObject m_rMapDetailContainer;       // The gameobject for the container holding all map details
    [SerializeField] GameObject m_rMapContainer;            // The gameobject for the container holding all maps
    private bool[] m_bRegionCollected;

    private Zone[] m_rZones;    //Zone array from the Zone script.
    private int m_rMapsCollected = 0;

    [SerializeField] RectTransform m_rMapDefaultPosition;
    [SerializeField] RectTransform m_rMapDestinationPosition;
    private Vector3 m_vecDefaultMapPos = Vector3.zero;
    private Vector3 m_vecDestinationMapPos = Vector3.zero;

    void Start() {
        RetrieveZones();
        m_vecDefaultMapPos = m_rMapDefaultPosition.position;
        m_vecDestinationMapPos = m_rMapDestinationPosition.position;
        //Set a default view for the map details UI
        //Reveal all map images
        HideAllMapUIZones(false);

        //Disable all zones
        DisableAllZones();
    }

    /// <summary>
    /// Disable both map buttons and mapnumber buttons on start
    /// </summary>
    public void DisableAllZones()
    {
        foreach (Button item in m_rMapRegions)
        {
            item.interactable = false;
        }

        foreach (Button item in m_rMapNumRegions)
        {
            item.interactable = false;
        }
    }

    /// <summary>
    /// Retrieve zones/update zone list from the static Zone class
    /// </summary>
    public void RetrieveZones() {
        //Get zones from the static Zone class
        List<Zone> zoneList = Zone.GetZoneList();

        //If map regions exist AND zones exist in the world
        if (m_rMapRegions != null && zoneList != null) {
            m_rZones = new Zone[zoneList.Count];
            m_bRegionCollected = new bool[zoneList.Count];

            //Populate arrays with values from zone
            foreach (Zone zone in zoneList) {
                int i = zone.GetZoneID();
                if(i > zoneList.Count) {
                    print("ZONE ERROR: A Zone GameObject is missing Zone script.");
                }
                else {
                    m_rZones[i - 1] = zone;
                }
            }
        }
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>    
    public void OpenMap() {
        if (m_rZones == null) {
            RetrieveZones();
        }

        //Update collection data onto map
        m_rMapsCollected = 0;

        //Reveal all map images
        HideAllMapUIZones(false);

        //For each zone, check if the map has been collected.
        for (int i = 0; i < m_rZones.Length; ++i) {
            if (m_rZones[i].GetIsMapFragmentCollected())
            {
                //m_bRegionCollected[i] = true;
                //Check if regions are interactable/collected
                m_rMapRegions[i].interactable = true;
                m_rMapNumRegions[i].interactable = true;

                ++m_rMapsCollected;
            }
            else
            {
                m_bRegionCollected[i] = false;
            }
        }
        MapDefaultSettings();
    }

    public void SortEnabledMapRegions()
    {
        for (int i = 0; i < m_rMapRegions.Length; ++i)
        {
            if(m_bRegionCollected[i])
            {
                m_rMapRegions[i].interactable = true;
                m_rMapNumRegions[i].interactable = true;
            }
        }
    }
    
    /// <summary>
    /// Hide/dont hide all the UI zone buttons from the map
    /// </summary>
    public void HideAllMapUIZones(bool _hide) {
        for (int i = 0; i < m_rMapRegions.Length; ++i) {
            m_rMapRegions[i].gameObject.SetActive(!_hide);
        }
    }

    /// <summary>
    /// Set the map to only display the number of maps collected
    /// </summary>
    public void MapDefaultSettings() {
        //Reveal all map images
        HideAllMapUIZones(false);

        //reset map position
        m_rMapContainer.GetComponent<RectTransform>().transform.position = m_vecDefaultMapPos;
        m_rMapDetailContainer.SetActive(false);

        // Set the default details panel to show the amount of regions that have currently been mapped.
        //m_rMapDetailRegionCount.SetActive(true);    //Counter of how many regions have been 
        //m_rMapDetailRegionCount.GetComponentInChildren<TextMeshProUGUI>().text = m_rMapsCollected + " / 5";

        //Hide all region details
        //m_rMapDetailMapCount.SetActive(false);
        //m_rMapDetailChestCount.SetActive(false);
        //m_rMapDetailCollectableCount.SetActive(false);
    }

    /// <summary>
    /// Activates the gameobjects in the details panel 
    /// </summary>
    public void ActivateDetails() {
        //Called everytime a map region button is clicked
        m_rMapContainer.GetComponent<RectTransform>().transform.position = m_vecDestinationMapPos;

        m_rMapDetailContainer.SetActive(true);

        //Counter of how many regions have been 
        //m_rMapDetailRegionCount.SetActive(false);    

        //Turn on all region details
        //m_rMapDetailMapCount.SetActive(true);
        //m_rMapDetailChestCount.SetActive(true);
        //m_rMapDetailCollectableCount.SetActive(true);
    }

    /// <summary>
    /// Activates and updates the details of the zone that was clicked
    /// </summary>
    /// <param name="_num">The region number to set details of</param>
    public void SetRegionSelected(int _num) {
        //Change title text to 'region #'
        m_rMapDetailTitle.text = "Region " + _num;

        int currentRegion = _num - 1; //To account for array counting

        //Set Zone details into UI text
        m_rMapDetailMapCount.GetComponentInChildren<TextMeshProUGUI>().text =
            (m_rZones[currentRegion].GetIsMapFragmentCollected() ? "1" : "0") + "/1";
        m_rMapDetailChestCount.GetComponentInChildren<TextMeshProUGUI>().text = 
            m_rZones[currentRegion].GetCurrentChestCount() + "/" + m_rZones[currentRegion].GetTotalChestCount();
        m_rMapDetailCollectableCount.GetComponentInChildren<TextMeshProUGUI>().text = 
            m_rZones[currentRegion].GetCurrentCollectableCount() + "/" + m_rZones[currentRegion].GetTotalCollectableCount();
    }
}


/***
 * Case - no maps collected
 *  - All maps are disabled.
 *  - All map nums are disabled
 *  - back to exit
 *  
 *  Case - 1 map collected
 *  - All maps except collected is disabled
 *  - Enable
 *      - if zone == collected, set enabled, else disable
 *  
 *  Case - Zone clicked
 *  - If zone clicked, Set mode to viewing map
 *  - Shift map left, display details right
 *  - If back clicked and in viewing mode, return to all mode
 *  
 *  Case - All mode clicked
 *  - Set to not viewing map
 *  - If back clicked, return to menu
 *  
 * */