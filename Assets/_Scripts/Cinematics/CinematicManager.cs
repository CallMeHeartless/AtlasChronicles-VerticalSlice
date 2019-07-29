using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MessageSystem;

public class CinematicManager : MonoBehaviour, IMessageReceiver
{
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
