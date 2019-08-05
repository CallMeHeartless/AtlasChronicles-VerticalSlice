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
        m_rAI.animator.SetTrigger("Idle");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        m_fWanderIntervalTimer += Time.deltaTime;
        // Check if it's time to find a new wander position
        if(m_fWanderIntervalTimer >= m_rWanderProperties.m_fWanderInterval) {
            FindNewPosition(); // Obtain new position
            m_fWanderIntervalTimer = 0.0f; // Reset timer
            m_rAgent.isStopped = false;
        }

        // Determine which animation should be played
        if (m_rAgent.velocity.sqrMagnitude == 0.0f) {
            //m_rAI.animator.SetTrigger("Idle");
            m_rAI.animator.SetBool("bIsWandering", false);
        } else {
            //m_rAI.animator.SetTrigger("Patrol");
            m_rAI.animator.SetBool("bIsWandering", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //if (!m_rAI.isKnockedOut) {
        //    m_rAI.animator.SetTrigger("SpotPlayer");
        //}
    }

    // Obtains a new destination for the Goon within their wander properties
    private void FindNewPosition() {
        float fRandomAngle = Random.Range(0.0f, 360.0f); // Angle within the circle
        float fRandomRadius = Random.Range(m_rWanderProperties.m_fMinWanderRadius, m_rWanderProperties.m_fMaxWanderRadius); // Random radius
        Vector3 target = Quaternion.AngleAxis(fRandomAngle, Vector3.up) * Vector3.right * fRandomRadius;
        target += m_rWanderProperties.m_HomePosition; // Add vector to home position to find offset point
        m_rAI.SetDestination(target);
    }

}