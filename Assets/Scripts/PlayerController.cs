using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {

    // External references
    [Header("External References")]
    [SerializeField]
    private Camera m_CameraReference;
    [SerializeField]
    private GameObject m_ProjectileArc;
    
    // Component references
    private CharacterController m_CharacterController;
    private Animator m_Animator;
    private PlayerAnimationController m_PAnimationController;

    #region INTERNAL_VARIABLES
    // Control References
    private string m_strJumpButton = "Jump";
    private string m_strSwitchButton = "YButton";
    private string m_strTeleportMarkerPlaceButton = "XboxXButton";
    private string m_strTeleportButton = "BButton";
    private string m_strAimHeldObjectButton = "XBoxR2";
    private string m_strAimButton = "XBoxL2";
    private string m_strPickupItemButton = "L1";

    // Movement variables
    [Header("Movement Variables")]
    [SerializeField]
    private float m_fMovementSpeed;
    private float m_fTurnSpeed = 10.0f;
    [SerializeField]
    private float m_fJumpPower;
    [Tooltip("Time where the player may still jump after falling")][SerializeField]
    private float m_fCoyoteTime = 0.5f;
    private float m_fCoyoteTimer = 0.0f;
    private Vector3 m_MovementDirection;
    private bool m_bCanDoubleJump = true;
    private float m_fVerticalVelocity = 0.0f;
    private float m_fExternal = 0.0f;
    private float m_fGravityMulitplier = 1.0f;
    [Tooltip("The time that the player can float for")][SerializeField]
    private float m_fFloatTime = 2.0f;
    private float m_fFloatTimer = 0.0f;
    [Tooltip("The fraction of that gravity affects the player while they are floating")][SerializeField]
    private float m_fFloatGravityReduction = 0.8f;
    private bool m_bIsFloating = false;
    private bool m_bIsWading = false;
    [SerializeField]
    private GameObject[] m_GlideTrails;

    // Combat variables
    [Header("Combat Variables")]
    [SerializeField]
    private int m_iMaxHealth = 4;
    private int m_iCurrentHealth;

    // Ability variables
    [Header("Ability Variables")]
    [Tooltip("The game object that will be used as the teleport marker")][SerializeField]
    private GameObject m_TeleportMarkerPrefab;
    [SerializeField]
    private Vector3 m_vecTeleportMarkerOffset;
    private Vector3 m_vecTeleportLocation;
    private bool m_bTeleportMarkerDown = false;
    private GameObject m_TeleportMarker; // Object to be instantiated and moved accordingly
    private GameObject m_SwitchTarget;
    private GameObject m_HeldObject;
    private bool m_bIsAiming = false;
    [SerializeField]
    private Transform m_HeldObjectLocation;
    private float m_fPickupRadius = 0.95f;
    [SerializeField]
    private float m_fThrowSpeed = 10.0f;
    [SerializeField]
    private GameObject m_TeleportParticles;
#endregion

    // Start is called before the first frame update
    void Start(){
        // Create component references
        m_CharacterController = GetComponent<CharacterController>();
        m_Animator = GetComponentInChildren<Animator>();
        m_PAnimationController = GetComponentInChildren<PlayerAnimationController>();
        if (!m_CameraReference) {
            m_CameraReference = GameObject.Find("Main Camera").GetComponent<Camera>();
        }

        // Initialise variables
        m_MovementDirection = Vector3.zero;
        m_iCurrentHealth = m_iMaxHealth;

        if (m_TeleportMarkerPrefab) {
            m_TeleportMarker = Instantiate(m_TeleportMarkerPrefab);
            m_TeleportMarker.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update(){
        if (!GameState.DoesPlayerHaveControl()) {
            return;
        }

        // Calculate movement for the frame
        m_MovementDirection = Vector3.zero;
        HandlePlayerMovement();
        HandlePlayerAbilities();

    }

    // Handles all of the functions that determine the vector to move the player, then move them
    private void HandlePlayerMovement() {
        CalculatePlayerMovement();
        CalculatePlayerRotation();
        ApplyGravity();
        ProcessFloat();
        Jump();
        m_MovementDirection.y += m_fVerticalVelocity * Time.deltaTime;
        m_MovementDirection.y += m_fExternal * Time.deltaTime;
        m_Animator.SetFloat("JumpSpeed", m_MovementDirection.y);

        // Move the player
        m_CharacterController.Move(m_MovementDirection * m_fMovementSpeed * Time.deltaTime);

        // Reset external vertical force
        if (m_fExternal > 0.0f) {
            m_fExternal -= 100.0f * Time.deltaTime;
        } else {
            m_fExternal = 0.0f;
        }
        //m_fExternal = 0.0f;
    }

    // Calculate movement
    private void CalculatePlayerMovement() {
        // Take player input
        m_MovementDirection = (m_CameraReference.transform.right * Input.GetAxis("Horizontal") + m_CameraReference.transform.forward * Input.GetAxis("Vertical")).normalized;
        m_MovementDirection.y = 0.0f;
        if (!m_CharacterController.isGrounded) {
            return;
        }
        if(m_MovementDirection.sqrMagnitude == 0) {
            // Idle
            m_Animator.ResetTrigger("Run");
            m_Animator.SetTrigger("Idle");
        } else {
            m_Animator.ResetTrigger("Idle");
            m_Animator.SetTrigger("Run");
        }
    }

    // Rotates the player to look in the direction they are moving
    private void CalculatePlayerRotation() {
        // Prevent turning when stationary
        if(m_MovementDirection.sqrMagnitude == 0) {
            return;
        }
        Vector3 vecLookDirection = m_MovementDirection;
        vecLookDirection.y = 0.0f; // Remove y component
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(vecLookDirection), Time.deltaTime * m_fTurnSpeed);
    }

    // Performs a simple jump
    private void Jump() {
        // Handle jump input
        if (m_CharacterController.isGrounded || m_bCanDoubleJump || m_fCoyoteTimer < m_fCoyoteTime) {
            // Jump code
            if (Input.GetButtonDown(m_strJumpButton)) { // Change this here
                m_fVerticalVelocity = m_fJumpPower;
                m_fGravityMulitplier = 1.0f;
                // Control use of double jump
                if (!m_CharacterController.isGrounded) {
                    m_bCanDoubleJump = false;
                }
                // Animation
                m_Animator.SetTrigger("Jump");
            }

        }

        // Handle related variables
        if (m_CharacterController.isGrounded) {
            m_bCanDoubleJump = true;
            m_fCoyoteTimer = 0.0f;
            if (m_bIsFloating) {
                m_bIsFloating = false;
            }
        } else {
            m_fCoyoteTimer += Time.deltaTime;
        }
    }

    // Updates the player's vertical velocity to consider gravity
    private void ApplyGravity() {
        // Check if floating
        if (m_bIsFloating) {
            m_fGravityMulitplier = m_fFloatGravityReduction;
        }
        if (m_CharacterController.isGrounded) {
            return;
        }

        // Accelerate the player
        m_fVerticalVelocity += Physics.gravity.y * m_fGravityMulitplier *  Time.deltaTime;
        if (m_CharacterController.isGrounded) {
            m_fGravityMulitplier = 1.0f;
        } else {
            m_fGravityMulitplier *= 1.2f;
            m_fGravityMulitplier = Mathf.Clamp(m_fGravityMulitplier, 1.0f, 20.0f);
        }
        m_fVerticalVelocity = Mathf.Clamp(m_fVerticalVelocity, -100.0f, 100.0f);
    }

    // Handles the player floating slowly downwards
    private void ProcessFloat() {
        if (!m_CharacterController.isGrounded && !m_bCanDoubleJump) {
            // The player can start floating after a double jump
            if(Input.GetButtonDown(m_strJumpButton) && m_fFloatTimer == 0.0f) { // Change comparison to < m_fFloatTimer for multiple floats per jump
                ToggleFloatState(true);
            }
            else if (Input.GetButtonUp(m_strJumpButton)) {
                ToggleFloatState(false);
            }
        }
        // Increment float timer while the player is floating
        if (m_bIsFloating) {
            m_fFloatTimer += Time.deltaTime;
            if(m_fFloatTimer > m_fFloatTime) {
                ToggleFloatState(false);
            }
        }
        // Allow the character to float again only once they have touched the ground
        if (m_CharacterController.isGrounded) {
            m_fFloatTimer = 0.0f;
            ToggleFloatState(false);
        }
    }

    // Toggles the internal variables when the player starts/stops floating
    private void ToggleFloatState(bool _bState) {
        if(m_bIsFloating == _bState) {
            return;
        }
        m_bIsFloating = _bState;

        if (m_bIsFloating) {
            // Level out the player's upward velocity to begin gliding
            m_fVerticalVelocity = 0.0f;
            m_Animator.SetTrigger("Glide");
            if (m_GlideTrails[0]) {
                foreach(GameObject trail in m_GlideTrails) {
                    trail.SetActive(true);
                }
            }
        } else {
            //m_Animator.ResetTrigger("Glide");
            m_Animator.SetTrigger("Jump");
            if (m_GlideTrails[0]) {
                foreach (GameObject trail in m_GlideTrails) {
                    trail.SetActive(false);
                }
            }
        }
    }

    // Deals damage to the player, and checks for death
    public void DamagePlayer(int _iDamage) {
        m_iCurrentHealth -= _iDamage;
        if(m_iCurrentHealth <= 0) {
            // Death
            print("Player is dead");
        }
    }

    // Handles all of the functions that control player abilities
    private void HandlePlayerAbilities() {
        // Handle placing a teleport marker
        if (Input.GetButtonDown(m_strTeleportMarkerPlaceButton)) {
            // Throw a tag if there is no  held object
            if (!m_HeldObject && m_CharacterController.isGrounded) {
                // Place on ground
                m_Animator.ResetTrigger("Idle");
                m_Animator.ResetTrigger("Run");
                m_Animator.SetTrigger("Pickup");
                PlaceTeleportMarker(transform.position - new Vector3(0, 0.7f, 0));
            } else {
                TagHeldObject();
            }

        }
        // Teleporting to the marker
        else if (Input.GetButtonDown(m_strTeleportButton)) {
            TeleportToTeleportMarker();
        }
        // Throw switch tag / switch teleport
        else if (Input.GetButtonDown(m_strSwitchButton)) {
            if (m_SwitchTarget) {
                SwitchWithTarget();
            } else if(!m_HeldObject){
                m_Animator.SetTrigger("Tag");
            }
        }
        // Toggle the projectile arc
        AimHeldObject();
        // Pickup or throw an item
        if (Input.GetButtonDown(m_strPickupItemButton)) {
            if (m_bIsAiming) {
                ThrowHeldObject();
            } else {
                GrabObject();
            }
        }
    }

    private void TeleportToLocation(Vector3 _vecTargetLocation) {
        // Play VFX
        TeleportParticles();
        // Update position
        transform.position = _vecTargetLocation;

        // If marker was placed on thrown object, remove it
        m_TeleportMarker.transform.SetParent(null);
        m_TeleportMarker.SetActive(false);
        m_bTeleportMarkerDown = false;
    }

    // Place the teleport marker on the ground
    public void PlaceTeleportMarker(Vector3 _vecPlacementLocation) {
        if (!m_TeleportMarker) {
            return;
        }
        //m_Animator.SetTrigger("Tag");

        m_TeleportMarker.transform.position = _vecPlacementLocation; // Need to use an offset, perhaps with animation
        // Enable teleport marker
        if (!m_TeleportMarker.activeSelf) {
            m_TeleportMarker.SetActive(true); // Replace this with teleport scroll animations, etc
            m_bTeleportMarkerDown = true;
        }
    }

    // Parent the teleport marker to the held object
    private void TagHeldObject() {
        if (!m_HeldObject) {
            return;
        }
        m_TeleportMarker.transform.position = m_HeldObject.transform.position;
        m_TeleportMarker.transform.SetParent(m_HeldObject.transform);
        m_TeleportMarker.SetActive(true);
        m_bTeleportMarkerDown = true;
    }

    private void TeleportToTeleportMarker() {
        if (!m_bTeleportMarkerDown || !m_TeleportMarker || m_HeldObject) {
            return; // Error animation / noise
        }

        TeleportToLocation(m_TeleportMarker.transform.position);
        // Disable teleport marker
        m_TeleportMarker.SetActive(false);
    }
    
    // Trade places with the switch target, then clear the target state
    private void SwitchWithTarget() {
        if (!m_SwitchTarget) {
            return;
        }
        TeleportParticles();

        // Switch positions
        Vector3 vecPlayerPosition = transform.position;
        transform.position = m_SwitchTarget.transform.position;
        m_PAnimationController.GetSwitchMarker().GetComponent<SwitchTagController>().Switch(vecPlayerPosition);

        // Remove reference
        m_SwitchTarget = null;
    }

    // Sets the player controller's switch target
    public void SetSwitchTarget(GameObject _switchTarget) {
        m_SwitchTarget = _switchTarget;
    }

    public void ThrowHeldObject() {
        if (!m_HeldObject) {
            return;
        }
        m_HeldObject.transform.SetParent(null);
        Rigidbody heldObjectRb = m_HeldObject.GetComponent<Rigidbody>();
        heldObjectRb.isKinematic = false;
        // Get velocity
        LineRenderer lineRenderer = m_ProjectileArc.GetComponent<LineRenderer>();
        Vector3 vecVelocity = lineRenderer.GetPosition(1) - lineRenderer.GetPosition(0);
        heldObjectRb.velocity = vecVelocity.normalized * m_fThrowSpeed;// Mathf.Sqrt(m_fThrowSpeed * m_fThrowSpeed + m_fThrowSpeed * m_fThrowSpeed);//m_fThrowSpeed;

        m_HeldObject.GetComponent<HoldableItem>().ToggleCollider();
        //heldObjectRb.AddForce(vecVelocity.normalized * m_fThrowSpeed, ForceMode.Acceleration);
        m_HeldObject = null;
        m_bIsAiming = false;
        m_ProjectileArc.SetActive(false); // Consider removing depending on how input will be handled
        // Animation
        m_Animator.SetTrigger("Throw");
    }

    // Show the projectile arc while the player is holding down the aim button || CHANGE CAMERA 
    private void AimHeldObject() {
        if (!m_ProjectileArc) {
            return;
        }

        if (Input.GetAxis(m_strAimButton) <0.0f || Input.GetKey(KeyCode.C)) {
            ToggleAiming(true);
            Vector3 vecCameraRotation = m_CameraReference.transform.rotation.eulerAngles;
            // Line up with camera
            transform.rotation = Quaternion.Euler(0.0f, vecCameraRotation.y, 0.0f);
            m_ProjectileArc.GetComponent<stoneArc>().SetRotation(vecCameraRotation.y);
        }
        else if(m_ProjectileArc.activeSelf){
            ToggleAiming(false);
        }
    }

    // Changes parameters for when the player is / is not aiming
    private void ToggleAiming(bool _bState) {
        if(m_bIsAiming == _bState) {
            return;
        }
        m_bIsAiming = _bState;
        if (m_bIsAiming) {
            m_Animator.SetTrigger("Aim");
            m_ProjectileArc.SetActive(true);
        } else {
            m_Animator.ResetTrigger("Aim");
            m_Animator.SetTrigger("Cancel");
            m_ProjectileArc.SetActive(false);
        }
    }

    // Pickup the nearest item or drop the held item
    private void GrabObject() {
            // Pick up the item
        if (!m_HeldObject) {
            GameObject nearestItem = GetClosestHoldableItem();
            m_Animator.SetTrigger("Pickup");
            if (!nearestItem) {
                return;
            }
            m_HeldObject = nearestItem;
            m_HeldObject.transform.position = m_HeldObjectLocation.transform.position;
            m_HeldObject.transform.SetParent(m_HeldObjectLocation);
            // Disable rigibody
            m_HeldObject.GetComponent<Rigidbody>().isKinematic = true;
        } else {
            // Drop item
            m_HeldObject.transform.SetParent(null);
            m_HeldObject.GetComponent<Rigidbody>().isKinematic = false;
            m_HeldObject = null;
        }
    }

    // Sets the player's vertical velocity 
    public void SetPlayerVerticalVelocity(float _fVelocity) {
        //m_fVerticalVelocity = _fVelocity;
        m_fExternal = _fVelocity;
        m_CharacterController.Move(Vector3.up * 3.0f * Time.deltaTime);
        m_Animator.SetTrigger("Jump");
        m_Animator.ResetTrigger("Idle");
        m_Animator.ResetTrigger("Run");
        print("SetPlayerVerticalVelocity");
    }

    // Finds the closest holdable object
    private GameObject GetClosestHoldableItem() {
        Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, m_fPickupRadius);
        GameObject nearest = null;
        float fDistanceToNearest = 1000.0f;

        // Iterate through and check distances
        foreach(Collider item in nearbyObjects) {
            if (!item.CompareTag("HoldableItem") || item.transform.position.y > transform.position.y) {
                continue;
            } else {
                float fItemDistance = (item.transform.position - transform.position).sqrMagnitude;
                if(fItemDistance < fDistanceToNearest) {
                    fDistanceToNearest = fItemDistance;
                    nearest = item.gameObject;
                }
            }
        }
        return nearest;
    }

    private void TeleportParticles() {
        if (!m_TeleportParticles) {
            return;
        }
        m_TeleportParticles.SetActive(true);
        StartCoroutine(DisableTeleportParticles());
    }

    private IEnumerator DisableTeleportParticles() {
        yield return new WaitForSeconds(2.0f);
        m_TeleportParticles.SetActive(false);
    }
}
