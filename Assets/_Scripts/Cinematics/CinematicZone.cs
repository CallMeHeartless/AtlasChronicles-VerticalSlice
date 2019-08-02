using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using MessageSystem;

public class CinematicZone : MonoBehaviour
{
    [Header("Cinematic ID")]
    [SerializeField] int m_iCinematicID = 0;

    [Header("Cinematic Settings")]
    [SerializeField] bool m_bPlayOnce = false;

    // Note: Hide when finished as reference
    [SerializeField] bool m_bEventTriggered = false;
    private bool m_bExecutedOnce = false;
    private PlayableDirector m_rDirector;
    private PlayerController m_rPlayer;
    public UnityEvent OnEnter;

    private void Start()
    {
        m_rDirector = GetComponent<PlayableDirector>();
    }

    public void Test()
    {
        // Function called and used to test if sections of code are working
        print("Test");
    }

    private void Update()
    {
        //If there is no event happening or the director does not exist, return.
        if (!m_bEventTriggered || !m_rDirector)
            return;

        if (m_rDirector.state == PlayState.Playing)
        {
            //Do something when director is playing
        }
        else
        {
            //When cinematic event ends, set relevant variables to false
            m_bEventTriggered = false;
            GameState.SetCinematicFlag(false);

            if (m_rPlayer)
            {
                m_rPlayer.SetCineGroundCheckTrue();
                m_rPlayer.Invoke("SetCineGroundCheckFalse", 0.1f);
            }
        }
    }

    public int GetCinematicID()
    {
        return m_iCinematicID;
    }

    private void OnTriggerEnter(Collider other)
    {
        // When player enters the cinematic zone, the event is yet to be triggered
        //                          Also check if user wants to play the event once
        // Note: m_bEventTriggered is necessary to prevent the player colliding twice 
        //                          in a row (capsule collider + CharacterController) 
        if (!m_bEventTriggered && !m_bExecutedOnce)
        {
            PlayCinematic(other.gameObject);
        }
    }

    public void PlayCinematic(GameObject _other)
    {
        if (_other.CompareTag("Player") && !m_bEventTriggered && !m_bExecutedOnce)
        {
            m_rPlayer = _other.GetComponent<PlayerController>();

            if (m_bPlayOnce)
            {
                m_bExecutedOnce = true;
            }

            m_bEventTriggered = true;
            // If director exists, play cinematic and set relevant variables true
            if (m_rDirector)
            {
                m_rDirector.enabled = true;
                m_rPlayer.GetAnimator().SetBool("Grounded", true);
                m_rPlayer.GetAnimator().SetTrigger("Idle");
                GameState.SetCinematicFlag(true);
                m_rDirector.Play();
            }
            //Invoke any customized events
            OnEnter.Invoke();
        }
    }

    public void PauseCinematic(bool _pause)
    {
        if(_pause)
        {
            m_rDirector.Pause();
        }
        else
        {
            m_rDirector.Resume();
        }
    }
}
