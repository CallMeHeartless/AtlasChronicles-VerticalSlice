using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    // External References
    private NavMeshAgent m_rNavAgent;
    [SerializeField]
    private Animator m_rAnimator;
    public Animator animator { get { return m_rAnimator; } }
    private Animator m_rStateMachine;
    [SerializeField]
    private GameObject m_rMapFragmentPrefab;
    [SerializeField]
    private GameObject m_rMap;
    [SerializeField]
    private GameObject m_rTagOnHead;

    [Header("Vision")]
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
    [SerializeField]
    private float m_fKnockoutTime = 10.0f;
    private bool m_bIsKnockedOut = false;
    public bool isKnockedOut { get { return m_bIsKnockedOut; } }
    private bool m_bIsTagged = false;

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
            behaviour.m_rAgent = m_rNavAgent;
        }
    }

    // Update is called once per frame
    void Update(){
        // Do not process the enemy if they should be disabled
        if (!GameState.DoesPlayerHaveControl() || m_bIsKnockedOut) {
            m_rNavAgent.isStopped = true;
            return;
        }
        m_rNavAgent.isStopped = false;
        // Look for the player
        m_rPlayer = m_rVision.DetectPlayer(m_rEyes);
        if (m_rPlayer) {
            // Move to player
            m_rStateMachine.SetBool("bCanSeePlayer", true);

            // If beyond home range, give up on chasing
            if (IsBeyondHomeRange()) {
                m_rStateMachine.SetBool("bCanSeePlayer", false);
            }
        }
        else {
            m_rStateMachine.SetBool("bCanSeePlayer", false);
            // Transition to look, then return home
        }

        // Check if away from navmesh
        if (!m_rNavAgent.isOnNavMesh) {
            Kill();
        }
    }

    private bool IsBeyondHomeRange() {
        return (m_rWanderProperties.m_HomePosition - transform.position).sqrMagnitude > m_fPursuitRadius * m_fPursuitRadius;
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
        if(m_bHasMapFragment == _bState) {
            return;
        }
        m_bHasMapFragment = _bState;
        if (!m_bHasMapFragment) {
            // Spawn the map fragment that was dropped
            GameObject map = GameObject.Instantiate<GameObject>(m_rMapFragmentPrefab, transform);
            map.transform.SetParent(null);
            Debug.Log("The goon dropped a map fragment");
        } else {
            // Grab map fragment animation
            m_rAnimator.SetTrigger("StealMap");
        }
        if (m_rMap) {
            m_rMap.SetActive(m_bHasMapFragment);
        }
        m_rStateMachine.SetBool("bIsEvading", m_bHasMapFragment);

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

    public void ForceNoticePlayer() {
        // Return if already evading player
        if (m_rStateMachine.GetBool("bIsEvading") || m_bIsKnockedOut) {
            return;
        }
        m_rPlayer = GameObject.Find("Player").GetComponent<PlayerController>();
        m_rStateMachine.SetBool("bCanSeePlayer", true);
        // Rotate around
        Vector3 toPlayer = (m_rPlayer.transform.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(toPlayer, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.5f);

    }

    public void Attack() {
        if (m_rAnimator) {
            m_rAnimator.SetTrigger("Attack");
        }
    }

    public void Knockout() {
        m_bIsKnockedOut = true;
        m_rStateMachine.SetBool("bIsKnockedOut", true);
        m_rAnimator.SetTrigger("Death");
        m_rNavAgent.isStopped = true;
        // VFX

        // Drop map (if holding)
        ToggleMapFragment(false);

        // Start timer
        StartCoroutine(Revive());
    }

    private IEnumerator Revive() {
        yield return new WaitForSeconds(m_fKnockoutTime);
        // Renable enemy
        m_rNavAgent.isStopped = false;
        GetComponent<DamageController>().ResetDamage();
        m_rAnimator.ResetTrigger("Death");
        m_rAnimator.SetTrigger("GetUp");
        m_bIsKnockedOut = false;
        m_rStateMachine.SetBool("bIsKnockedOut", false);
    }

    // Apply a tag to the enemy, disorienting them
    public void SetTag(bool _bState) {
        m_rStateMachine.SetBool("bIsTagged", _bState);
        m_rAnimator.SetBool("Tagged", _bState);
        m_bIsTagged = _bState;
        if (_bState) {
            ToggleMapFragment(false);
        }
    }

    public void Patrol() {
        m_rAnimator.SetTrigger("Patrol");
    }

    public void ToggleTagOnHead(bool _bState) {
        if (m_rTagOnHead) {
            m_rTagOnHead.SetActive(_bState);
        }
    }


#if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        m_rVision.EditorGizmo(m_rEyes.transform);
        m_WanderProperties.EditorGizmo(transform.position);
    }

#endif
}
