using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MessageSystem;

public class SwitchController : MonoBehaviour, IMessageReceiver
{
    [SerializeField]
    private bool m_bTriggeredOnce = true;

    private DamageController m_rDamageController;

    //[SerializeField]
    public List<MonoBehaviour> m_ObjectsToMessage;

    // Start is called before the first frame update
    void Start()
    {
        m_rDamageController = GetComponent<DamageController>();
    }

    void Reset() {
        m_rDamageController.ResetDamage();
    }

    public void Activate() {
        // Send message
        for(int i = 0; i < m_ObjectsToMessage.Count; ++i) {
            IMessageReceiver target = m_ObjectsToMessage[i] as IMessageReceiver;
                target.OnReceiveMessage(MessageType.eActivate, null);
        }
        Debug.Log("Activate Messages sent");
    }

    public void SendResetMessages() {
        // Send message
        for (int i = 0; i < m_ObjectsToMessage.Count; ++i) {
            IMessageReceiver target = m_ObjectsToMessage[i] as IMessageReceiver;
            target.OnReceiveMessage(MessageType.eReset, null);
        }
        //Debug.Log("Reset Messages sent");
    }

    public void OnReceiveMessage(MessageType _eType, object _message) {
        switch (_eType) {
            case MessageType.eReset: {
                if (!m_bTriggeredOnce) {
                    Reset();
                }
                break;
            }
            default: break;
        }
    }
}
