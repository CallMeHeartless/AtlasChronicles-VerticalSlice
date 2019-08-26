using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class CharacterScriptable : ScriptableObject
{
    public string m_fulName;
    public Sprite m_characterPic;
}
