using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivator : MonoBehaviour
{
    [SerializeField] Dialogue m_conversation;
    private DialogueManager m_manager;

    private void Start()
    {
        m_manager = FindObjectOfType<DialogueManager>();
    }

    public string GetDialogueName()
    {
        return m_conversation.m_strName;
    }

    public void TriggerDialogue()
    {
        Debug.Log("Dialogue activated: " + m_conversation.m_strName);
        m_manager.StartDialogue(m_conversation);
    }

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
