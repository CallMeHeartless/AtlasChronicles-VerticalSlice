using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MessageSystem;

public class SwitchController : MonoBehaviour, IMessageReceiver
{
    [SerializeField]
    private bool m_bTriggeredOnce = true;

    private DamageController m_rDamageController;
    public List<GameObject> OpenThisDoor;
    public List<GameObject> TurnOnOrOf;
    //[SerializeField]
    public List<MonoBehaviour> m_ObjectsToMessage;

    [SerializeField] private bool m_bDescendObject = false;
    [SerializeField] private GameObject m_rObjectToDescend;
    [SerializeField] private float m_rDescendDistance;
    private float m_fInitYPos = 0.0f;
    private bool m_bDescending = false;

    [SerializeField] private Material m_rGlowMat;

    // Start is called before the first frame update
    void Start()
    {
        if(m_rObjectToDescend)
        {
            m_fInitYPos = m_rObjectToDescend.transform.position.y;
        }
        m_rDamageController = GetComponent<DamageController>();
    }

    private void Update()
    {
        if (!m_bDescendObject || !m_bDescending || !m_rObjectToDescend)
            return;

        print("Pos: " + m_rObjectToDescend.transform.position.y);
        print("Dest: " + (m_fInitYPos - m_rDescendDistance));

        if (m_rObjectToDescend.transform.position.y > m_fInitYPos - m_rDescendDistance)
        {
            m_rObjectToDescend.transform.position = new Vector3(m_rObjectToDescend.transform.position.x, m_rObjectToDescend.transform.position.y - (0.5f *Time.deltaTime), m_rObjectToDescend.transform.position.z);
        }
        else
        {
            m_bDescending = false;
            if(m_rGlowMat)
            {
                MeshRenderer renderer = GetComponent<MeshRenderer>();
                renderer.material = m_rGlowMat;
            }
        }
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

    public void DescendObject()
    {
        if (m_bDescendObject)
        {
            m_bDescending = true;
        }
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
    public void switchoff()
    {
        for (int i = 0; i < OpenThisDoor.Count; ++i)
        {
            OpenThisDoor[i].GetComponent<Door>().SwitchChanged(0, true);
        }
        for (int i = 0; i < TurnOnOrOf.Count; i++)
        {
            if (TurnOnOrOf[i].activeSelf)
            {
                TurnOnOrOf[i].SetActive(false);
            }
            else
            {
                TurnOnOrOf[i].SetActive(true);
            }
           
        }
    }
}
