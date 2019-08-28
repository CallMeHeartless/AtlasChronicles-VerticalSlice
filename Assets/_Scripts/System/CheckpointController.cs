using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public bool m_bPoint = false;

    public bool m_bIsPowered = true;

    [Header("Materials")]
    [SerializeField]
    private Material m_rInactive;
    [SerializeField]
    private Material m_rActive;
    private MeshRenderer m_rRenderer;

    private void Start() {
        m_rRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Do nothing if the checkpoint is not powered.
        if (!m_bIsPowered) {
            return;
        }

        // Handle the player entering the field
        if (other.CompareTag("Player")){
            // Get a reference to the player
            PlayerController player = other.GetComponent<PlayerController>();

            // If found, update their most recent respawn position
            if (player) {
                if (m_bPoint)
                {
                    player.m_rRespawnLocation =  transform.GetChild(0).position;
                }
                else
                {
                    player.m_rRespawnLocation = transform.position;
                }

                // Toggle the materials of all checkpoints
                UpdateAllMaterials();

                // Heal the player
                other.gameObject.GetComponent<DamageController>().ResetDamage();
            }
            else {
                Debug.LogError("ERROR: Could not update player respawn position");
            }
        }
    }

    // Ensures that the recently activated checkpoint has the active material, and all others have inactive ones
    private void UpdateAllMaterials() {
        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("spawns");
        foreach(GameObject checkpoint in checkpoints) {
            CheckpointController controller = checkpoint.GetComponent<CheckpointController>();
            if (controller && controller == this) {
                controller.SetMaterial(true);
            } else {
                controller.SetMaterial(false);
            }
        }
    }

    // Toggles the material used by the checkpoint
    private void SetMaterial(bool _bActive) {
        if (_bActive) {
            m_rRenderer.material = m_rActive;
        } else {
            m_rRenderer.material = m_rInactive;
        }
    }

    public void PowerOn() {
        m_bIsPowered = true;
    }
}
