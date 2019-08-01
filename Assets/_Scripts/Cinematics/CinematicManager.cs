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
        //var components = GetComponentsInChildren<CinematicZone>();
        //m_rChildren = new GameObject[components.Length];

        //for (int i = 0; i <= components.Length; ++i)
        //{
        //    m_rChildren [i]= components[i].gameObject;
        //}

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
        //Activate a cinematic by ID
        foreach (GameObject cinematic in m_rChildren)
        {
            if (cinematic.GetComponent<CinematicZone>().GetCinematicID() == _ID)
            {
                cinematic.gameObject.GetComponent<CinematicZone>().PlayCinematic(m_rPlayer);
            }
        }
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
