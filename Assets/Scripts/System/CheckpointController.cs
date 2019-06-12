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
            PlayerController player = other.GetComponent<PlayerController>();
            //Debug.Log("here");
            if (player) {
                if (m_bPoint)
                {
                    player.m_rRespawnLocation =  transform.GetChild(0).position;
                }
                else
                {
                    player.m_rRespawnLocation = transform.position;
                }

            }
            else {
                Debug.LogError("ERROR: Could not update player respawn position");
            }
        }
    }
}
