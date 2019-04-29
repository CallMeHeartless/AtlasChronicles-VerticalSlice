using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAnimationController : MonoBehaviour
{
    private void Start()
    {
        //Start fruit animation at random frame
        Animator rAnim = GetComponent<Animator>();
        AnimatorStateInfo state = rAnim.GetCurrentAnimatorStateInfo(0);
        rAnim.Play(state.fullPathHash, -1, Random.Range(0f, 1f));
    }

    public void DestroyPickup() {
        Destroy(gameObject);
    }
}
