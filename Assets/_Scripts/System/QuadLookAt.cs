using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadLookAt : MonoBehaviour
{
    private Camera m_rCameraReference;
    private SpriteRenderer m_rBubble;
    private Animator m_rAnimator;

    // Start is called before the first frame update
    void Start()
    {
        m_rCameraReference = GameObject.Find("Camera").GetComponent<Camera>();
        m_rBubble = GetComponentInChildren<SpriteRenderer>();
        m_rAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Only look at cam if enabled
        if(m_rBubble.enabled)
        {
            transform.LookAt(transform.position + m_rCameraReference.transform.rotation * Vector3.forward,
                                m_rCameraReference.transform.rotation * Vector3.up);
        }
    }

    private void OnDisable()
    {
        if(m_rBubble)
            m_rBubble.enabled = false;
    }

    private void OnEnable()
    {
        if (m_rBubble)
            m_rBubble.enabled = true;
    }
}
