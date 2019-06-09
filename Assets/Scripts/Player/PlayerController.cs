using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using MessageSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour, IMessageReceiver {

    // External references
    [Header("External References")]
    [SerializeField]
    private Camera m_rCameraReference;
    [SerializeField]
    private GameObject m_rSwitchTagCrosshair;
    private PlayerAudioController m_rPlayerAudioController;
    private DisplayStat m_rUI;

    // Component references
    private CharacterController m_rCharacterController;
    [SerializeField] Animator m_rAnimator;
    private PlayerAnimationController m_rPAnimationController;

    #region INTERNAL_VARIABLES
    private static PlayerController m_rInstance = null;
    public static PlayerController instance { get { return m_rInstance; } }

    // Control References
    private string m_strJumpButton = "Jump";
    private string m_strSwitchButton = "XBoxL2";
    private string m_strTeleportMarkerPlaceButton = "L1";
    private string m_strTeleportButton = "R1";
    private string m_strAimHeldObjectButton = "XBoxR2";
    private string m_strPickupItemButton = "L1";
    private string m_strSprintButton = "BButton";
    private string m_strAttackButton = "XBoxXButton";
    private string m_strCameraLockButton = "XBoxR2";

    // Movement variables
    [Header("Movement Variables")]
    [SerializeField]
    private float m_fMovementSpeed;
    [SerializeField]
    private float m_fSprintMultiplier = 1.75f;
    [SerializeField]
    private float m_fWaterSlowMultiplier = 0.5f;
    private bool m_bIsWading = false;
    private float m_fCurrentMovementSpeed;
    private float m_fTurnSpeed = 15.0f;
    [SerializeField]
    private float m_fJumpPower;
    [Tooltip("Time where the player may still jump after falling")] [SerializeField]
    private float m_fCoyoteTime = 0.5f;
    private float m_fCoyoteTimer = 0.0f;
    private Vector3 m_MovementDirection;
    [SerializeField] private Vector3 m_ExternalForce = Vector3.zero;
    private Vector3 m_MovementInput = Vector3.zero;
    private bool m_bCanDoubleJump = false;
    [SerializeField]
    private Vector3 m_Velocity = Vector3.zero;
    private float m_fVerticalVelocity = 0.0f;
    private float m_fExternal = 0.0f;
    private float m_fGravityMulitplier = 1.0f;
    [SerializeField] private float m_fMaxGravityMultiplier = 2.0f;
    [SerializeField][Tooltip("The rate by which gravity is multiplied every frame, up to the maximum")]
    private float m_fGravityMultiplierRate = 1.2f;

    [Tooltip("The time that the player can float for")] [SerializeField]
    private float m_fFloatTime = 2.0f;
    private float m_fFloatTimer = 0.0f;
    [Tooltip("The fraction of that gravity affects the player while they are floating")] [SerializeField]
    private float m_fFloatGravityReduction = 0.8f;
    private bool m_bIsFloating = false;
    [SerializeField]
    private GameObject[] m_rGlideTrails;

    // Combat variables
    [Header("Combat Variables")]
    [SerializeField]
    private float m_fAttackCooldown = 1.0f;
    private bool m_bCanAttack = true;
    private bool m_bSlamAttack = false;
    private bool m_bPlummeting = false;
    [SerializeField]
    private float m_fSlamAttackSpeed = 20.0f;
    [SerializeField]
    private GameObject m_rSlamAttack;

    // Ability variables
    [Header("Ability Variables")]
    [Tooltip("The game object that will be used as the teleport marker")] [SerializeField]
    private GameObject m_rTeleportMarkerPrefab;
    [SerializeField] [Tooltip("The distance beyond which the player cannot activate their teleport abilities")]
    private float m_fTeleportTetherDistance = 50.0f;
    [SerializeField]
    [Tooltip("The distance at which teleport markers are removed")]
    private float m_fTeleportBreakDistance = 55.0f;
    [SerializeField]
    private Vector3 m_vecTeleportMarkerOffset;
    private Vector3 m_vecTeleportLocation;
    private bool m_bTeleportMarkerDown = false;
    private bool m_bTeleportThresholdWarning = false;
    private bool m_bSwitchThresholdWarning = false;
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
    [SerializeField]
    private GameObject m_rTeleportScroll;
    [SerializeField]
    private GameObject m_rGlideScroll;
    private bool m_bWasSwitchLastTeleportCommand = false; // An internal flag to determine if the most recent teleport command was to switch or teleport to the marker

    [Header("Slide Detection Variables")]
    [SerializeField] private bool m_bIsSliding = false;
    [SerializeField] bool m_bIsOnMovingPlatform = false;
    private RaycastHit rayHit;
    private Vector3 hitNormal; //orientation of the slope.
    private Vector3 m_vSlideDir;
    private float rayDistance = 0.0f;
    private bool m_bSteepSlopeCollided = false;
    private bool m_bStandingOnSlope = false; // is on a slope or not
    private float m_fSlideSpeed = 200.0f;

    //Extforce variables
    private bool m_bExtForceOccuring;
    private float m_bXSmoothSpeed = 0.0f;
    private float m_bZSmoothSpeed = 0.0f;
    private float m_fHorizontalSmoothSpeed = 0.3f;
    private bool m_bCanGlide = false;
    private bool m_bCoyoteAllowed = false;
    private bool m_bInitialJumped = true;
    private bool m_bCanExtraGlide = false;

    public float m_fGlideTime = 0.5f;
    private float m_fGlideTimer = 0.0f;

    [Header("Audio")]
    [SerializeField] private AudioPlayer m_rJumpAudio;
    [SerializeField] private AudioPlayer m_rWalkAudio;
    [SerializeField] private AudioPlayer m_rGliderAudio;
    [SerializeField] private AudioPlayer m_rSlamAttackAudio;
    [SerializeField] private AudioPlayer m_rTagAudio;

    private Material m_CurrentWalkingSurface = null;    // Reference used to make decisions about audio.
    private bool m_bIsSprinting = false;
    #endregion

    // Start is called before the first frame update
    void Awake() {
        // Lock mouse
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        // Create component references
        m_rCharacterController = GetComponent<CharacterController>();
        //m_rAnimator = GetComponentInChildren<Animator>();
        m_rPAnimationController = GetComponentInChildren<PlayerAnimationController>();
        if (!m_rCameraReference) {
            m_rCameraReference = GameObject.Find("Camera").GetComponent<Camera>();
        }

        // Initialise variables
        m_MovementDirection = Vector3.zero;

        m_fCurrentMovementSpeed = m_fMovementSpeed;

        if (m_rTeleportMarkerPrefab) {
            m_rTeleportMarker = Instantiate(m_rTeleportMarkerPrefab);
            m_rTeleportMarker.SetActive(false);
        }

        // Find UI
        GameObject ui = GameObject.Find("GameUI");
        if (ui) {
            m_rUI = GameObject.Find("GameUI").GetComponent<DisplayStat>();
        }
        else {
            Debug.Log("UI not found");
        }


        if (!m_rInstance) {
            m_rInstance = this;
        }
    }

    // Update is called once per frame
    void Update() {
        if (!GameState.DoesPlayerHaveControl()) {
            return;
        }

        if (m_rCharacterController.isGrounded)
            ResetJump();

        // Calculate movement for the frame
        m_MovementDirection = Vector3.zero;
        HandlePlayerMovement();
        HandlePlayerAbilities();
    }

    private void LateUpdate() {
        if(m_ExternalForce.y <= 0.0f) {
            m_ExternalForce = Vector3.zero;
        }
        if(!m_bExtForceOccuring) {
            m_Velocity.x = SmoothFloatToZero(m_Velocity.x, m_bXSmoothSpeed);
            m_Velocity.z = SmoothFloatToZero(m_Velocity.z, m_bZSmoothSpeed);
        }
        // Handle the player doing a slam attack on the ground
        if(m_rCharacterController.isGrounded && m_bSlamAttack) {
            //m_bSlamAttack = false;
            // Impact animation?
            m_rAnimator.SetBool("Grounded", true);
            //SlamAttackReset();
        }
    }

    // Handles all of the functions that determine the vector to move the player, then move them
    private void HandlePlayerMovement() {
        CalculatePlayerMovement();
        CalculatePlayerRotation();
        ApplyGravity();
        ProcessFloat();
        Jump();
        m_Velocity += m_ExternalForce * Time.deltaTime;

        // Limit vertical velocity
        m_Velocity.y = Mathf.Clamp(m_Velocity.y, -100.0f, 100.0f);
        m_MovementDirection = (m_MovementInput + m_Velocity) * Time.deltaTime;
        m_rAnimator.SetFloat("JumpSpeed", m_Velocity.y);

        // Move the player
        if (m_bSlamAttack && m_bPlummeting) {
            m_rCharacterController.Move(Vector3.down * m_fSlamAttackSpeed * Time.deltaTime);
        }
        else if (m_MovementDirection!= Vector3.zero && !(m_bSlamAttack || m_bPlummeting)) {
            m_rCharacterController.Move(m_MovementDirection);
        }
        
    }

    // Calculate movement
    private void CalculatePlayerMovement() {
        // Check for sprinting
        HandleSprint();

        // Take player input
        m_MovementInput = m_fCurrentMovementSpeed * (m_rCameraReference.transform.right * Input.GetAxis("Horizontal") + m_rCameraReference.transform.forward * Input.GetAxis("Vertical")).normalized;
        m_MovementInput.y = 0.0f;

        if (m_MovementInput.sqrMagnitude == 0) {
            // Idle
            m_rAnimator.ResetTrigger("Walk");
            m_rAnimator.SetTrigger("Idle");
        } else {
            m_rAnimator.ResetTrigger("Idle");
            m_rAnimator.SetTrigger("Walk");
        }
    }

    // Rotates the player to look in the direction they are moving
    private void CalculatePlayerRotation() {
        // Prevent turning when stationary
        Vector3 vecLookDirection = (m_rCameraReference.transform.right * Input.GetAxis("Horizontal") + m_rCameraReference.transform.forward * Input.GetAxis("Vertical")).normalized;
        if (vecLookDirection.sqrMagnitude == 0) {
            return;
        }
        vecLookDirection.y = 0.0f; // Remove y component
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(vecLookDirection), Time.deltaTime * m_fTurnSpeed);
    }

    // Performs a simple jump
    private void Jump() {
        // Handle jump input
        if (Input.GetButtonDown(m_strJumpButton) && !m_bIsFloating && !m_bIsSliding) {
            //Reset glide timer
            m_fGlideTimer = 0.0f;

            //If player is grounded as they press the jump button
            if (m_rCharacterController.isGrounded) {
                m_bInitialJumped = true;    // jump is initial
                m_bCanDoubleJump = true;    // double jump may be used
                m_bCanGlide = false;        // gliding is reset
                m_bCoyoteAllowed = false;   // coyote jump is no longer allowed
                m_Velocity.y = m_fJumpPower;
                m_fGravityMulitplier = 1.0f;
                m_fCoyoteTimer = 0.0f;

                //Play jump audio
                if (m_rJumpAudio)
                    m_rJumpAudio.PlayAudio();
            }
            else {
                // If the player has not done an initial jump (jump on ground)
                if(!m_bInitialJumped) {
                    m_fCoyoteTimer += Time.deltaTime;
                    // Within the defined number of seconds, player is allowed a coyote jump
                    if (m_fCoyoteTimer < m_fCoyoteTime) {
                        m_bCoyoteAllowed = true;
                    }
                    else {
                        m_bCoyoteAllowed = false;
                    }
                }
                // Handle Coyote jumping
                if (m_bCoyoteAllowed && !m_bCanDoubleJump) {
                    //Allow coyote jump
                    m_bInitialJumped = true;        // Set coyote jump as the initial jump
                    m_bCanDoubleJump = true;        // Allow a double jump
                    m_bCoyoteAllowed = false;       // Coyote jump is no longer allowed
                    m_Velocity.y = m_fJumpPower;
                    m_fGravityMulitplier = 1.0f;

                    //Play jump audio
                    if (m_rJumpAudio)
                        m_rJumpAudio.PlayAudio();
                }
                // Handle double jump
                else if (!m_bCoyoteAllowed && m_bCanDoubleJump) {
                    m_bCanDoubleJump = false;       // Double jump is no longer allowed
                    m_bCanExtraGlide = true;        // Allow an extra glide 
                    m_Velocity.y = m_fJumpPower;
                    m_fGravityMulitplier = 1.0f;

                    //Play jump audio
                    if (m_rJumpAudio)
                        m_rJumpAudio.PlayAudio();
                }
                // Handle extra glide
                else if(m_bCanExtraGlide) {
                    m_bCanGlide = true;             // Start gliding
                    m_bCanExtraGlide = false;       // Extra glide is no longer allowed
                }
            }
            // Stop sprinting
            ToggleSprint(false);
        }

        // If player holds down the jump button
        if (Input.GetAxis(m_strJumpButton) != 0) {
            // If player is in the air
            if (!m_rCharacterController.isGrounded) {
                // If cannot currently glide
                if (!m_bCanGlide) {
                    //Handle button held glide through a timer
                    m_fGlideTimer += Time.deltaTime;
                    if (m_fGlideTimer > m_fGlideTime) {
                        m_bCanGlide = true;          // Start gliding
                        m_bCanExtraGlide = false;    // Do not allow an extra glide 
                    }
                }
            }
        }
        else {
            //If the player lets go of the jump button, cancel glide
            m_bCanGlide = false;
            ToggleFloatState(false);
        }
    }

    public void ResetJump()
    {
        m_bCoyoteAllowed = true;
        m_bInitialJumped = false;
        m_bCanDoubleJump = false;
    }

    //Detect if player is able to slide down a steep slope
    void SlideMethod() {
        m_bIsSliding = false;
        m_vSlideDir = Vector3.zero; //Reset slide direction
        if (m_bSteepSlopeCollided) {  //If the player is colliding with a steep slope
            //Check if player if standing on a slope
            if (Physics.Raycast(transform.position, -Vector3.up, out rayHit, 10.0f)) {
                if (Vector3.Angle(rayHit.normal, Vector3.up) > m_rCharacterController.slopeLimit && Vector3.Angle(rayHit.normal, Vector3.up) < 180.0f) {
                    m_bIsSliding = true;
                }
                //If player is stuck on a steep slope while not on the ground, set sliding as true
                else if (transform.position.y - rayHit.point.y >= 0.5f) {
                    m_bIsSliding = true;
                }
            }
        }

        //If player is not facing a slippery object, let player exit slide
        if (m_bIsSliding) {
            if (!Physics.Raycast(transform.position, transform.forward, out rayHit, 2.0f)) {
                //Check if player is trying to move while on a slope and not facing slippery object
                bool playerIsMoving = Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f;
                if (playerIsMoving)
                    m_bIsSliding = false;
            }
            else {
                m_MovementInput = Vector3.zero;
            }
        }

        if (!m_bIsSliding)
            return;

        //If player is able to slide, apply sliding forces
        if (m_rCharacterController.isGrounded && m_bIsSliding) {
            m_ExternalForce = new Vector3(hitNormal.x, -hitNormal.y, hitNormal.z);
            Vector3.OrthoNormalize(ref hitNormal, ref m_ExternalForce);
            m_ExternalForce *= m_fSlideSpeed;
        }
    }

    // Updates the player's vertical velocity to consider gravity
    private void ApplyGravity() {
        // Check if floating
        if (m_bIsFloating) {
            m_fTurnSpeed = 1.0f;
            m_fGravityMulitplier = m_fFloatGravityReduction / 5.0f;
            m_fCurrentMovementSpeed = m_fMovementSpeed - 2.0f;
        }

        //Check if the player is sliding or not
        if(!m_bIsOnMovingPlatform)
            SlideMethod();

        // Accelerate the player
        if (!m_bIsOnMovingPlatform) {
            m_ExternalForce += Physics.gravity * m_fGravityMulitplier;
        }
        else {
            m_ExternalForce.y = 0.0f;
        }

        if (m_rCharacterController.isGrounded) {
            m_rAnimator.SetBool("Grounded", true);
            m_fGravityMulitplier = 1.0f;
            m_Velocity = Vector3.zero;
            m_fTurnSpeed = 15.0f;
            //m_fCurrentMovementSpeed = m_fMovementSpeed;
        }
        else {
            if (!m_bIsOnMovingPlatform) {
                m_rAnimator.SetBool("Grounded", false);
                if (!m_bIsFloating)
                {// && m_Velocity.y < 0.0f
                    m_fGravityMulitplier *= m_fGravityMultiplierRate;
                    m_fGravityMulitplier = Mathf.Clamp(m_fGravityMulitplier, 1.0f, m_fMaxGravityMultiplier);
                    m_fCurrentMovementSpeed = m_fMovementSpeed;
                }
            }
        }
        //m_fVerticalVelocity = Mathf.Clamp(m_fVerticalVelocity, -100.0f, 100.0f);
    }

    // Handles the player floating slowly downwards
    private void ProcessFloat() {
        if (!m_rCharacterController.isGrounded) {
            // The player can start floating after a double jump
            if (m_bCanGlide && m_fFloatTimer == 0.0f) { // Change comparison to < m_fFloatTimer for multiple floats per jump
                ToggleFloatState(true);
            }
            else if (Input.GetButtonUp(m_strJumpButton)) {
                m_bCanGlide = false;
                ToggleFloatState(false);
            }
        }
        // Increment float timer while the player is floating
        if (m_bIsFloating) {
            m_fFloatTimer += Time.deltaTime;
            if (m_fFloatTimer > m_fFloatTime) {
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
        if (m_bIsFloating == _bState) {
            return;
        }
        m_bIsFloating = _bState;
        ToggleGlideScroll(_bState);

        if (m_bIsFloating) {
            // Level out the player's upward velocity to begin gliding
            //m_fVerticalVelocity = 0.0f;

            m_Velocity.y = 0.0f;
            m_rAnimator.SetBool("Glide", true);
            m_rGliderAudio.PlayAudio(0);

            if (m_rGlideTrails[0]) {
                foreach (GameObject trail in m_rGlideTrails) {
                    trail.SetActive(true);
                }
            }
        } else {
            m_rAnimator.SetBool("Glide", false);
            //m_rAnimator.SetTrigger("Jump");
            m_rGliderAudio.PlayAudio(1);

            if (m_rGlideTrails[0]) {
                foreach (GameObject trail in m_rGlideTrails) {
                    trail.SetActive(false);
                }
            }
        }
    }

    // Handles all of the functions that control player abilities
    private void HandlePlayerAbilities() {
        // Check the teleport tether thresholds
        HandleTeleportTethers();

        // Don't process abilities if slamming
        if (m_bSlamAttack) return;
        // Aim switch tag
        AimSwitchTag();

        // Handle placing a teleport marker
        if (Input.GetButtonDown(m_strTeleportMarkerPlaceButton)) {
            // Throw a tag if there is no  held object
            if (!m_rHeldObject && m_rCharacterController.isGrounded) {
                // Place on ground
                m_rAnimator.ResetTrigger("Idle");
                m_rAnimator.ResetTrigger("Walk");
                m_rAnimator.SetTrigger("Pickup");
                PlaceTeleportMarker(transform.position - new Vector3(0, 0.7f, 0));
            } else {
                TagHeldObject();
            }
        }
        // Teleporting to the marker
        else if (Input.GetButtonDown(m_strTeleportButton) && m_bTeleportMarkerDown) {
            m_bWasSwitchLastTeleportCommand = false;
            //TeleportToTeleportMarker();
            if (!m_bTeleportThresholdWarning)
            {
                m_rAnimator.SetTrigger("Teleport");
            }

        }
        // Throw switch tag / switch teleport
        else if (Input.GetButtonUp(m_strSwitchButton)) {
            if (m_rSwitchTarget) {
                if (!m_bSwitchThresholdWarning)
                {
                    m_bWasSwitchLastTeleportCommand = true;
                    m_rAnimator.SetTrigger("Teleport");
                }

                //SwitchWithTarget();
            } else if (!m_rHeldObject) {
                m_rAnimator.SetTrigger("ThrowTag");
            }
        }
        // Attack
        else if (Input.GetButtonDown(m_strAttackButton)  && m_bCanAttack && m_rCharacterController.isGrounded) {
            // Basic attack when on the ground
            m_rAnimator.SetTrigger("Attack");
        }else if(Input.GetButtonDown(m_strAttackButton) && m_bCanAttack && !m_rCharacterController.isGrounded) {
            // Slam attack when in the air
            m_rAnimator.SetTrigger("GroundSlam");
        }

    }

    // Teleports the player directly to a location (Should become called from PlayerAnimationController)
    private IEnumerator TeleportToLocation(Vector3 _vecTargetLocation) {
        yield return new WaitForEndOfFrame();
        Vector3 vecPlayerPosition = transform.position;
        // Play VFX
        TeleportParticles();
        // Update position
        transform.position = _vecTargetLocation;

        GameState.SetPlayerTeleportingFlag(false);
    }

    // Place the teleport marker on the ground
    public void PlaceTeleportMarker(Vector3 _vecPlacementLocation) {
        if (!m_rTeleportMarker) {
            return;
        }
        //m_Animator.SetTrigger("Tag");
        m_rTagAudio.PlayAudio(0);
        m_rTeleportMarker.transform.position = _vecPlacementLocation; // Need to use an offset, perhaps with animation
        // Enable teleport marker
        if (!m_rTeleportMarker.activeSelf) {
            ToggleTeleportMarker(true);
        }
    }

    // Parent the teleport marker to the held object
    private void TagHeldObject() {
        if (!m_rHeldObject) {
            return;
        }
        m_rTeleportMarker.transform.position = m_rHeldObject.transform.position;
        m_rTeleportMarker.transform.SetParent(m_rHeldObject.transform);
        ToggleTeleportMarker(true);
    }

    private void TeleportToTeleportMarker() {
        if (!m_bTeleportMarkerDown || !m_rTeleportMarker || m_rHeldObject || m_bTeleportThresholdWarning) {
            return; // Error animation / noise
        }
        ToggleTeleportScroll(true);
        StartCoroutine(TeleportToLocation(m_rTeleportMarker.transform.position));
        // Disable teleport marker
        ToggleTeleportMarker(false);
    }

    // Trade places with the switch target, then clear the target state
    private void SwitchWithTarget() {
        if (!m_rSwitchTarget || m_bSwitchThresholdWarning) {
            return;
        }
        ToggleTeleportScroll(true);
        TeleportParticles();

        // Switch positions
        Vector3 vecPlayerPosition = transform.position;
        //transform.position = m_rSwitchTarget.transform.position;
        StartCoroutine(TeleportToLocation(m_rSwitchTarget.transform.position));
        StartCoroutine(m_rPAnimationController.GetSwitchMarker.GetComponent<SwitchTagController>().Switch(vecPlayerPosition));

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
        LineRenderer lineRenderer = m_rSwitchTagCrosshair.GetComponent<LineRenderer>();
        Vector3 vecVelocity = lineRenderer.GetPosition(1) - lineRenderer.GetPosition(0);
        heldObjectRb.velocity = vecVelocity.normalized * m_fThrowSpeed;// Mathf.Sqrt(m_fThrowSpeed * m_fThrowSpeed + m_fThrowSpeed * m_fThrowSpeed);//m_fThrowSpeed;

        m_rHeldObject.GetComponent<HoldableItem>().ToggleCollider();
        //heldObjectRb.AddForce(vecVelocity.normalized * m_fThrowSpeed, ForceMode.Acceleration);
        m_rHeldObject = null;
        m_bIsAiming = false;
        m_rSwitchTagCrosshair.SetActive(false); // Consider removing depending on how input will be handled
        // Animation
        m_rAnimator.SetTrigger("Throw");
    }

    // Align the player with a ray towards where the switch tag will go
    private void AimSwitchTag() {
        if (m_rSwitchTarget) {
            return;
        }

        if (Input.GetButton(m_strSwitchButton)) {
            //ToggleAiming(true);
            m_rSwitchTagCrosshair.SetActive(true);
            Vector3 vecCameraRotation = m_rCameraReference.transform.rotation.eulerAngles;
            // Line up with camera
            transform.rotation = Quaternion.Euler(0.0f, vecCameraRotation.y, 0.0f);

        }else if (Input.GetButtonUp(m_strSwitchButton)){
            // Disable 
            m_rSwitchTagCrosshair.SetActive(false);
        }
        
    }

    // Changes parameters for when the player is / is not aiming
    private void ToggleAiming(bool _bState) {
        if (m_bIsAiming == _bState) {
            return;
        }
        m_bIsAiming = _bState;
        if (m_bIsAiming) {
            m_rAnimator.SetTrigger("Aim");
            //m_rProjectileArc.SetActive(true);
        } else {
            m_rAnimator.ResetTrigger("Aim");
            m_rAnimator.SetTrigger("Cancel");
           // m_rProjectileArc.SetActive(false);
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
        m_rAnimator.ResetTrigger("Walk");
        print("SetPlayerVerticalVelocity");
    }

    // Finds the closest holdable object
    private GameObject GetClosestHoldableItem() {
        Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, m_fPickupRadius);
        GameObject nearest = null;
        float fDistanceToNearest = 1000.0f;

        // Iterate through and check distances
        foreach (Collider item in nearbyObjects) {
            if (!item.CompareTag("HoldableItem") || item.transform.position.y > transform.position.y) {
                continue;
            } else {
                float fItemDistance = (item.transform.position - transform.position).sqrMagnitude;
                if (fItemDistance < fDistanceToNearest) {
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

    public void HandleFootsteps() {
        if (m_rCharacterController.isGrounded) {
            //Debug.DrawRay(transform.position, Vector3.down);
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit)) {
                Renderer groundRenderer = hit.collider.GetComponent<Renderer>();
                m_CurrentWalkingSurface = groundRenderer ? groundRenderer.sharedMaterial : null;
            }
            else {
                m_CurrentWalkingSurface = null;
            }
        }
        if (m_MovementInput.sqrMagnitude != 0 && m_CurrentWalkingSurface != null) {
            m_rWalkAudio.PlayAudio(m_CurrentWalkingSurface);
        }
    }

    public void PlayGliderSound(bool _start) {
        if (_start)
            m_rGliderAudio.PlayAudio(0);
        else
            m_rGliderAudio.PlayAudio(1);
    }

    // Adds an external force to the player this frame
    public void AddExternalForce(Vector3 _vecExternalForce) {
        if(_vecExternalForce == Vector3.zero) {
            m_bExtForceOccuring = false;
        }
        else {
            m_bExtForceOccuring = true;
            //Make force less than half if theres an existing force in that direction
            if (m_Velocity.x != 0) _vecExternalForce.x /= 2;
            if (m_Velocity.z != 0) _vecExternalForce.z /= 2;

            m_ExternalForce.x += _vecExternalForce.x;   //Change through ext forces
            m_Velocity.y = _vecExternalForce.y;         //Directly change the y velocity value
            m_ExternalForce.z += _vecExternalForce.z;   //Change through ext forces
        }
    }

    float SmoothFloatToZero(float _floatToReset, float _currSpeed) {
        //Resets float value to 0 slowly over time whether it is negative or positive
        //m_fHorizontalSmoothSpeed decides the amount of time the smoothing takes to reset values to 0
        return Mathf.SmoothDamp(_floatToReset, 0.0f, ref _currSpeed, m_fHorizontalSmoothSpeed);
    }

    private void ToggleTeleportMarker(bool _bState) {
        m_rTeleportMarker.SetActive(_bState);
        m_bTeleportMarkerDown = _bState;
    }

    // Checks if the player has passed through warning and breaking thresholds for teleport markers / switch tags
    private void HandleTeleportTethers() {
        // Check teleport maker
        if (m_bTeleportMarkerDown) {
            float fMarkerDistance = (transform.position - m_rTeleportMarker.transform.position).magnitude;
            // Compare to threshold distances
            if (fMarkerDistance >= m_fTeleportBreakDistance) {
                ToggleTeleportMarker(false);
                m_rTeleportMarker.transform.SetParent(null);
                m_bTeleportThresholdWarning = false;
                // Play sound / VFX
                //m_rPlayerAudioController.TeleportThresholdBreak();
            }
            else if (fMarkerDistance >= m_fTeleportTetherDistance && !m_bTeleportThresholdWarning) {
                m_bTeleportThresholdWarning = true;
                // Play sound / VFX
                //m_rPlayerAudioController.TeleportThresholdWarning();
            }
            if (fMarkerDistance < m_fTeleportTetherDistance) {
                m_bTeleportThresholdWarning = false;
            }
        }


        // Check switch tag
        if (m_rSwitchTarget) {
            float fSwitchTagDistance = (transform.position - m_rSwitchTarget.transform.position).magnitude;
            // Compare to threshold distances
            if (fSwitchTagDistance >= m_fTeleportBreakDistance) {
                Debug.Log("Switch tag beyond break distance");
                m_bSwitchThresholdWarning = false;
                m_rSwitchTarget = null;
                // Play sound / VFX
                m_rTagAudio.PlayAudio(2);

                //m_rPlayerAudioController.TeleportThresholdBreak();
                m_rPAnimationController.GetSwitchMarker.GetComponent<SwitchTagController>().DetachFromObject();
            }
            else if (fSwitchTagDistance >= m_fTeleportTetherDistance && !m_bSwitchThresholdWarning) {
                Debug.Log("Switch tag beyond use distance");
                m_bSwitchThresholdWarning = true;
                m_rTagAudio.PlayAudio(1);


                // Play sound / VFX
                //m_rPlayerAudioController.TeleportThresholdWarning();
            }
            if (fSwitchTagDistance < m_fTeleportTetherDistance) {
                m_bSwitchThresholdWarning = false;
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        hitNormal = hit.normal;
    }

    public void SetIsOnSlipperyObject(bool _slippery)
    {
        m_bSteepSlopeCollided = _slippery;
    }

    public void ResetGravityMultiplier() {
        m_fGravityMulitplier = 1.0f;
    }

    // Removes all external forces
    public void ClearExternalForces() {
        m_ExternalForce = Vector3.zero;
    }

    // Resets the float timer - used to provide infinite floating
    public void ResetFloatTimer() {
        m_fFloatTimer = 0.0f;
    }
    public bool IsFloating() {
        return m_bIsFloating;
    }

    // Toggles whether the player should be sprinting
    private void ToggleSprint(bool _bSprinting) {
        if (_bSprinting) {
            //m_rAnimator.speed = 2.0f;
            m_rAnimator.SetBool("Running", true);
            m_fCurrentMovementSpeed = m_fMovementSpeed * m_fSprintMultiplier;
            m_bIsSprinting = true;
        }
        else {
            //m_rAnimator.speed = 1.0f;
            m_rAnimator.SetBool("Running", false);
            m_fCurrentMovementSpeed = m_fMovementSpeed;
            m_bIsSprinting = false;
        }
    }

    public void ToggleWading(bool _bIsWading) {
        if (_bIsWading) {
            m_fCurrentMovementSpeed = m_fMovementSpeed * m_fWaterSlowMultiplier;
            m_bIsWading = true;
        }
        else {
            m_fCurrentMovementSpeed = m_fMovementSpeed;
            m_bIsWading = false;
        }
    }

    public bool GetIsWading()
    {
        return m_bIsWading;
    }

    private void HandleSprint() {
        if((Input.GetButton(m_strSprintButton) || Input.GetKeyDown(KeyCode.LeftShift)) && m_rCharacterController.isGrounded) {
            ToggleSprint(true);
        }else if (Input.GetButtonUp(m_strSprintButton) || Input.GetKeyUp(KeyCode.LeftShift)){
            ToggleSprint(false);
        }
    }

    public void SetOnMovingPlatform(bool _onPlatform) {
        m_bIsOnMovingPlatform = _onPlatform;
        //transform.localScale = Vector3.one;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("SlipperyObject"))
        {
            SetIsOnSlipperyObject(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SlipperyObject"))
        {
            SetIsOnSlipperyObject(false);
        }
    }

    // Forces a cooldown for the player attack
    public IEnumerator AttackCooldown() {
        m_bCanAttack = false;
        yield return new WaitForSeconds(m_fAttackCooldown);
        m_bCanAttack = true;
    }

    public void UpdateHealth() {
        int iHealth = GetComponent<DamageController>().iCurrentHealth;
        m_rUI.NewHealth(iHealth);
    }

    // Turn the glide scroll on / off
    private void ToggleGlideScroll(bool _bState) {
        if (m_rGlideScroll) {
            m_rGlideScroll.SetActive(_bState);
        }
    }

    // Turn the teleport scroll on / off
    public void ToggleTeleportScroll(bool _bState) {
        if (m_rTeleportScroll) {
            m_rTeleportScroll.SetActive(_bState);
        }
    }

    // External trigger for teleportation transition based on animation state
    public void TeleportationTransition() {
        // Determine which teleport command to execute
        if (m_bWasSwitchLastTeleportCommand) {
            SwitchWithTarget();
        }
        else {
            TeleportToTeleportMarker();
        }
    }

    // Force the player to Respawn
    public void RespawnPlayer() {
        GetComponent<DamageController>().ResetDamage();
        Switchable.ResetAllPositions();
        GameObject respawnController = GameObject.Find("RespawnController");

        if (respawnController) {
            respawnController.GetComponent<RespawnController>().RespawnPlayer();
        }

    }

    // Check if the player was damaged by a goon, stealing a map fragment from them if they have one
    private void StealMapFragment(EnemyController _Goon) {
            // Check if the player has swag to steal
            if(GameStats.s_iMapsBoard[GameStats.s_iLevelIndex] > 0) {
                // Award a map fragment to the goon
                --GameStats.s_iMapsBoard[GameStats.s_iLevelIndex];
                _Goon.ToggleMapFragment(true);
            }
    }

    // Slam attack begin - first stage
    public void SlamAttackBegin() {
        m_bSlamAttack = true;
        ToggleFloatState(false);
        m_Velocity = Vector3.zero;
        m_bCanAttack = false;
    }

    // Slam attack middle - damage stage
    public void SlamAttackMiddle() {
        if (m_rSlamAttack) {
            m_rSlamAttack.SetActive(true);
            m_rSlamAttack.GetComponent<MeleeAttack>().m_bIsActive = true;
            m_bPlummeting = true;
            m_rSlamAttackAudio.PlayAudio();
        }
    }

    // Slam attack end - impact and reset
    public void SlamAttackReset() {
        if (m_rSlamAttack) {
            m_rSlamAttack.SetActive(false);
        }

        m_bCanAttack = true;
        // Clear slam attack flag
        m_bSlamAttack = false;
        m_bPlummeting = false;
        // Player is grounded
        m_rAnimator.SetBool("Grounded", true);
        m_rAnimator.ResetTrigger("GroundSlam");

    }

    // Message events
    public void OnReceiveMessage(MessageType _eMessageType, object _message) {
        switch (_eMessageType) {
            case MessageType.eDamageMessage: {
                // If the player was damaged, check to see if they were damaged by a goon.
                DamageMessage message = (DamageMessage)_message;
                EnemyController goon = message.source.GetComponentInParent<EnemyController>();
                if (goon) {
                    // If so, reward the goon with a map fragment
                    StealMapFragment(goon);
                }
                break;
            }

            default: break;
        }
    }
}