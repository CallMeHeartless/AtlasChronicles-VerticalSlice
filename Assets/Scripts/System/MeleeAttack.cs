using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class MeleeAttack : MonoBehaviour
{
    [SerializeField]
    private int m_iDamage = 1;
    [SerializeField]
    private ParticleSystem m_rHitParticles;
    //[SerializeField]
    //private LayerMask[] m_HitLayer;
    private Collider m_HitBox;
    [SerializeField]
    private string[] m_strTargetTag;
    [HideInInspector]
    public bool m_bIsActive = false;

    public UnityEvent OnHit;

    // Start is called before the first frame update
    void Start()
    {
        m_HitBox = GetComponent<Collider>();
    }

    
    private void OnTriggerEnter(Collider other) {
        if(m_strTargetTag.Length == 1) {
            if (other.CompareTag(m_strTargetTag[0]) && m_bIsActive) {
                // Damage event
                DamageController target = other.GetComponent<DamageController>();
                if (target) {
                    DamageMessage message = new DamageMessage();
                    message.damage = m_iDamage;
                    message.source = gameObject;
                    target.ApplyDamage(message);
                    OnHit.Invoke();
                    m_bIsActive = false; // Prevent multiple collisions
                }
            }
        }
        else {
            if (CheckMultipleTags(other.tag) && m_bIsActive) {
                // Damage event
                DamageController target = other.GetComponent<DamageController>();
                if (target) {
                    DamageMessage message = new DamageMessage();
                    message.damage = m_iDamage;
                    message.source = gameObject;
                    target.ApplyDamage(message);
                    OnHit.Invoke();
                    m_bIsActive = false; // Prevent multiple collisions
                }
            }
        }


    }

    private bool CheckMultipleTags(string _tag) {
        foreach(string tag in m_strTargetTag) {
            if(tag == _tag) {
                return true;
            }
        }
        return false;
    }


}
