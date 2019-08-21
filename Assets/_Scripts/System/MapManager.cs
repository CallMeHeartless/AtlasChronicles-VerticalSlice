using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private GameObject[] m_rMapRegions;    // Array of map region buttons

    [SerializeField] TextMeshProUGUI m_rMapDetailTitle;     // The details title text
    [SerializeField] GameObject m_rMapDetailRegionCount;    // The region count that is only displayed in default mode (when no region is selected)
    [SerializeField] GameObject m_rMapDetailMapCount;       // The gameobject containing the image UI and text to display a map count
    [SerializeField] GameObject m_rMapDetailChestCount;     // The gameobject containing the image UI and text to display a chest count
    [SerializeField] GameObject m_rMapDetailCollectableCount;   // The gameobject containing the image UI and text to display a collectable count

    private Zone[] m_rZones;    //Zone array from the Zone script.
    private int m_rMapsCollected = 0;    

    void Start() {
        RetrieveZones();
        //Set all map regions inactive
        HideAllMapUIZones();

        //Set a default view for the map details UI
        MapDefaultSettings();
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

        //For each zone, check if the map has been collected.
        for (int i = 0; i < m_rZones.Length; ++i) {
            if(m_rZones[i].GetIsMapFragmentCollected()) {
                m_rMapRegions[i].SetActive(true);
                ++m_rMapsCollected;
            }
        }

        //Update the UI to display details
        MapDefaultSettings();
    }
    
    /// <summary>
    /// Hide all the UI zone buttons from the map
    /// </summary>
    public void HideAllMapUIZones() {
        //Hide all button sections of the map
        for (int i = 0; i < m_rMapRegions.Length; ++i) {
            m_rMapRegions[i].SetActive(false);
        }
    }

    /// <summary>
    /// Set the map to only display the number of maps collected
    /// </summary>
    public void MapDefaultSettings() {
        // Set the default details panel to show the amount of regions that have currently been mapped.
        m_rMapDetailTitle.text = "Regions Mapped";  //Change title text to 'regions mapped' instead of 'region #'
        m_rMapDetailRegionCount.SetActive(true);    //Counter of how many regions have been 
        m_rMapDetailRegionCount.GetComponentInChildren<TextMeshProUGUI>().text = m_rMapsCollected + " / 5";

        //Hide all region details
        m_rMapDetailMapCount.SetActive(false);
        m_rMapDetailChestCount.SetActive(false);
        m_rMapDetailCollectableCount.SetActive(false);
    }

    /// <summary>
    /// Activates the gameobjects in the details panel 
    /// </summary>
    public void ActivateDetails() {
        //Called everytime a map region button is clicked

        //Counter of how many regions have been 
        m_rMapDetailRegionCount.SetActive(false);    

        //Turn on all region details
        m_rMapDetailMapCount.SetActive(true);
        m_rMapDetailChestCount.SetActive(true);
        m_rMapDetailCollectableCount.SetActive(true);
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
