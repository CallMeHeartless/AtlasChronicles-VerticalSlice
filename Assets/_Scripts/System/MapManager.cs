using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Button[] m_rMapRegions;    // Array of map region buttons
    [SerializeField] private Button[] m_rMapNumRegions; // Array of map number buttons
    [SerializeField] private GameObject[] m_rFlavourTexts; // Array of flavour texts
    [SerializeField] private Image[] m_rZonePointers; // Array of flavour texts

    [SerializeField] TextMeshProUGUI m_rMapDetailTitle;         // The details title text
    [SerializeField] TextMeshProUGUI m_rMapDetailMapCount;      // The textObject containing the image UI and text to display a map count
    [SerializeField] TextMeshProUGUI m_rMapDetailChestCount;    // The textObject containing the image UI and text to display a chest count
    [SerializeField] TextMeshProUGUI m_rMapDetailCollectableCount;   // The textObject containing the image UI and text to display a collectable count
    [SerializeField] GameObject m_rMapName;                     // The gameObject containing the name of the map/island
    [SerializeField] GameObject m_rMapDetailContainer;          // The gameobject for the container holding all map details
    [SerializeField] GameObject m_rMapContainer;                // The gameobject for the container holding all maps
    [SerializeField] GameObject m_rMapGameObject;

    private Zone[] m_rZones;    //Zone array from the Zone script.
    private int m_rMapsCollected = 0;
    private bool m_rDisableZones = false;

    [SerializeField] RectTransform m_rMapDefaultPosition;
    [SerializeField] RectTransform m_rMapDestinationPosition;
    private Vector3 m_vecDefaultMapPos = Vector3.zero;
    private Vector3 m_vecDestinationMapPos = Vector3.zero;


    public UnityEvent OnBPressed;

    void Start() {
        RetrieveZones();
        m_vecDefaultMapPos = m_rMapDefaultPosition.position;
        m_vecDestinationMapPos = m_rMapDestinationPosition.position;

        //Zones should not be interactable until collected
        DisableAllZones();

        //Apply default settings to the map
        MapDefaultSettings();
    }

    private void Update()
    {
        if (!m_rMapGameObject.activeSelf)
            return;

        if (Input.GetButtonDown("BButton")) //Back button
        {
            OnBPressed.Invoke(); //Back
        }
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
        m_rDisableZones = ((zoneList == null) ? true : false);

        if (m_rDisableZones)
            return;

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
        if (m_rZones == null && !m_rDisableZones)
        {
            RetrieveZones();
        }

        //Update collection data onto map
        m_rMapsCollected = 0;

        MapDefaultSettings();

        //For each zone, check if the map has been collected.
        for (int i = 0; i < m_rZones.Length; ++i) {
            if (m_rZones[i].GetIsMapFragmentCollected())
            {
                //Check if regions are interactable/collected
                m_rMapRegions[i].interactable = true;
                m_rMapNumRegions[i].interactable = true;

                ++m_rMapsCollected;
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
        if (m_rZones == null && !m_rDisableZones)
        {
            RetrieveZones();
        }

        //Reveal all map images
        HideAllMapUIZones(false);

        //For each zone, check if the map has been collected.
        for (int i = 0; i < m_rZones.Length; ++i)
        {
            if (m_rZones[i].GetIsMapFragmentCollected())
            {
                //Check if regions are interactable/collected
                m_rMapRegions[i].interactable = true;
            }
        }

        //reset map position
        m_rMapContainer.GetComponent<RectTransform>().transform.position = m_vecDefaultMapPos;
        m_rMapDetailContainer.SetActive(false);

        //Deactivate all flavour texts and activate the map name
        SetAllFlavourTextsActive(false);
        m_rMapName.SetActive(true);

        //Deactivate details panel
        m_rMapDetailContainer.SetActive(false);

        //Deactivate all pointers
        SetAllPointersActive(false);

        //Leave All button pointer active
        m_rZonePointers[0].enabled = true;
    }

    /// <summary>
    /// Activate or deactivate all flavour texts
    /// </summary>
    /// <param name="_activate">Bool to activate/deactivate all flavour texts</param>
    public void SetAllFlavourTextsActive(bool _activate)
    {
        foreach (GameObject item in m_rFlavourTexts)
        {
            item.SetActive(_activate);
        }
    }

    /// <summary>
    /// Deactivate all pointers
    /// </summary>
    /// <param name="_activate"></param>
    public void SetAllPointersActive(bool _activate)
    {
        foreach (Image item in m_rZonePointers)
        {
            item.enabled = _activate;
        }
    }

    /// <summary>
    /// Activates and updates the details of the zone that was clicked
    /// </summary>
    /// <param name="_num">The region number to set details of</param>
    public void SetRegionSelected(int _num) {
        //Deactivate all flavour texts and activate the map name
        SetAllFlavourTextsActive(false);

        //Called everytime a map region button is clicked
        m_rMapContainer.GetComponent<RectTransform>().transform.position = m_vecDestinationMapPos;
        m_rMapDetailContainer.SetActive(true);

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


        //Deactivate all regions except for the currently selected region
        for (int i = 0; i < m_rMapRegions.Length; ++i)
        {
            m_rMapRegions[i].interactable = false;
        }

        m_rMapRegions[currentRegion].interactable = true;


        //Activate Flavour text for specified zone
        m_rFlavourTexts[currentRegion].SetActive(true);
        m_rMapName.SetActive(false);

        //Deactivate all pointers
        SetAllPointersActive(false);

        //Set selected zone pointer on. 
        //Using _num instead of currentRegion as the ALL pointer is included in the m_rZonePointers array
        m_rZonePointers[_num].enabled = true;
    }
}