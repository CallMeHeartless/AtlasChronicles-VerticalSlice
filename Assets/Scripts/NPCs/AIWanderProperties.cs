using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AIWanderProperties{
    [HideInInspector]
    public Vector3 m_HomePosition;
    public float m_fMinWanderRadius = 1.0f;
    public float m_fMaxWanderRadius = 1.0f;
    public float m_fWanderInterval = 3.0f;
    
}
