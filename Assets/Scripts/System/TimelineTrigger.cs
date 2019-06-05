using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

[RequireComponent(typeof(Collider))]
public class TimelineTrigger : MonoBehaviour
{
    public enum TriggerType
    {
        Once, Everytime,
    }

    [Tooltip("This is the gameobject which will trigger the director to play.  For example, the player.")]
    public PlayableDirector director;
    public TriggerType triggerType;
    public UnityEvent OnDirectorPlay;
    public UnityEvent OnDirectorFinish;

    protected bool m_AlreadyTriggered;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (triggerType == TriggerType.Once && m_AlreadyTriggered)
            return;

        OnDirectorPlay.Invoke();
        GameState.SetCinematicFlag(true);
        director.Play();
        m_AlreadyTriggered = true;
        Invoke("FinishInvoke", (float)director.duration);
    }

    void FinishInvoke()
    {
        OnDirectorFinish.Invoke();
        GameState.SetCinematicFlag(false);
    }
}
