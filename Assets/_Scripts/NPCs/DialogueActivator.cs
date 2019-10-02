using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Serializable class used to store dialogue texts
/// </summary>
[System.Serializable]
public class Dialogue
{
    public string m_strName;
    [TextArea(1, 3)]
    public string[] m_sentences;
}

/// <summary>
/// A class that activates its dialogue when called, by passing data to the parent Dialogue Manager
/// </summary>
public class DialogueActivator : MonoBehaviour
{
    [SerializeField] Dialogue m_conversation;
    private DialogueManager m_manager;

    private void Start()
    {
        m_manager = transform.parent.GetComponent<DialogueManager>();
    }

    /// <summary>
    /// Returns the dialogue's name
    /// </summary>
    /// <returns></returns>
    public string GetDialogueName()
    {
        return m_conversation.m_strName;
    }

    /// <summary>
    /// Begins dialogue through the Dialogue manager
    /// </summary>
    public void TriggerDialogue()
    {
        //Debug.Log("Dialogue activated: " + m_conversation.m_strName);
        m_manager.StartDialogue(m_conversation);
    }

    /// <summary>
    /// Gets whether the current dialogue is activated
    /// </summary>
    /// <returns></returns>
    public bool GetIsTalking()
    {
        return m_manager.GetIsTalking();
    }

    /// <summary>
    /// Getter to check if player is conversing with NPC
    /// </summary>
    /// <returns></returns>
    public bool GetIsConversing()
    {
        return m_manager.GetIsConversing();
    }
}
