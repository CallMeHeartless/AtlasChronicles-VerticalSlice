﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTagController : MonoBehaviour
{
    [SerializeField]
    private float m_fMoveSpeed = 20.0f;
    private bool m_bIsMoving = false;
    private Transform m_AttachedObject = null;
    [SerializeField]
    private Vector3 m_vecOffset;
    private PlayerController m_PlayerReference;

  

    // Update is called once per frame
    void Update(){
        // Move the tag forward if it has been thrown
        if (m_bIsMoving) {
            transform.Translate(Vector3.forward * m_fMoveSpeed * Time.deltaTime);
        }
    }

    // Instructs the tag to fly forwards or not
    public void SetMoving(bool _bState) {
        m_bIsMoving = _bState;
    }

    void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Switchable>() && !m_AttachedObject) {
            // Attach to the object
            m_AttachedObject = other.transform;
            transform.position = m_AttachedObject.position + m_vecOffset;
            transform.SetParent(m_AttachedObject);
            m_bIsMoving = false;
            m_PlayerReference.SetSwitchTarget(m_AttachedObject.gameObject);
            m_AttachedObject.GetComponent<Switchable>().Tag();
        } else if(!other.CompareTag("Player")){
            // Destroy animation? DO NOT DESTROY OBJECT
            DetachFromObject();
        }
    }

    // Removes the switch tag from the object
    public void DetachFromObject() {
        transform.SetParent(null);
        if (m_AttachedObject) {
            m_AttachedObject.GetComponent<Switchable>().DeTag();
        }
       
        m_AttachedObject = null;
        gameObject.SetActive(false);
    }

    // Places the attached object at the input position, then detaches itself
    public void Switch(Vector3 _vecSwitchPosition) {
        // Visual effects
        m_AttachedObject.position = _vecSwitchPosition;
        DetachFromObject();
    }

    // Manually sets the player controller reference
    public void SetPlayerReference(PlayerController _playerReference) {
        m_PlayerReference = _playerReference;
    }
}
