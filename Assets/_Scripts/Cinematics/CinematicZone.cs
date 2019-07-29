using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using MessageSystem;

public class CinematicZone : MonoBehaviour
{
    public UnityEvent OnEnter;
    public bool eventTriggered = false;
    [SerializeField] PlayableDirector m_director;

    public void Test()
    {
        print("Test");
    }

    private void Update()
    {
        if (!eventTriggered)
            return;

        if (m_director.state == PlayState.Playing)
        {

        }
        else
        {
            eventTriggered = false;
            GameState.SetCinematicFlag(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !eventTriggered)
        {

            eventTriggered = true;
            if(m_director)
            {
                GameState.SetCinematicFlag(true);
                m_director.Play();
            }
            OnEnter.Invoke();
        }
    }
}
