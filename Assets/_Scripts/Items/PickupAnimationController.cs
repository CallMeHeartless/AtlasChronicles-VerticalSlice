using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAnimationController : MonoBehaviour
{
    Pickup m_rPickup;
    private void Start()
    {
        //Start animation at random frame
        Animator rAnim = GetComponent<Animator>();
        AnimatorStateInfo state = rAnim.GetCurrentAnimatorStateInfo(0);
        rAnim.Play(state.fullPathHash, -1, Random.Range(0f, 1f));
        m_rPickup = transform.parent.GetComponent<Pickup>();
    }

    public void DestroyPickup() {
        //Only destroy pickup if it is a collectable or if it is a map fragment that is stolen
        if (m_rPickup.GetPickupType() == PickupType.ECollectable || 
           (m_rPickup.GetPickupType() == PickupType.EMap && m_rPickup.GetIsStolen()))
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
