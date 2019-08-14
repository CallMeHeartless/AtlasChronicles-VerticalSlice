using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField]
    private EnemyController m_rEnemyController;
    [SerializeField]
    private MeleeAttack[] m_Weapons;
    [SerializeField]
    private GameObject m_Exclamation;


    void Start()
    {
        // Gain a reference to the parent enemy controller
        if (!m_rEnemyController) {
            m_rEnemyController = GetComponentInParent<EnemyController>();
        }
    }

    public void Kill() {
        Destroy(gameObject);
    }

    // Readies all melee weapons for attacking
    public void EnableAttacks() {
        foreach (MeleeAttack weapon in m_Weapons) {
            weapon.gameObject.SetActive(true);
            weapon.m_bIsActive = true;
        }
    }

    // Disables all melee weapons
    public void DisableAttacks() {
        foreach (MeleeAttack weapon in m_Weapons) {
            weapon.gameObject.SetActive(false);
            weapon.m_bIsActive = false;
        }
    }

    // Stun the goon
    public void Knockout() {
        m_rEnemyController.Knockout();
    }

    // Stops the navmesh agent
    public void StopAgent() {
        m_rEnemyController.StopAgent();
    }

    // Releases the navemesh agent
    public void FreeAgent() {
        m_rEnemyController.FreeAgent();
    }

    public void ExclamationOn() {
        m_Exclamation.SetActive(true);
    }

    public void ExclamationOff() {
        m_Exclamation.SetActive(false);
    }


}
