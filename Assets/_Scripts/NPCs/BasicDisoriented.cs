﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDisoriented : AIState
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        m_rAgent.isStopped = true;
        m_rAI.ToggleTagOnHead(true);
        m_rAI.animator.SetBool("Tagged", true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        m_rAgent.isStopped = false;
        m_rAI.ToggleTagOnHead(false);
        m_rAI.animator.SetBool("Tagged", false);
    }

}
