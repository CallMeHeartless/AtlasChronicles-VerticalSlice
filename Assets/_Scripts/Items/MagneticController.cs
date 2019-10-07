using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticController : MonoBehaviour
{
    private static PlayerController s_rPlayer = null;
    [SerializeField]
    private float m_fMagneticDistance = 1.0f;
    private float m_fDistanceToPlayer = 0.0f;
    [SerializeField]
    private float m_fMoveSpeed = 0.3f;

    private void Start() {
        // Initialise the player reference
        if(s_rPlayer == null) {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            s_rPlayer = player.GetComponent<PlayerController>();
        }
    }

    void Update(){
        // Do not process if there is no player reference, or the game is paused etc.
        if (!s_rPlayer || !GameState.DoesPlayerHaveControl()) {
            return;
        }

        // Determine how far away the player is
        m_fDistanceToPlayer = Vector3.Distance(transform.position, s_rPlayer.transform.position);

        // Move the object towards the player if they are close enough to feel the pull
        if(m_fDistanceToPlayer <= m_fMagneticDistance) {
            // Find the unit direction towards the player
            Vector3 ToPlayer = (transform.position - s_rPlayer.transform.position).normalized;

            // Move the object towards the player
            transform.Translate(ToPlayer * m_fMoveSpeed * Time.deltaTime, Space.World);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        Color c = new Color(0.0f, 0.7f, 0.7f, 0.4f);
        UnityEditor.Handles.color = c;
        Vector3 vecRotatedForward = Quaternion.Euler(0, -360.0f * 0.5f, 0.0f) * transform.forward;
        UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.up, vecRotatedForward, 360.0f, m_fMagneticDistance);
    }

#endif
}
