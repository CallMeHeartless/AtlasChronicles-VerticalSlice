using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadController : MonoBehaviour {
    [SerializeField]
    private float m_fJumpForce = 10.0f;

    private void OnTriggerEnter(Collider other) {

        if (other.CompareTag("Player")) {
            other.GetComponent<PlayerController>().SetPlayerVerticalVelocity(m_fJumpForce);
        }
    }

    //private void OnCollisionEnter(Collision collision) {
    //    if (collision.gameObject.CompareTag("Player")) {
    //        collision.gameObject.GetComponent<PlayerController>().SetPlayerVerticalVelocity(m_fJumpForce);
    //        print("jump");
    //    }
    //}
}
