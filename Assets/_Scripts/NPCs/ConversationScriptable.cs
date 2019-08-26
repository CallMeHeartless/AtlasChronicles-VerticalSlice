using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Line
{
    public CharacterScriptable m_character;
    [TextArea(2, 5)]
    public string m_strText;
}

[CreateAssetMenu(fileName = "New Conversation", menuName = "Conversation")]
public class ConversationScriptable : ScriptableObject
{
    public Line[] m_lines;
}
