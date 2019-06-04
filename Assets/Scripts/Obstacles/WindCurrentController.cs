using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindCurrentController : MonoBehaviour
{

    [SerializeField]
    private float m_fWindStrength = 10.0f;
    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player.IsFloating()) {
                player.ResetJump();
                player.ResetFloatTimer();
                player.AddExternalForce(transform.up * m_fWindStrength);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().AddExternalForce(Vector3.zero);
        }
    }
}
