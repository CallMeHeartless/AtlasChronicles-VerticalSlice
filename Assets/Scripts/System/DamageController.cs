using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class DamageController : MonoBehaviour
{
    // Internal Variables
    [Header("Hit Properties")]
    [SerializeField]
    private int m_iMaxHealth;
    private int m_iCurrentHealth;
    private bool m_bIsInvulnerable = false;
    [SerializeField]
    private float m_fInvulnerabilityTime;
    private float m_fInvulnerabilityCounter = 0.0f;
    [SerializeField]
    [Range(0.0f, 360.0f)]
    private float m_fHitAngle;
    private Collider m_HitBox;


    [Header("Damage Events")]
    public UnityEvent OnDeath, OnReceiveDamage,OnReceiveHealing, OnHitWhileInvulnerable, OnBecomeVulnerable, OnResetDamage;
    System.Action m_Schedule;

    //[Tooltip("These gameObjects are notified when this object takes damage")]
    //public List<MonoBehaviour> m_OnDamageMessageReceivers;

    // Accessors
    public bool bIsInvulnerable { get { return m_bIsInvulnerable; } }
    public int iCurrentHealth { get { return m_iCurrentHealth; } }

    // Start is called before the first frame update
    void Start()
    {
        m_HitBox = GetComponent<Collider>();
        ResetDamage();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bIsInvulnerable) {
            ProcessInvulnerabilityTimer();
        }
    }

    private void ProcessInvulnerabilityTimer() {
        m_fInvulnerabilityCounter += Time.deltaTime;
        if(m_fInvulnerabilityCounter >= m_fInvulnerabilityTime) {
            m_fInvulnerabilityCounter = 0.0f;
            m_bIsInvulnerable = false;
            OnBecomeVulnerable.Invoke();
        }
    }

    // Used to clear the damage that the object has suffered
    public void ResetDamage() {
        m_iCurrentHealth = m_iMaxHealth;
        m_bIsInvulnerable = false;
        m_fInvulnerabilityCounter = 0.0f;
        OnResetDamage.Invoke();
    }

    public void ApplyDamage(DamageMessage _message) {
        // Return if already dead
        if (m_iCurrentHealth <= 0) {
            return;
        }

        // Process a hit whilst invulnerable
        if (m_bIsInvulnerable) {
            OnHitWhileInvulnerable.Invoke();
            return;
        }

        // Project direction to damager to the plane formed by direction of damage
        Vector3 positionToDamager = _message.source.transform.position - transform.position;
        positionToDamager -= transform.up * Vector3.Dot(transform.up, positionToDamager);

        // Check damage is within hit angle
        if(Vector3.Angle(transform.forward, positionToDamager) > 0.5f * m_fHitAngle) {
            return;
        }

        // Process damage
        m_bIsInvulnerable = true;
        m_iCurrentHealth -= _message.damage;

        // Invoke either a death or damage event
        if(m_iCurrentHealth <= 0) {
            //m_Schedule += OnDeath.Invoke(); // Avoids race condition when objects kill each other
            OnDeath.Invoke();
        }
        else {
            OnReceiveDamage.Invoke();
        }

        /// If message system is implemented, damage/death messages should be sent here

    }

    public void ApplyHealing(int _iHealth) {
        if(m_iCurrentHealth <= 0) { // Do not allow dead objects to be healed
            return;
        }

        m_iCurrentHealth += _iHealth;
        if(m_iCurrentHealth > m_iMaxHealth) {
            m_iCurrentHealth = m_iMaxHealth;
        }
        OnReceiveHealing.Invoke();
    }


#if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        if(Event.current.type == EventType.Repaint) {
            UnityEditor.Handles.color = Color.red;
            UnityEditor.Handles.ArrowHandleCap(0, transform.position, Quaternion.LookRotation(transform.forward), 1.0f, EventType.Repaint);
        }

        UnityEditor.Handles.color = new Color(1.0f, 0.0f, 0.0f, 0.5f);
        Vector3 forward = Quaternion.AngleAxis(-m_fHitAngle * 0.5f, transform.up) * transform.forward;
        UnityEditor.Handles.DrawSolidArc(transform.position, transform.up, forward, m_fHitAngle, 1.0f);
    }

#endif


}

public struct DamageMessage {
    public GameObject source;
    public int damage;
    public Vector3 direction;
}
