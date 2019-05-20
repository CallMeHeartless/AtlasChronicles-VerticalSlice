using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField]
    private int m_iDamage = 1;
    [SerializeField]
    private ParticleSystem m_rHitParticles;
    [SerializeField]
    private LayerMask m_HitLayer;
    private Collider m_HitBox;

    // Start is called before the first frame update
    void Start()
    {
        m_HitBox = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
