using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAttacher : MonoBehaviour
{    
    private void OnTriggerStay(Collider other) {
        //Parent player to platform
        if (other.gameObject.CompareTag("Player")) {
            other.gameObject.GetComponent<PlayerController>().SetOnMovingPlatform(true);
            other.gameObject.transform.parent = this.transform;
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
