using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadController : MonoBehaviour {
    [SerializeField]
    private float m_fJumpForce = 10.0f;
    private AudioPlayer m_rAudioPlayer;

    private void Start()
    {
        m_rAudioPlayer = GetComponentInChildren<AudioPlayer>();
    }

    private void OnTriggerStay(Collider other) {

        if (other.CompareTag("Player")) {
            PlayerController player = other.GetComponent<PlayerController>();

            //Reset jump and glide when player lands on jump pad
            player.ResetJump();
            player.ResetFloatTimer();

            player.ClearExternalForces();
            player.ResetGravityMultiplier();
            player.AddExternalForce(Vector3.up * m_fJumpForce);
            m_rAudioPlayer.PlayAudio();
        }
    }
}
