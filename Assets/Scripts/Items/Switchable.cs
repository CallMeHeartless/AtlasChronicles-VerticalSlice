﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Switchable : MonoBehaviour
{
    [SerializeField]
    private bool m_bChangeMaterialOnTag = false;
    [SerializeField]
    private Material m_AlternateMaterial;
    [SerializeField]
    private Material m_Material;
    private Transform m_OriginalTransform;
    private Vector3 m_StartPosition;
    private EnemyController m_Enemy = null;

    private void Start() {
        //m_Material = GetComponent<MeshRenderer>().material;
        m_OriginalTransform = transform;
        m_StartPosition = transform.position;
        m_Enemy = GetComponent<EnemyController>();
    }

    // Called to indicated the switchable object has been tagged
    public void Tag() {
        if (m_bChangeMaterialOnTag) {
            GetComponentInChildren<MeshRenderer>().material = m_AlternateMaterial;
        }
        // Check if this is an enemy
        if (m_Enemy) {
            m_Enemy.SetTag(true);
        }
    }

    // Called to indicate that the switchable object has had a tag removed
    public void DeTag() {
        if (m_bChangeMaterialOnTag) {
            GetComponentInChildren<MeshRenderer>().material = m_Material;
        }
        if (m_Enemy) {
            m_Enemy.SetTag(false);
        }
    }

    // Returns the switchable object to its original position
    public void ReturnToStartPosition() {
        if (GetComponent<NavMeshAgent>()) return;
        transform.position = m_StartPosition;
        transform.rotation = m_OriginalTransform.rotation;

        // Check for rigidbody
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (rigidbody) {
            rigidbody.velocity = Vector3.zero;
        }
        
    }

    public static void ResetAllPositions(){
        Switchable[] switchables = GameObject.FindObjectsOfType<Switchable>();
        foreach(Switchable switchable in switchables){
            switchable.ReturnToStartPosition();
        }
    }
}
