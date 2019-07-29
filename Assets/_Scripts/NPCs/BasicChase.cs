using System.Collections;
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

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!m_rPlayerReference) {
            m_rPlayerReference = GameObject.Find("Player").GetComponent<PlayerController>();
            m_fAttackCooldown = m_rAI.m_fAttackCooldown;
        }
        if (!m_rAI.isKnockedOut){
            // Play this animation when the Goon starts chasing the player
            m_rAI.animator.SetTrigger("SpotPlayer");
        }
        m_rAgent.stoppingDistance = m_fAttackRange;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!GameState.DoesPlayerHaveControl()) return;
        if (PlayerInAttackRange()) {
            m_rAgent.isStopped = true; // Stop the agent from moving while attacking
            // Attack
            if(m_fAttackTimer >= m_fAttackCooldown) {
                m_rAI.Attack();
                m_fAttackTimer = 0.0f;
            } 
        }
        else {
           // m_rAgent.isStopped = false;
            m_PlayerPosition = m_rPlayerReference.transform.position;
            m_rAI.SetDestination(m_PlayerPosition);
        }

        m_fAttackTimer += Time.deltaTime;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        m_rAgent.stoppingDistance = 0.0f;
    }

    // Returns true if the player is in the defined attack range
    private bool PlayerInAttackRange() {
        return (m_rAI.transform.position - m_rPlayerReference.transform.position).sqrMagnitude <= m_fAttackRange * m_fAttackRange;
    }
}
