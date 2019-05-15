using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AIVision
{
    //[SerializeField]
    public float m_fVisionRadius = 10.0f;
    [SerializeField]
    [Range(0.0f, 360.0f)]
    private float m_fVisionAngle = 180.0f;
    [SerializeField]
    private float m_fMaxHeight = 1.0f;
    [SerializeField]
    private LayerMask m_BlockingMask;

    // Returns the player if they are visible, null otherwise
    public PlayerController DetectPlayer(Transform _rDetectionPoint) {
        // If player does not exist, return null
        if (!PlayerController.instance) {
            return null;
        }

        // Determine vectors to the player and their head
        Vector3 vecToPlayer = PlayerController.instance.transform.position - _rDetectionPoint.position;
        Vector3 vecToPlayerHead = vecToPlayer + Vector3.up * 0.7f; // The height of the player is 1.4 m

        // Check if the player is too high or low to detected
        if(Mathf.Abs(vecToPlayer.y) > m_fMaxHeight) {
            return null;
        }

        // Obtain vector concerning only the horizontal plane
        Vector3 vecToPlayerXZ = vecToPlayer;
        vecToPlayerXZ.y = 0.0f;

        // Check if the distance is within range
        if(vecToPlayerXZ.sqrMagnitude <= m_fVisionRadius * m_fVisionRadius) {
            // Next, check if player is within the vision angle
            if(Vector3.Dot(vecToPlayerXZ.normalized, _rDetectionPoint.forward) > Mathf.Cos(m_fVisionAngle * 0.5f * Mathf.Deg2Rad)) {
                // Finally, check if there is any obstruction
                bool bIsVisibile = false;
                Debug.DrawRay(_rDetectionPoint.position, vecToPlayer,Color.red);
                Debug.DrawRay(_rDetectionPoint.position, vecToPlayerHead, Color.red);

                bIsVisibile |= !Physics.Raycast(_rDetectionPoint.position, vecToPlayer.normalized, m_fVisionRadius, m_BlockingMask, QueryTriggerInteraction.Ignore);
                bIsVisibile |= !Physics.Raycast(_rDetectionPoint.position, vecToPlayerHead.normalized, m_fVisionRadius, m_BlockingMask, QueryTriggerInteraction.Ignore);

                if (bIsVisibile) {
                    return PlayerController.instance;
                }
            }
        }

        return null;
    }

    // Editor visualisation
#if UNITY_EDITOR
    public void EditorGizmo(Transform _transform) {
        Color c = new Color(0.0f, 0.0f, 0.7f, 0.4f);
        UnityEditor.Handles.color = c;
        Vector3 vecRotatedForward = Quaternion.Euler(0, -m_fVisionAngle * 0.5f, 0.0f) * _transform.forward;
        UnityEditor.Handles.DrawSolidArc(_transform.position, Vector3.up, vecRotatedForward, m_fVisionAngle, m_fVisionRadius);

        Gizmos.color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
        Gizmos.DrawWireSphere(_transform.position, 0.2f);
    }

#endif
}
