using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
 public bool m_bPoint = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){

            // Update the most recent checkpoint
            RespawnController parentController = transform.root.GetComponent<RespawnController>();
            //Debug.Log("here");
            if (parentController) {
                if (m_bPoint)
                {
                    parentController.SetRespawnPoint(transform.GetChild(0));
                }
                else
                {
                    parentController.SetRespawnPoint(transform);
                }
                
                //Debug.Log("new");
            }
            else {
                Debug.LogError("ERROR: Could not locate parent respawn controller. Is this checkpoint a child of the controller?");
            }
        }
    }
}
