using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivator : MonoBehaviour
{
    [SerializeField] Dialogue m_conversation;

    public string GetDialogueName()
    {
        return m_conversation.m_strName;
    }

    public void TriggerDialogue()
    {
        Debug.Log("Dialogue activated: " + m_conversation.m_strName);
        FindObjectOfType<DialogueManager>().StartDialogue(m_conversation);
    }
}
