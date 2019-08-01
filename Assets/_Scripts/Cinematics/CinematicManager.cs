using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MessageSystem;

public class CinematicManager : MonoBehaviour
{
    GameObject[] m_rChildren;

    private void Start()
    {
        //Get all zone cinematics in child
        var components = GetComponentsInChildren<CinematicZone>();
        m_rChildren = new GameObject[components.Length];

        for (int i = 0; i <= components.Length; ++i)
        {
            m_rChildren [i]= components[i].gameObject;
        }

        ActivateCinematics(true);
    }

    public void ActivateCinematics(bool _activate)
    {
        //Activate or Deactivate all cinematics in scene
        foreach (GameObject cinematic in m_rChildren)
        {
            cinematic.SetActive(_activate);
        }
    }

    public void ActivateCinematicByID(int _ID)
    {
        //Activate a cinematic by ID
        foreach (GameObject cinematic in m_rChildren)
        {
            if(cinematic.GetComponent<CinematicZone>().GetCinematicID() == _ID)
            {
                cinematic.SetActive(true);
            }
        }
    }
}
