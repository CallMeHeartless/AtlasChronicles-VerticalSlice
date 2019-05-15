using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HoldableItem : MonoBehaviour
{
    
    public void ToggleCollider() {
        GetComponent<Collider>().enabled = false;
        StartCoroutine(ColliderOn());
    }

    private IEnumerator ColliderOn() {
        yield return new WaitForSeconds(0.35f);
        GetComponent<Collider>().enabled = true;
    }

}
