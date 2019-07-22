using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetherDestructionVolume : MonoBehaviour
{

    [SerializeField]
    private ParticleSystem m_rTriggerParticles;
    // Breaks all the teleport tethers a player has upon entry
    public void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            other.GetComponent<PlayerController>().BreakTethers();
            // VFX
            if (m_rTriggerParticles) {
                m_rTriggerParticles.Play();
            }
        }
    }
}
