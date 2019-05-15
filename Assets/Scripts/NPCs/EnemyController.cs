using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    // External References
    private NavMeshAgent m_rNavAgent;
    private Animator m_rAnimator;
    //[SerializeField]
    public AIVision m_rVision;
    [SerializeField][Tooltip("An empty game object positioned where the AI's eyes are, with forward direction aligned with its parent")]
    private Transform m_rEyes;
    private PlayerController m_rPlayer = null;

    // Internal variables
    [Header("Navmesh properties")]
    [SerializeField]
    private float m_fMovementSpeed;
    [SerializeField]
    private float m_fTurningSpeed;
    [SerializeField]
    private float m_fMaxAcceleration;
    private Vector3 m_HomeLocation;
    private Vector3 m_CurrentTarget;
    [SerializeField]
    private float m_fPursuitRadius = 15.0f;

    // Start is called before the first frame update
    void Start(){
        m_rNavAgent = GetComponent<NavMeshAgent>();
        // Define agent properties
        m_rNavAgent.speed = m_fMovementSpeed;
        Debug.Log("NMA Orientation: " + m_rNavAgent.updateRotation);
        Debug.Log("NMA Turning speed: " + m_rNavAgent.angularSpeed);
        Debug.Log("NMA Acceleration: " + m_rNavAgent.acceleration);
    }

    // Update is called once per frame
    void Update(){
        // Do not process the enemy if there is no 
        if (!GameState.DoesPlayerHaveControl()) {
            return;
        }

        // Look for the player
        m_rPlayer = m_rVision.DetectPlayer(m_rEyes);
        if (m_rPlayer) {
            // Move to player
            m_CurrentTarget = m_rPlayer.transform.position;
            m_rNavAgent.SetDestination(m_CurrentTarget);
        }
    }


#if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        m_rVision.EditorGizmo(m_rEyes.transform);
    }

#endif
}
