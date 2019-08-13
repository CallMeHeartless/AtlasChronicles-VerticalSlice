using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MessageSystem;

public class CinematicManager : MonoBehaviour
{
    static CinematicZone[] m_rChildren;
    static private GameObject m_rPlayer;
    static int m_iPausedCine = -1;
    //Stack<int> m_rCineStack = new Stack<int>();

    private void Start()
    {
        //Get all zone cinematics in child
        GameObject[] cineZone = GameObject.FindGameObjectsWithTag("Cinematic");
        m_rChildren = new CinematicZone[cineZone.Length];
        for (int i = 0; i < cineZone.Length; i++)
        {
            m_rChildren[i] = cineZone[i].GetComponent<CinematicZone>();
        }

        GameObject.FindGameObjectsWithTag("Cinematic");
        m_rPlayer = GameObject.FindGameObjectWithTag("Player");

        ActivateCinematics(true);
    }

    static public void ActivateCinematics(bool _activate)
    {
        //Activate or Deactivate all cinematics in scene
        foreach (CinematicZone cinematic in m_rChildren)
        {
            cinematic.gameObject.SetActive(_activate);
        }
    }

    static public void ActivateCinematicByID(int _ID)
    {
        CinematicZone zone = FindCinematicByID(_ID);

        //Activate a cinematic by ID
        if(zone)
        {
            zone.PlayCinematic(m_rPlayer);
        }
        else
        {
            Debug.Log("Cinematic Zone does not exist");
        }
    }

    static public int GetActiveCinematic()
    {
        if(m_rChildren == null) {
            return -1;
        }
        foreach (CinematicZone cinematic in m_rChildren)
        {
            if(cinematic.GetDirector().playableGraph.IsValid())
            {
                if (cinematic.GetDirector().playableGraph.IsPlaying())
                {
                    return cinematic.GetCinematicID();
                }
            }
        }
        return -1;
    }

    static public void PauseCinematicByID(bool _pause, int _ID)
    {
        CinematicZone zone = FindCinematicByID(_ID);

        //Pause a cinematic by ID
        if (zone)
        {
            if(_pause)
            {
                zone.PauseCinematic(true);
            }
            else
            {
                zone.PauseCinematic(false);
            }
        }
        else
        {
            Debug.Log("Cinematic Zone does not exist");
        }
    }

    static public void PauseCinematics(bool _pause)
    {
        if(_pause)
        {
            m_iPausedCine = GetActiveCinematic();
        }

        if (m_iPausedCine == -1)
        {
            return;
        }
        PauseCinematicByID(_pause, m_iPausedCine);
    }

    static public CinematicZone FindCinematicByID(int _ID)
    {
        // Null reference check
        if(m_rChildren == null) {
            return null;
        }
        //Activate a cinematic by ID
        foreach (CinematicZone cinematic in m_rChildren)
        {
            if (cinematic.GetCinematicID() == _ID)
            {
                return cinematic;
            }
        }
        return null;
    }
}
