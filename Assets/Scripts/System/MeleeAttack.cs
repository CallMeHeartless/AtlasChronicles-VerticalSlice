using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MeleeAttack : MonoBehaviour
{
    [SerializeField]
    private int m_iDamage = 1;
    [SerializeField]
    private ParticleSystem m_rHitParticles;
    [SerializeField]
    private LayerMask m_HitLayer;
    private Collider m_HitBox;
    [SerializeField]
    private string m_strTargetTag;
    [HideInInspector]
    public bool m_bIsActive = false;

    // Start is called before the first frame update
    void Start()
    {
        m_HitBox = GetComponent<Collider>();
    }

    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(m_strTargetTag) && m_bIsActive) {
            // Damage shit
            DamageController target = other.GetComponent<DamageController>();
            if (target) {
                DamageMessage message = new DamageMessage();
                message.damage = m_iDamage;
                message.source = gameObject;

                target.ApplyDamage(message);
                m_bIsActive = false; // Prevent multiple collisions
            }
        }
    }


}
