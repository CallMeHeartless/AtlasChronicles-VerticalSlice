using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class QuadLookAt : MonoBehaviour
{
    private Camera m_rCameraReference;
    private SpriteRenderer m_rBubble;
    private Animator m_rAnimator;
    [SerializeField] Sprite m_rControllerBubble;
    [SerializeField] Sprite m_rKeyBubble;

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
            m_rBubble.sprite = ((InputManager.s_bInputIsController) ? m_rControllerBubble : m_rKeyBubble);

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
