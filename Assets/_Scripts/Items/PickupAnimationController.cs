using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAnimationController : MonoBehaviour
{
    Pickup m_rPickup;
    private void Start()
    {
        m_rPickup = transform.parent.GetComponent<Pickup>();

        if(m_rPickup != null)
        {
            //Start animation at random frame
            Animator rAnim = GetComponent<Animator>();
            AnimatorStateInfo state = rAnim.GetCurrentAnimatorStateInfo(0);
            rAnim.Play(state.fullPathHash, -1, Random.Range(0f, 1f));
        }
    }

    public void DestroyPickup() {
        //Deactivate pickups when picked up

        if(m_rPickup != null)
        {
            transform.parent.gameObject.SetActive(false);
        }
    }
}
