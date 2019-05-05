using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {

    // External references
    [Header("External References")]
    [SerializeField]
    private Camera m_rCameraReference;
    [SerializeField]
    private GameObject m_rProjectileArc;
    
    // Component references
    private CharacterController m_rCharacterController;
    private Animator m_rAnimator;
    private PlayerAnimationController m_rPAnimationController;

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
    private Vector3 m_ExternalForce = Vector3.zero;
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
    private GameObject[] m_rGlideTrails;

    // Combat variables
    [Header("Combat Variables")]
    [SerializeField]
    private int m_iMaxHealth = 4;
    private int m_iCurrentHealth;

    // Ability variables
    [Header("Ability Variables")]
    [Tooltip("The game object that will be used as the teleport marker")][SerializeField]
    private GameObject m_rTeleportMarkerPrefab;
    [SerializeField]
    private Vector3 m_vecTeleportMarkerOffset;
    private Vector3 m_vecTeleportLocation;
    private bool m_bTeleportMarkerDown = false;
    private GameObject m_rTeleportMarker; // Object to be instantiated and moved accordingly
    private GameObject m_rSwitchTarget;
    private GameObject m_rHeldObject;
    private bool m_bIsAiming = false;
    [SerializeField]
    private Transform m_rHeldObjectLocation;
    private float m_fPickupRadius = 0.95f;
    [SerializeField]
    private float m_fThrowSpeed = 10.0f;
    [SerializeField]
    private GameObject m_rTeleportParticles;
#endregion

    // Start is called before the first frame update
    void Start(){
        // Create component references
        m_rCharacterController = GetComponent<CharacterController>();
        m_rAnimator = GetComponentInChildren<Animator>();
        m_rPAnimationController = GetComponentInChildren<PlayerAnimationController>();
        if (!m_rCameraReference) {
            m_rCameraReference = GameObject.Find("Camera").GetComponent<Camera>();
        }

        // Initialise variables
        m_MovementDirection = Vector3.zero;
        m_iCurrentHealth = m_iMaxHealth;

        if (m_rTeleportMarkerPrefab) {
            m_rTeleportMarker = Instantiate(m_rTeleportMarkerPrefab);
            m_rTeleportMarker.SetActive(false);
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
        m_ExternalForce = Vector3.zero;
    }

    // Handles all of the functions that determine the vector to move the player, then move them
    private void HandlePlayerMovement() {
        CalculatePlayerMovement();
        CalculatePlayerRotation();
        ApplyGravity();
        ProcessFloat();
        Jump();
        m_MovementDirection.y += m_fVerticalVelocity * Time.deltaTime;
        //m_MovementDirection.y += m_fExternal * Time.deltaTime;
        m_rAnimator.SetFloat("JumpSpeed", m_MovementDirection.y);
        // Add external forces
        m_MovementDirection += m_ExternalForce * Time.deltaTime;
        
        // Move the player
        m_rCharacterController.Move(m_MovementDirection * m_fMovementSpeed * Time.deltaTime);

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
        m_MovementDirection = (m_rCameraReference.transform.right * Input.GetAxis("Horizontal") + m_rCameraReference.transform.forward * Input.GetAxis("Vertical")).normalized;
        m_MovementDirection.y = 0.0f;
        if (!m_rCharacterController.isGrounded) {
            return;
        }
        if(m_MovementDirection.sqrMagnitude == 0) {
            // Idle
            m_rAnimator.ResetTrigger("Run");
            m_rAnimator.SetTrigger("Idle");
        } else {
            m_rAnimator.ResetTrigger("Idle");
            m_rAnimator.SetTrigger("Run");
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
        if (m_rCharacterController.isGrounded || m_bCanDoubleJump || m_fCoyoteTimer < m_fCoyoteTime) {
            // Jump code
            if (Input.GetButtonDown(m_strJumpButton) && !m_bIsFloating) { // Change this here
                m_fVerticalVelocity = m_fJumpPower;
                m_fGravityMulitplier = 1.0f;
                // Control use of double jump
                if (!m_rCharacterController.isGrounded) {
                    m_bCanDoubleJump = false;
                }
                // Animation
                m_rAnimator.SetTrigger("Jump");
            }

        }

        // Handle related variables
        if (m_rCharacterController.isGrounded) {
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
        if (m_rCharacterController.isGrounded) {
            return;
        }

        // Accelerate the player
        m_fVerticalVelocity += Physics.gravity.y * m_fGravityMulitplier *  Time.deltaTime;
        if (m_rCharacterController.isGrounded) {
            m_fGravityMulitplier = 1.0f;
        } else {
            if(!m_bIsFloating) {
                m_fGravityMulitplier *= 1.2f;
                m_fGravityMulitplier = Mathf.Clamp(m_fGravityMulitplier, 1.0f, 20.0f);
            }
        }
        m_fVerticalVelocity = Mathf.Clamp(m_fVerticalVelocity, -100.0f, 100.0f);
    }

    // Handles the player floating slowly downwards
    private void ProcessFloat() {
        if (!m_rCharacterController.isGrounded && !m_bCanDoubleJump) {
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
        if (m_rCharacterController.isGrounded) {
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
            m_rAnimator.SetBool("Glide", true);
            if (m_rGlideTrails[0]) {
                foreach(GameObject trail in m_rGlideTrails) {
                    trail.SetActive(true);
                }
            }
        } else {
            m_rAnimator.SetBool("Glide", false);
            m_rAnimator.SetTrigger("Jump");
            if (m_rGlideTrails[0]) {
                foreach (GameObject trail in m_rGlideTrails) {
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
            if (!m_rHeldObject && m_rCharacterController.isGrounded) {
                // Place on ground
                m_rAnimator.ResetTrigger("Idle");
                m_rAnimator.ResetTrigger("Run");
                m_rAnimator.SetTrigger("Pickup");
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
            if (m_rSwitchTarget) {
                SwitchWithTarget();
            } else if(!m_rHeldObject){
                m_rAnimator.SetTrigger("Tag");
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

    // Teleports the player directly to a location (Should become called from PlayerAnimationController)
    private void TeleportToLocation(Vector3 _vecTargetLocation) {
        Vector3 vecPlayerPosition = transform.position;
        // Play VFX
        TeleportParticles();
        // Update position
        transform.position = _vecTargetLocation;

        // If marker was placed on thrown object, remove it
        if (m_rTeleportMarker.transform.parent) {
            m_rTeleportMarker.transform.parent.position = vecPlayerPosition;
            m_rTeleportMarker.transform.SetParent(null);
        }

        // Disable teleport marker
        m_rTeleportMarker.SetActive(false);
        m_bTeleportMarkerDown = false;
    }

    // Place the teleport marker on the ground
    public void PlaceTeleportMarker(Vector3 _vecPlacementLocation) {
        if (!m_rTeleportMarker) {
            return;
        }
        //m_Animator.SetTrigger("Tag");

        m_rTeleportMarker.transform.position = _vecPlacementLocation; // Need to use an offset, perhaps with animation
        // Enable teleport marker
        if (!m_rTeleportMarker.activeSelf) {
            m_rTeleportMarker.SetActive(true); // Replace this with teleport scroll animations, etc
            m_bTeleportMarkerDown = true;
        }
    }

    // Parent the teleport marker to the held object
    private void TagHeldObject() {
        if (!m_rHeldObject) {
            return;
        }
        m_rTeleportMarker.transform.position = m_rHeldObject.transform.position;
        m_rTeleportMarker.transform.SetParent(m_rHeldObject.transform);
        m_rTeleportMarker.SetActive(true);
        m_bTeleportMarkerDown = true;
    }

    private void TeleportToTeleportMarker() {
        if (!m_bTeleportMarkerDown || !m_rTeleportMarker || m_rHeldObject) {
            return; // Error animation / noise
        }

        TeleportToLocation(m_rTeleportMarker.transform.position);
        // Disable teleport marker
        m_rTeleportMarker.SetActive(false);
    }
    
    // Trade places with the switch target, then clear the target state
    private void SwitchWithTarget() {
        if (!m_rSwitchTarget) {
            return;
        }
        TeleportParticles();

        // Switch positions
        Vector3 vecPlayerPosition = transform.position;
        transform.position = m_rSwitchTarget.transform.position;
        m_rPAnimationController.GetSwitchMarker().GetComponent<SwitchTagController>().Switch(vecPlayerPosition);

        // Remove reference
        m_rSwitchTarget = null;
    }

    // Sets the player controller's switch target
    public void SetSwitchTarget(GameObject _switchTarget) {
        m_rSwitchTarget = _switchTarget;
    }

    public void ThrowHeldObject() {
        if (!m_rHeldObject) {
            return;
        }
        m_rHeldObject.transform.SetParent(null);
        Rigidbody heldObjectRb = m_rHeldObject.GetComponent<Rigidbody>();
        heldObjectRb.isKinematic = false;
        // Get velocity
        LineRenderer lineRenderer = m_rProjectileArc.GetComponent<LineRenderer>();
        Vector3 vecVelocity = lineRenderer.GetPosition(1) - lineRenderer.GetPosition(0);
        heldObjectRb.velocity = vecVelocity.normalized * m_fThrowSpeed;// Mathf.Sqrt(m_fThrowSpeed * m_fThrowSpeed + m_fThrowSpeed * m_fThrowSpeed);//m_fThrowSpeed;

        m_rHeldObject.GetComponent<HoldableItem>().ToggleCollider();
        //heldObjectRb.AddForce(vecVelocity.normalized * m_fThrowSpeed, ForceMode.Acceleration);
        m_rHeldObject = null;
        m_bIsAiming = false;
        m_rProjectileArc.SetActive(false); // Consider removing depending on how input will be handled
        // Animation
        m_rAnimator.SetTrigger("Throw");
    }

    // Show the projectile arc while the player is holding down the aim button || CHANGE CAMERA 
    private void AimHeldObject() {
        if (!m_rProjectileArc) {
            return;
        }

        if (Input.GetAxis(m_strAimButton) <0.0f || Input.GetKey(KeyCode.C)) {
            ToggleAiming(true);
            Vector3 vecCameraRotation = m_rCameraReference.transform.rotation.eulerAngles;
            // Line up with camera
            transform.rotation = Quaternion.Euler(0.0f, vecCameraRotation.y, 0.0f);
            m_rProjectileArc.GetComponent<ProjectileArc>().SetRotation(vecCameraRotation.y);
        }
        else if(m_rProjectileArc.activeSelf){
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
            m_rAnimator.SetTrigger("Aim");
            m_rProjectileArc.SetActive(true);
        } else {
            m_rAnimator.ResetTrigger("Aim");
            m_rAnimator.SetTrigger("Cancel");
            m_rProjectileArc.SetActive(false);
        }
    }

    // Pickup the nearest item or drop the held item
    private void GrabObject() {
            // Pick up the item
        if (!m_rHeldObject) {
            GameObject nearestItem = GetClosestHoldableItem();
            m_rAnimator.SetTrigger("Pickup");
            if (!nearestItem) {
                return;
            }
            m_rHeldObject = nearestItem;
            m_rHeldObject.transform.position = m_rHeldObjectLocation.transform.position;
            m_rHeldObject.transform.SetParent(m_rHeldObjectLocation);
            // Disable rigibody
            m_rHeldObject.GetComponent<Rigidbody>().isKinematic = true;
        } else {
            // Drop item
            m_rHeldObject.transform.SetParent(null);
            m_rHeldObject.GetComponent<Rigidbody>().isKinematic = false;
            m_rHeldObject = null;
        }
    }

    // Sets the player's vertical velocity 
    public void SetPlayerVerticalVelocity(float _fVelocity) {
        //m_fVerticalVelocity = _fVelocity;
        m_fExternal = _fVelocity;
        m_rCharacterController.Move(Vector3.up * 3.0f * Time.deltaTime);
        m_rAnimator.SetTrigger("Jump");
        m_rAnimator.ResetTrigger("Idle");
        m_rAnimator.ResetTrigger("Run");
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
        if (!m_rTeleportParticles) {
            return;
        }
        m_rTeleportParticles.SetActive(true);
        StartCoroutine(DisableTeleportParticles());
    }

    private IEnumerator DisableTeleportParticles() {
        yield return new WaitForSeconds(2.0f);
        m_rTeleportParticles.SetActive(false);
    }

    // Adds an external force to the player this frame
    public void AddExternalForce(Vector3 _vecExternalForce) {
        m_ExternalForce += _vecExternalForce;
        //m_rCharacterController.Move(Vector3.up * Time.deltaTime);
        //m_fVerticalVelocity += _vecExternalForce.y;
    }
}
