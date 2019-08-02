using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAttacher : MonoBehaviour
{
    private void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("TagIgnore");
    }

    private void OnTriggerStay(Collider other) {
        //Parent player to platform
        if (other.gameObject.CompareTag("Player")) {
            if(other.gameObject.GetComponent<PlayerController>().GetIsWading())
            {
                OnTriggerExit(other);
            }
            else
            {
                other.gameObject.GetComponent<PlayerController>().SetOnMovingPlatform(true);
                other.gameObject.transform.parent = this.transform;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        //Unparent player from platform
        if (other.gameObject.CompareTag("Player")) {
            other.gameObject.GetComponent<PlayerController>().SetOnMovingPlatform(false);
            other.gameObject.transform.parent = null;
        }
    }
}
