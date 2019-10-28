
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
    public bool m_fActiveOnce = false;
    private bool m_bUnlocked = false;

    private bool m_bMoving = false;
    [SerializeField] private AudioSource m_rAudioPlayer;
    [SerializeField] private AudioClip m_rPillarAudio;
    [SerializeField] private AudioClip m_rPillarReverseAudio;

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
            if (Vector3.Distance(transform.position, m_gPillersPostion[currentPostion].transform.position) < .1f)
            {
                m_bMoving = false;
                PlayAudio(false);
            }
            transform.position = Vector3.MoveTowards(transform.position, m_gPillersPostion[currentPostion].transform.position, m_fSpeed);
            if (m_fSpeed<= m_fMaxSpeed)
            {
                m_fSpeed += m_fSpeedBoust;
            }
        }
    }

    public void ActivatePillar()
    {
        m_bMoving = true;
        currentPostion += 1;
        m_fSpeed = m_fMinSpeed;
        PlayAudio(true, true);
    }

    // Implement Message interface
    public void OnReceiveMessage(MessageType _message, object _source)
    {
        switch (_message)
        {
            // Open the door
            case MessageType.eOn:
                {
                    if (currentPostion == m_gPillersPostion.Length)
                    {

                    }
                    else
                    {
                        m_bMoving = true;
                        currentPostion += (int)_source;
                        m_fSpeed = m_fMinSpeed;
						PlayAudio(true, false);
                    }
                   // m_bUnlocked = true;
                    break;
                }
            // Reset the door
            case MessageType.eOff:
                {
                    if (m_fActiveOnce)
                    {

                    }
                    else
                    {
                        if (m_fActiveOnce == true)
                        {

                        }
                        else
                        {
                            m_bMoving = true;
                            currentPostion -= (int)_source;
                            m_fSpeed = m_fMinSpeed;
                            PlayAudio(true, true);
                        }
                    }
                    break;
                }

            default: break;
        }
    }

    void PlayAudio(bool _play, bool _reverse = false)
    {
        if (!m_rAudioPlayer)
            return;
        if (_play)
        {
            if (_reverse)
            {
                m_rAudioPlayer.clip = m_rPillarReverseAudio;
            }
            else
            {
                m_rAudioPlayer.clip = m_rPillarAudio;
            }
            m_rAudioPlayer.Play();
        }
        else
        {
            m_rAudioPlayer.Stop();
        }
    }
}
