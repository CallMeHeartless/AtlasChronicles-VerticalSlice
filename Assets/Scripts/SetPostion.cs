using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPostion : MonoBehaviour
{
    public GameObject m_gSetLoctation;
    
    // Update is called once per frame
    void Update()
    {
        // Sets the location to follow on the rotating wheel 
        // If gameObject isnt null 
        if(m_gSetLoctation)
            transform.position = m_gSetLoctation.transform.position;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            other.gameObject.GetComponent<PlayerController>().SetOnMovingPlatform(true);
            other.gameObject.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            other.gameObject.GetComponent<PlayerController>().SetOnMovingPlatform(false);
            other.gameObject.transform.parent = null;
        }
    }
}
