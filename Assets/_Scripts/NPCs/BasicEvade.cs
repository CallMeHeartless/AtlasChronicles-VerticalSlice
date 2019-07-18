using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEvade : AIState
{
    private GameObject m_rPlayer;
    private AIWanderProperties m_rWanderProperties;
    //private NavMeshAgent m_rAgent;
    private Vector3 m_PreviousPlayerPosition = Vector3.zero;
    private Vector3 m_PlayerVelocity = Vector3.zero;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // Obtain reference to the player
        if (!m_rPlayer) {
            m_rPlayer = GameObject.Find("Player");
        }
        // Obtain reference to wander properties (for determining home position)
        if (m_rWanderProperties == null) {
            m_rWanderProperties = m_rAI.m_rWanderProperties;
        }
        if (!m_rAgent) {
            m_rAgent = m_rAI.GetComponent<NavMeshAgent>();
        }
    }
  
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // Return home if holding a map fragment, then evade the player once there
        if (IsAtHome()) {
            EvadePlayer();
        }
        else {
            m_rAgent.SetDestination(m_rWanderProperties.m_HomePosition);            
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

    }

    // Returns true if the agent is within its home radius
    private bool IsAtHome() {
        return (m_rAI.transform.position - m_rWanderProperties.m_HomePosition).sqrMagnitude <= m_rWanderProperties.m_fMaxWanderRadius * m_rWanderProperties.m_fMaxWanderRadius;
    }

    // Moves away from the player, whilst remaining within its home base
    private void EvadePlayer() {
        // Basic Evade
        UpdatePlayerVelocity();
        Vector3 expectedPosition = m_rPlayer.transform.position + m_PlayerVelocity;
        // Obtain directional vector for AI
        Vector3 playerToAI = (m_rAI.transform.position - expectedPosition).normalized;
        // Obtain target
        Vector3 aiTarget = m_rAI.transform.position + playerToAI;
        // Restrain to home position
        Vector3 homeToTarget = aiTarget -  m_rWanderProperties.m_HomePosition;
        if(homeToTarget.sqrMagnitude > m_rWanderProperties.m_fMaxWanderRadius * m_rWanderProperties.m_fMaxWanderRadius) {
            aiTarget = homeToTarget.normalized * m_rWanderProperties.m_fMaxWanderRadius + m_rWanderProperties.m_HomePosition;
        }
        m_rAgent.SetDestination(aiTarget);
    }

    private void UpdatePlayerVelocity() {
        m_PlayerVelocity = (m_rPlayer.transform.position - m_PreviousPlayerPosition);
        m_PreviousPlayerPosition = m_rPlayer.transform.position;
    }
}
