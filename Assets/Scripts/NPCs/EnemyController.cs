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
    private Animator m_rStateMachine;
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
    [SerializeField]
    private AIWanderProperties m_WanderProperties;
    public AIWanderProperties m_rWanderProperties { get { return m_WanderProperties; } }

    [Header("Combat Properties")]
    [SerializeField]
    private float m_fAttackCooldown;
    private float m_fAttackTimer = 0.0f;
    private bool m_bHasMapFragment = false;

    // Start is called before the first frame update
    void Start(){
        m_rNavAgent = GetComponent<NavMeshAgent>();
        // Define agent properties
        m_rNavAgent.speed = m_fMovementSpeed;

        // Initialise wander properties
        m_WanderProperties.m_HomePosition = transform.position;

        // Initialise State machine
        m_rStateMachine = GetComponent<Animator>();
        AIState[] behaviours = m_rStateMachine.GetBehaviours<AIState>();
        if(behaviours[0] == null) {
            Debug.LogError("NO BEHAVIOUR FOUND");
        }
        foreach(AIState behaviour in behaviours) {
            behaviour.m_rAI = this;
        }

        // Initialise animator
        //m_rAnimator = GetComponentsInChildren<Animator>()[1];

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
            m_rStateMachine.SetBool("bCanSeePlayer", true);
        }
        else {
            m_rStateMachine.SetBool("bCanSeePlayer", false);
            // Transition to look, then return home
        }
    }

    public void SetDestination(Vector3 _vecTarget) {
        m_CurrentTarget = _vecTarget;
        m_rNavAgent.SetDestination(m_CurrentTarget);
    }

    // Returns the enemy to its home position
    public void WarpHome() {
        m_rNavAgent.Warp(m_HomeLocation);
    }

    // Controls whether the agent holds a map fragment
    public void ToggleMapFragment(bool _bState) {
        m_rStateMachine.SetBool("bIsEvading", _bState);
    }


    // Pause or resume all agents in scene
    public static void SetEnemyNavState(bool _bActive) {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies) {
            NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();
            if (agent) {
                agent.isStopped = _bActive;
            }
        }
    }

    public void Kill() {
        Destroy(gameObject);
    }


#if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        m_rVision.EditorGizmo(m_rEyes.transform);
        m_WanderProperties.EditorGizmo(transform.position);
    }

#endif
}
