using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEvade : AIState
{
    private GameObject m_rPlayer;
    private AIWanderProperties m_rWanderProperties;
    private NavMeshAgent m_rAgent;

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
        // Basic flee
        Vector3 playerToAI = (m_rAI.transform.position - m_rPlayer.transform.position).normalized;
        m_rAgent.SetDestination(m_rWanderProperties.m_HomePosition + m_rWanderProperties.m_fMaxWanderRadius * playerToAI);
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
