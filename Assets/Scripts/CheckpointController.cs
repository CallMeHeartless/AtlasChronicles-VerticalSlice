using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            // Update the most recent checkpoint
            RespawnController parentController = transform.root.GetComponent<RespawnController>();
            if (parentController) {
                parentController.SetRespawnPoint(transform);
            }
            else {
                Debug.LogError("ERROR: Could not locate parent respawn controller. Is this checkpoint a child of the controller?");
            }
        }
    }
}
