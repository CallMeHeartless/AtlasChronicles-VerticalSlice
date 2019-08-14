using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapManager : MonoBehaviour
{
    [SerializeField] GameObject[] m_rMapRegions;    // Array of map region buttons

    [SerializeField] TextMeshProUGUI m_rMapDetailTitle;     // The details title text
    [SerializeField] GameObject m_rMapDetailRegionCount;    // The region count that is only displayed in default mode (when no region is selected)
    [SerializeField] GameObject m_rMapDetailMapCount;       // The gameobject containing the image UI and text to display a map count
    [SerializeField] GameObject m_rMapDetailChestCount;     // The gameobject containing the image UI and text to display a chest count
    [SerializeField] GameObject m_rMapDetailCollectableCount;   // The gameobject containing the image UI and text to display a collectable count

    private Zone[] m_rZones;    //Zone array from the Zone script.
    private int m_rMapsCollected = 0;

    // Start is called before the first frame update
    void Start()
    {
        RetrieveZones();
        //Set all map regions inactive
        HideAllMapUIZones();

        //Set a default view for the map details UI
        MapDefaultSettings();
    }

    public void RetrieveZones() {
        List<Zone> zoneList = Zone.GetZoneList();

        if (m_rMapRegions != null && m_rZones != null)
        {
            m_rZones = new Zone[zoneList.Count];

            //Populate arrays with values from zone
            foreach (Zone zone in zoneList)
            {
                int i = zone.GetZoneID();
                m_rZones[i - 1] = zone;
            }
        }
    }

    // Update is called once per frame
    public void OpenMap()
    {
        if (m_rZones == null && m_rZones.Length != 0)
        {
            RetrieveZones();
        }

        //Update collection data onto map
        m_rMapsCollected = 0;

        //For each zone, check if the map has been collected.
        for (int i = 0; i < m_rZones.Length; ++i)
        {
            if(m_rZones[i].GetIsMapFragmentCollected())
            {
                m_rMapRegions[i].SetActive(true);
                ++m_rMapsCollected;
            }
        }

        //Update the UI to display details
        MapDefaultSettings();
    }

    public void HideAllMapUIZones()
    {
        //Hide all button sections of the map
        m_rMapsCollected = 0;
        for (int i = 0; i < m_rMapRegions.Length; ++i)
        {
            m_rMapRegions[i].SetActive(false);
        }
    }

    public void MapDefaultSettings()
    {
        // Set the default details panel to show the amount of regions that have currently been mapped.
        m_rMapDetailTitle.text = "REGIONS MAPPED";  //Change title text to 'regions mapped' instead of 'region #'
        m_rMapDetailRegionCount.SetActive(true);    //Counter of how many regions have been 
        m_rMapDetailRegionCount.GetComponentInChildren<TextMeshProUGUI>().text = m_rMapsCollected + " / 5";

        //Hide all region details
        m_rMapDetailMapCount.SetActive(false);
        m_rMapDetailChestCount.SetActive(false);
        m_rMapDetailCollectableCount.SetActive(false);
    }

    public void ActivateDetails()
    {
        //Counter of how many regions have been 
        m_rMapDetailRegionCount.SetActive(false);    

        //Turn on all region details
        m_rMapDetailMapCount.SetActive(true);
        m_rMapDetailChestCount.SetActive(true);
        m_rMapDetailCollectableCount.SetActive(true);
    }

    public void SetRegionSelected(int _num)
    {
        //Change title text to 'region #'
        m_rMapDetailTitle.text = "REGION " + _num; 
    }
}
