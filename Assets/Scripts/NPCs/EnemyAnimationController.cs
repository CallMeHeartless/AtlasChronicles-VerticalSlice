using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField]
    private EnemyController m_rEnemyController;

    [SerializeField]
    private MeleeAttack[] m_Weapons;

    // Start is called before the first frame update
    void Start()
    {
        if (!m_rEnemyController) {
            m_rEnemyController = GetComponentInParent<EnemyController>();
        }
    }

    public void Kill() {
        Destroy(gameObject);
    }

    public void ToggleAttacks(bool _bState) {
        foreach(MeleeAttack weapon in m_Weapons) {
            weapon.gameObject.SetActive(_bState);
            weapon.m_bIsActive = _bState;
        }
    }

    public void EnableAttacks() {
        foreach (MeleeAttack weapon in m_Weapons) {
            weapon.gameObject.SetActive(true);
            weapon.m_bIsActive = true;
        }
    }

    public void DisableAttacks() {
        foreach (MeleeAttack weapon in m_Weapons) {
            weapon.gameObject.SetActive(false);
            weapon.m_bIsActive = false;
        }
    }
}
