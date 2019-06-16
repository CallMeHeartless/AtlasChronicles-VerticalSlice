using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour {
    //[SerializeField]
    //private RespawnController m_rRespawnController;

    private void OnTriggerEnter(Collider other) {
        // Check object is player
        if (other.CompareTag("Player")) {
            // Respawn the player
            //if (m_rRespawnController) {
            //    m_rRespawnController.RespawnPlayer();
            //}
            //else {
            //    Debug.LogError("ERROR: Killbox does not have respawn controller");
            //}
            DamageMessage message = new DamageMessage();
            message.damage = 4;
            message.source = gameObject;
            other.GetComponent<DamageController>().ApplyDamage(message);
        }
    }
}