using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMenuInteraction : MonoBehaviour
{
    Animator m_rAnimator;

    // Start is called before the first frame update
    void Start()
    {
        m_rAnimator = GetComponent<Animator>();
        m_rAnimator.SetTrigger("PopOut");
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
