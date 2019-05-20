﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AIWanderProperties{
    [HideInInspector]
    public Vector3 m_HomePosition = Vector3.zero;
    public float m_fMinWanderRadius = 1.0f;
    public float m_fMaxWanderRadius = 1.0f;
    public float m_fWanderInterval = 3.0f;

    // Editor visualisation
#if UNITY_EDITOR
    public void EditorGizmo(Vector3 _Position) {
        if(m_HomePosition == Vector3.zero) {
            m_HomePosition = _Position;
        }
        Color c = new Color(0.0f, 0.7f, 0.0f, 0.4f);
        UnityEditor.Handles.color = c;
        UnityEditor.Handles.DrawSolidArc(m_HomePosition, Vector3.up, Vector3.right, 360.0f, m_fMaxWanderRadius);
    }

#endif
}
