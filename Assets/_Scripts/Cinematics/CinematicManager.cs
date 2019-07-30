using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MessageSystem;

public class CinematicManager : MonoBehaviour, IMessageReceiver
{
    GameObject[] m_rChildren;

    private void Start()
    {
        m_rChildren = GameObject.FindGameObjectsWithTag("Cinematic");
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

    public void OnReceiveMessage(MessageType _message, object _source)
    {
        switch (_message)
        {
            case MessageType.eActivate:
            {
                    
                break;
            }
            case MessageType.eReset:
            {

                break;
            }
            default:
                break;
        }
    }
}
