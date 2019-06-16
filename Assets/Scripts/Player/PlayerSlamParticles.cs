using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlamParticles : StateMachineBehaviour
{
    private ParticleSystem m_rParticles;
    private CharacterController m_rCharacterController;
    private bool m_bHasLanded = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!m_rParticles) {
            m_rParticles = GameObject.Find("VFX_GroundSlam").GetComponent<ParticleSystem>();
        }
        if (!m_rCharacterController) {
            m_rCharacterController = GameObject.Find("Player").GetComponent<CharacterController>();
        }
        m_bHasLanded = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if(m_rCharacterController.isGrounded && !m_bHasLanded) {
            m_bHasLanded = true;
            m_rParticles.Play();
            //m_rCharacterController.GetComponent<PlayerController>().SlamAttackReset();
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //}
}
