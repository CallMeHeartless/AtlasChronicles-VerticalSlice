using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAnimationController : MonoBehaviour
{
    public void DestroyPickup() {
        Destroy(gameObject);
    }
}
