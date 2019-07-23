using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapManager : MonoBehaviour
{
    [SerializeField] GameObject[] m_rMapRegions;

    [SerializeField] TextMeshProUGUI m_rMapDetailTitle;
    [SerializeField] GameObject m_rMapDetailRegionCount;
    [SerializeField] GameObject m_rMapDetailMapCount;
    [SerializeField] GameObject m_rMapDetailChestCount;
    [SerializeField] GameObject m_rMapDetailCollectableCount;

    private Zone[] m_rZones;
    private bool[] m_rZoneMapCollected;
    private int m_rMapsCollected = 0;

    // Start is called before the first frame update
    void Start()
    {
        List<Zone> zoneList = Zone.GetZoneList();
        m_rZones = new Zone[zoneList.Count];
        m_rZoneMapCollected = new bool[zoneList.Count];

        //Populate arrays with values from zone
        foreach (Zone zone in zoneList)
        {
            int i = zone.GetZoneID();
            m_rZones[i - 1] = zone;
            m_rZoneMapCollected[i - 1] = m_rZones[i - 1].GetIsMapFragmentCollected();
        }

        for (int i = 0; i < m_rMapRegions.Length; ++i)
        {
            m_rMapRegions[i].SetActive(false);
        }

        MapDefaultSettings();
    }

    // Update is called once per frame
    public void OpenMap()
    {
        m_rMapsCollected = 0;
        for (int i = 0; i < m_rZones.Length; ++i)
        {
            m_rZoneMapCollected[i] = m_rZones[i].GetIsMapFragmentCollected();
            if(m_rZoneMapCollected[i])
            {
                m_rMapRegions[i].SetActive(true);
                ++m_rMapsCollected;
            }
        }
        MapDefaultSettings();
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

    public void TurnOffDefaultSettings()
    {
        m_rMapDetailRegionCount.SetActive(false);    //Counter of how many regions have been 

        //Turn on all region details
        m_rMapDetailRegionCount.SetActive(true);
        m_rMapDetailMapCount.SetActive(true);
        m_rMapDetailChestCount.SetActive(true);
        m_rMapDetailCollectableCount.SetActive(true);
    }

    public void SetRegionSelected(int _num)
    {
        m_rMapDetailTitle.text = "REGION " + _num;  //Change title text to 'region #'
    }
}
