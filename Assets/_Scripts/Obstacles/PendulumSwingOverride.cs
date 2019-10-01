using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumSwingOverride : MonoBehaviour {
    [Range(0.0f, 1.0f)]
    public float m_fBlendOverride = 1.0f;

    private Animator m_rAnimator;

    // Start is called before the first frame update
    void Start() {
        m_rAnimator = GetComponent<Animator>();
        if (m_rAnimator) {
            m_rAnimator.SetFloat("Blend", m_fBlendOverride);
        }
    }


}