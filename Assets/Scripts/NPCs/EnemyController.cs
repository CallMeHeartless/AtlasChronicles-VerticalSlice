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
    [SerializeField]
    private float m_fMovementSpeed;
    private Vector3 m_HomeLocation;
    [SerializeField]
    private float m_fPursuitRadius = 15.0f;

    // Start is called before the first frame update
    void Start(){
        m_rNavAgent = GetComponent<NavMeshAgent>();
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

        }
    }


#if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        m_rVision.EditorGizmo(m_rEyes.transform);
    }

#endif
}
