using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWander : AIState
{
    private AIWanderProperties m_rWanderProperties;
    private float m_fWanderIntervalTimer = 0.0f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // Reset wander interval timer
        m_fWanderIntervalTimer = 0.0f;
        m_rWanderProperties = m_rAI.m_rWanderProperties;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        m_fWanderIntervalTimer += Time.deltaTime;
        if(m_fWanderIntervalTimer >= m_rWanderProperties.m_fWanderInterval) {
            FindNewPosition();
            m_fWanderIntervalTimer = 0.0f;
        }

        // Determine which animation should be played
        if (m_rAgent.velocity.sqrMagnitude == 0.0f) {
            m_rAI.animator.SetTrigger("Idle");
        } else {
            m_rAI.animator.SetTrigger("Patrol");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!m_rAI.isKnockedOut) {
            m_rAI.animator.SetTrigger("SpotPlayer");
        }
    }

    private void FindNewPosition() {
        float fRandomAngle = Random.Range(0.0f, 360.0f);
        float fRandomRadius = Random.Range(m_rWanderProperties.m_fMinWanderRadius, m_rWanderProperties.m_fMaxWanderRadius);
        Vector3 target = Quaternion.AngleAxis(fRandomAngle, Vector3.up) * Vector3.right * fRandomRadius;
        target += m_rWanderProperties.m_HomePosition;
        m_rAI.SetDestination(target);
    }

}
