using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MessageSystem;

public class CinematicManager : MonoBehaviour
{
    static GameObject[] m_rChildren;
    static private GameObject m_rPlayer;

    private void Start()
    {
        //Get all zone cinematics in child
        m_rChildren = GameObject.FindGameObjectsWithTag("Cinematic");
        m_rPlayer = GameObject.FindGameObjectWithTag("Player");

        ActivateCinematics(true);
    }

    static public void ActivateCinematics(bool _activate)
    {
        //Activate or Deactivate all cinematics in scene
        foreach (GameObject cinematic in m_rChildren)
        {
            cinematic.SetActive(_activate);
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
            Debug.Log("Zone does not exist");
        }
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
            Debug.Log("Zone does not exist");
        }
    }

    static public CinematicZone FindCinematicByID(int _ID)
    {
        //Activate a cinematic by ID
        foreach (GameObject cinematic in m_rChildren)
        {
            if (cinematic.GetComponent<CinematicZone>().GetCinematicID() == _ID)
            {
                return cinematic.gameObject.GetComponent<CinematicZone>();
            }
        }
        return null;
    }

    //public void OnReceiveMessage(MessageType _message, object _source)
    //{
    //    switch (_message)
    //    {
    //        case MessageType.eActivate:
    //        {

    //            break;
    //        }
    //        case MessageType.eReset:
    //        {

    //            break;
    //        }
    //        default:
    //            break;
    //    }
    //}
}
