
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MessageSystem;

public class Piller : MonoBehaviour, IMessageReceiver
{
   public GameObject[] m_gPillersPostion;
    public int currentPostion=0; 
    public float m_fSpeed = 0.01f;
    public float m_fSpeedBoust = 0.0001f;
    public float m_fMinSpeed = 0.01f;
    public float m_fMaxSpeed = 0.1f;
    private bool m_bUnlocked = false;
    private bool m_bMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        m_bMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bMoving)
        {
            //Debug.Log(currentPostion);
                if (Vector3.Distance(transform.position, m_gPillersPostion[currentPostion].transform.position) < .1f)
                {
                    m_bMoving = false;
                }
                transform.position = Vector3.MoveTowards(transform.position, m_gPillersPostion[currentPostion].transform.position, m_fSpeed);
            if (m_fSpeed<= m_fMaxSpeed)
            {
                m_fSpeed += m_fSpeedBoust;
            }

        }
    }

    // Implement Message interface
    public void OnReceiveMessage(MessageType _message, object _source)
    {
        switch (_message)
        {
            // Open the door
            case MessageType.eOn:
                {
                   // m_bUnlocked = true;
                    m_bMoving = true;
                    currentPostion += (int)_source;
                    m_fSpeed = m_fMinSpeed;
                    break;
                }
            // Reset the door
            case MessageType.eOff:
                {
                    m_bMoving = true;
                    currentPostion -= (int)_source;
                    m_fSpeed = m_fMinSpeed;
                    break;
                }

            default: break;
        }
    }
}
