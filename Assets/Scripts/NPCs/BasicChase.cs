﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicChase : AIState
{
    private PlayerController m_rPlayerReference;
    private Vector3 m_PlayerPosition;
    private float m_fAttackRange = 1.75f;
    private float m_fAttackCooldown = 1.0f;
    private float m_fAttackTimer = 0.0f;
    private Animator m_rAnimator = null;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!m_rPlayerReference) {
            m_rPlayerReference = GameObject.Find("Player").GetComponent<PlayerController>();
        }
        if (!m_rAnimator) {
            m_rAnimator = m_rAI.animator;
        }
        m_rAgent.stoppingDistance = m_fAttackRange;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!GameState.DoesPlayerHaveControl()) return;
        if (PlayerInAttackRange()) {
            m_rAgent.isStopped = true;
            // Attack
            if(m_fAttackTimer >= m_fAttackCooldown) {
                m_rAI.Attack();
                m_fAttackTimer = 0.0f;
            } 
        }
        else {
            m_rAgent.isStopped = false;
            m_PlayerPosition = m_rPlayerReference.transform.position;
            m_rAI.SetDestination(m_PlayerPosition);
        }

        m_fAttackTimer += Time.deltaTime;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        m_rAgent.stoppingDistance = 0.0f;
    }

    private bool PlayerInAttackRange() {
        return (m_rAI.transform.position - m_rPlayerReference.transform.position).sqrMagnitude <= m_fAttackRange * m_fAttackRange;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
