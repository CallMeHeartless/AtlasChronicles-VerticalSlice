using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using MessageSystem;
using UnityEngine.Audio;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour, IMessageReceiver {

    // External references
    [Header("External References")]
    [SerializeField]
    private Camera m_rCameraReference;
    private CinemachineFreeLook m_rFreeLook;
    [SerializeField]
    private GameObject m_rSwitchTagCrosshair;
    private PlayerAudioController m_rPlayerAudioController;
    private DisplayStat m_rUI;
    public Vector3 m_rRespawnLocation;
    [SerializeField]
    private PointOfInterestController m_rPointOfInterestController;

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
    private string m_strSwitchLaunchButton = "XBoxR2";
    private string m_strTeleportMarkerPlaceButton = "L1";
    private string m_strTeleportButton = "R1";
    private string m_strAimHeldObjectButton = "XBoxR2";
    private string m_strPickupItemButton = "L1";
    private string m_strSprintButton = "BButton";
    private string m_strAttackButton = "XBoxXButton";
    private string m_strCameraLockButton = "XBoxR2";
    private string m_strTetherBreakButton = "XBoxRightStickClick";
    private string m_strYAxisButton = "RightYAxis";
    private string m_strXAxisButton = "RightXAxis";
    private string m_strMapVision = "YButton";
    private string m_strCheatYeet = "Zero";
    private AxisToButton m_rSwitchButton = new AxisToButton();
    private AxisToButton m_rSwitchLaunchButton = new AxisToButton();

    // Movement variables
    [Header("Movement Variables")]
    [SerializeField]
    private float m_fMovementSpeed;
    [SerializeField]
    private float m_fSprintMultiplier = 1.75f;
    [SerializeField]
    private float m_fWaterSlowMultiplier = 0.5f;
    [SerializeField]
    private GameObject m_fWaterParticles;
    private bool m_bIsWading = false;
    private float m_fCurrentMovementSpeed;
    [SerializeField]
    private float m_fPlayerTurnSpeed = 15.0f;
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
    //private float m_fExternal = 0.0f;
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
    [SerializeField]
    private GameObject[] m_rFootDust;

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
    [SerializeField][Tooltip("The distance at which teleport markers are removed")]
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
    private bool m_bWasSwitchLastTeleportCommand = false; // An internal flag to determine if the most recent teleport command was to switch or teleport to the marker
    private bool m_bMapVisionOn = false;

    [Header("Scroll Objects | Effects")]
    [SerializeField]
    private GameObject m_rTeleportParticles;
    [SerializeField]
    private GameObject m_rTeleportScroll;
    [SerializeField]
    private GameObject m_rGlideScroll;
    [SerializeField]
    private GameObject m_rHipScroll;
    [SerializeField]
    private GameObject m_rWeaponScroll;

    [Header("Slide Detection Variables")]
    [SerializeField] private bool m_bAllowedToSlide = false;
    [SerializeField] private bool m_bIsSliding = false;
    [SerializeField] bool m_bIsOnMovingPlatform = false;
    private RaycastHit rayHit;
    private Vector3 m_hitNormal; //orientation of the slope.
    private Vector3 m_vSlideDir;
    private float rayDistance = 0.0f;
    private bool m_bSlipperySlopeCollided = false;
    private bool m_bStandingOnSlope = false; // is on a slope or not
    private float m_fSlideAngle = 0.0f;
    private bool m_bAllowYeetSlide = false;
    private GameObject m_rCheatSlide;

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
    [SerializeField] private GameObject m_rCineManagerPrefab;

    [Header("Audio")]
    [SerializeField] private AudioPlayer m_rJumpAudio;
    [SerializeField] private AudioPlayer m_rWalkAudio;
    [SerializeField] private AudioPlayer m_rGliderAudio;
    [SerializeField] private AudioPlayer m_rSlamAttackAudio;
    [SerializeField] private AudioPlayer m_rTagAudio;
    [SerializeField] private AudioPlayer m_rDamagedAudio;
    [SerializeField] private AudioSource m_rCollectableAudio;
    [SerializeField] private AudioSource m_rLastCollectedAudio;

    //Collectable Audio pitch shift variables
    [SerializeField] private float m_fIncreasePitch = 0.12f;
    [SerializeField] float m_fMaxPitch = 1.1f;
    private float m_fCurrentPitch = 0.55f;
    private float m_fInitPitch = 0.6f;
    private float m_fCurrentCollectionTime = 1.0f;
    private float m_fMaximumCollectionTime = 2.0f;
    private bool m_bCurrentlyCollecting = false;

    private Material m_CurrentWalkingSurface = null;    // Reference used to make decisions about audio.
    private bool m_bIsSprinting = false;
    private int m_iWeight = 3;
    private bool m_bCineGroundCheck = false;
    #endregion

    // Start is called before the first frame update
    void Awake() {
        // Lock mouse
        Cursor.lockState = CursorLockMode.Locked;

        // Create component references
        m_rCharacterController = GetComponent<CharacterController>();
        m_rPAnimationController = GetComponentInChildren<PlayerAnimationController>();
        if (!m_rCameraReference) {
            m_rCameraReference = GameObject.Find("Camera").GetComponent<Camera>();
        }
        m_rFreeLook = m_rCameraReference.gameObject.GetComponentInParent<CinemachineFreeLook>();
        m_rCheatSlide = GameObject.FindGameObjectWithTag("CheatSlide");
        m_rPointOfInterestController = m_rFreeLook.GetComponent<PointOfInterestController>();

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
            Debug.LogError("UI not found");
        }

        // Initialise trigger to button
        m_rSwitchButton.m_strAxis = m_strSwitchButton;
        m_rSwitchLaunchButton.m_strAxis = m_strSwitchLaunchButton;

        // Set static instance for ease of reference
        if (!m_rInstance) {
            m_rInstance = this;
        }

        m_fWaterParticles.SetActive(false);
        m_rWeaponScroll.SetActive(false);

        m_fCurrentPitch = m_fInitPitch;
    }

    // Update is called once per frame
    void Update() {

        if (transform.parent != null)
        {

        }
        else
        {
            if (transform.localScale != Vector3.one)
            {
                transform.localScale = Vector3.one;
            }
        }

        if (!GameState.DoesPlayerHaveControl()) {
            ClearPlayerEvents();
            return;
        }

        // Update L2
        m_rSwitchButton.Update();
        m_rSwitchLaunchButton.Update();

        // Check if the character is grounded to reset jump count
        if (m_rCharacterController.isGrounded)
            ResetJump();

        // Calculate movement for the frame
        m_MovementDirection = Vector3.zero;
        HandlePlayerMovement();
        HandlePlayerAbilities();

        UpdateCollectableAudio();

        // Debug
        //if (Input.GetKeyDown(KeyCode.L)) {
        //    AddMapToSatchel();
        //}
        //if (Input.GetKeyDown(KeyCode.K)) {
        //    m_rAnimator.SetTrigger("LoseMap");
        //}
    }

    private void LateUpdate() {
        // Clear external force
        if(m_ExternalForce.y <= 0.0f) {
            m_ExternalForce = Vector3.zero;
        }
        // Simulate horizontal friction
        if(!m_bExtForceOccuring) {
            m_Velocity.x = SmoothFloatToZero(m_Velocity.x, m_bXSmoothSpeed);
            m_Velocity.z = SmoothFloatToZero(m_Velocity.z, m_bZSmoothSpeed);
        }

        // Disable map vision if the player tried to move // Kerry - NOTE: Remove this if the mode of map vision is reverted
        if (m_bMapVisionOn && m_MovementInput.sqrMagnitude > 0.0f) {
            HandleMapVision();
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
        if (Input.GetButtonDown(m_strJumpButton) && !m_bIsFloating && !m_bAllowedToSlide) {
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
            ToggleGlideScroll(false);

            m_bCanGlide = false;
            ToggleFloatState(false);
        }
    }

    //Resets variables related to jumping
    public void ResetJump()
    {
        m_bCoyoteAllowed = true;
        m_bInitialJumped = false;
        m_bCanDoubleJump = false;
    }

    //Detect if player is able to slide down a steep slope
    void SlideMethod() {
        //Reset sliding and slide direction
        m_bAllowedToSlide = false;
        m_vSlideDir = Vector3.zero;
        m_fSlideAngle = 0.0f;

        //ONLY slide if the player has reached a peak from jumping to prevent unrealistic sliding behaviour
        float jumpState = m_rAnimator.GetFloat("JumpSpeed");
        //Dont slide if the player is just starting to fall/at the peak of their jump
        if (jumpState > 1.0f)
            return;
        //Make a bool to check whether the player is handing off a slippery object or not
        bool hanging = false;

        //If the player is physically colliding with a slippery object
        if (m_bSlipperySlopeCollided) {  
            //Check if player is standing on a slope //Checks below player
            if (Physics.Raycast(transform.position, -Vector3.up, out rayHit, 10.0f)) {
                //Set the slide angle to check later
                m_fSlideAngle = Vector3.Angle(rayHit.normal, Vector3.up);

                //Check if the slope is bigger than the character's slope limit
                if (Vector3.Angle(rayHit.normal, Vector3.up) > m_rCharacterController.slopeLimit 
                 || Vector3.Angle(rayHit.normal, Vector3.up) > 180.0f) {
                    m_bAllowedToSlide = true;
                }
                else if (transform.position.y - rayHit.point.y >= 1.0f) {
                    //If player happens to be stuck on a steep slope while not on the ground, set sliding as true
                    hanging = true;
                    m_bAllowedToSlide = true;
                }
                else {
                    //Player is not on a slope so do not activate slide
                    m_bAllowedToSlide = false;
                    return;
                }
            }
        }

        //If the player is allowed to slide
        if (m_bAllowedToSlide) {
            //If player is not facing a slippery object, let player exit slide
            if (!Physics.Raycast(transform.position, transform.forward, out rayHit, 2.0f)) {
                //Check if player is trying to move while on a slope and not facing slippery object
                bool playerIsMoving = Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f;
                if (playerIsMoving) {
                    m_bAllowedToSlide = false;
                }
            }
            else {
                if (hanging) {
                    //If the player IS facing the slippery object 
                    //      WHILE hanging on a really steep slope slide. 
                    //      (slope cant be detected via feet)
                    m_ExternalForce = new Vector3(m_hitNormal.x, -0.2f, m_hitNormal.z);
                }
                else {
                    //If the player is on a slippery object while standing on a regular slope, slide.
                    m_ExternalForce = new Vector3(m_hitNormal.x, -m_hitNormal.y, m_hitNormal.z);
                }

                //Apply external force to force player to slide
                Vector3.OrthoNormalize(ref m_hitNormal, ref m_ExternalForce);
                m_ExternalForce *= m_fSlideAngle;

                //Set player as sliding
                m_bIsSliding = true;

                //Player should not be able to be able to move towards the slippery object.
                m_MovementInput = Vector3.zero;
            }
        }
        else {
            //Set player as not sliding
            m_bIsSliding = false;
        }
    }

    // Updates the player's vertical velocity to consider gravity
    private void ApplyGravity() {
        
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            m_bAllowYeetSlide = !m_bAllowYeetSlide;
        }
        m_rCheatSlide?.SetActive(m_bAllowYeetSlide);

        // Check if floating
        if (m_bIsFloating) {
            if(m_bIsSliding && !m_bAllowYeetSlide)
            {
                //sliding while gliding
                m_fGravityMulitplier = 5.0f;
            }
            else
            {
                m_fTurnSpeed = 1.0f;
                m_fGravityMulitplier = m_fFloatGravityReduction / 5.0f;
                m_fCurrentMovementSpeed = m_fMovementSpeed - 2.0f;
            }
        }

        //Check if the player is sliding or not
        if (!m_bIsOnMovingPlatform)
            SlideMethod();

        // Accelerate the player
        if (!m_bIsOnMovingPlatform) {
            m_ExternalForce += Physics.gravity * m_fGravityMulitplier;
        }
        else {
            m_ExternalForce.y = 0.0f;
        }

        if (m_rCharacterController.isGrounded || m_bCineGroundCheck) {
            m_rAnimator.SetBool("Grounded", true);
            m_fGravityMulitplier = 1.0f;
            m_Velocity = Vector3.zero;
            m_fTurnSpeed = m_fPlayerTurnSpeed;
            //m_fCurrentMovementSpeed = m_fMovementSpeed;
        }
        else {
            if (!m_bIsOnMovingPlatform) {
                m_rAnimator.SetBool("Grounded", false);
                if (!m_bIsFloating)
                {// && m_Velocity.y < 0.0f
                    //m_fGravityMulitplier *= m_fGravityMultiplierRate;
                    m_fGravityMulitplier *= (m_Velocity.y < 0.0f ? m_fGravityMultiplierRate * 3.0f : m_fGravityMultiplierRate);
                    m_fGravityMulitplier = Mathf.Clamp(m_fGravityMulitplier, 1.0f, m_fMaxGravityMultiplier);
                    m_fCurrentMovementSpeed = m_fMovementSpeed;
                }
            }
        }
    }

    // Handles the player floating slowly downwards
    private void ProcessFloat() {
        if (!m_rCharacterController.isGrounded) {
            // The player can start floating after a double jump
            if (m_bCanGlide && m_fFloatTimer < m_fFloatTime) { // Change comparison to < m_fFloatTime for multiple floats per jump //== 0.0f
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
        if(!_bState)
        {
            ToggleGlideScroll(_bState);
        }

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
        if (Input.GetButtonDown(m_strTeleportMarkerPlaceButton) && m_rCharacterController.isGrounded) {
            // Place on ground
            m_rAnimator.ResetTrigger("Idle");
            m_rAnimator.ResetTrigger("Walk");
            m_rAnimator.SetTrigger("PlaceTag");
            PlaceTeleportMarker(transform.position - new Vector3(0, 0.7f, 0)); 
        }
        // Teleporting to the marker
        else if (Input.GetButtonDown(m_strTeleportButton) && m_bTeleportMarkerDown) {
            m_bWasSwitchLastTeleportCommand = false;
            //TeleportToTeleportMarker();
            if (!m_bTeleportThresholdWarning) {
                m_rAnimator.SetTrigger("Teleport");
            }

        }
        // Throw switch tag (while aiming)
        else if (m_rSwitchButton.GetCurrentState() == AxisToButton.InputState.FirstReleased || Input.GetButtonUp("SwitchTagKeyboard")) {// Input.GetButtonUp("SwitchTagKeyboard") ||
            m_rAnimator.SetTrigger("ThrowTag");

        }
        // Teleport to switch tag // Kerry
        else if (m_rSwitchLaunchButton.GetCurrentState() == AxisToButton.InputState.FirstPressed || Input.GetButtonDown("SwitchTagLaunchKeyboard")) {
            if (m_rSwitchTarget && !m_bSwitchThresholdWarning) { // Check that there is a valid target and it is within range
                m_bWasSwitchLastTeleportCommand = true;
                m_rAnimator.SetTrigger("Teleport");
            }
        }

        // Allow the player to manually cancel their switch tag
        else if (Input.GetButtonDown(m_strTetherBreakButton)) {
            CancelSwitchTag();
        }
        // Handle map vision
        else if (Input.GetButtonDown(m_strMapVision)) { /// Kerry
            HandleMapVision();
        }
        // Attack
        if (Input.GetButtonDown(m_strAttackButton)  && m_bCanAttack && m_rCharacterController.isGrounded) {
            // Basic attack when on the ground
            m_rAnimator.SetTrigger("Attack");
            //// Adds a mixing transform using a path instead
            //Transform mixTransform = transform.Find("Base HumanLLegThigh");

            //// Add mixing transform
            //anim["wave_hand"].AddMixingTransform(mixTransform);
        }
        else if(Input.GetButtonDown(m_strAttackButton) && m_bCanAttack && !m_rCharacterController.isGrounded) {
            // Slam attack when in the air
            ToggleGlideScroll(false);
            m_rAnimator.SetTrigger("GroundSlam");

        }

    }

    // Teleports the player directly to a location (Should become called from PlayerAnimationController)
    private IEnumerator TeleportToLocation(Vector3 _vecTargetLocation) {
        yield return new WaitForEndOfFrame();
        m_rCharacterController.enabled = false;
      
        // Play VFX
        TeleportParticles();
        // Update position
        transform.position = _vecTargetLocation;

        GameState.SetPlayerTeleportingFlag(false);
        m_rCharacterController.enabled = true;
    }

    // Place the teleport marker on the ground
    public void PlaceTeleportMarker(Vector3 _vecPlacementLocation) {
        if (!m_rTeleportMarker) {
            return;
        }

        // Reset the marker to default rotation / no parent
        m_rTeleportMarker.transform.SetParent(null);
        m_rTeleportMarker.transform.rotation = Quaternion.identity;

        m_rTagAudio.PlayAudio(0); // play audio
        m_rTeleportMarker.transform.position = _vecPlacementLocation; // Need to use an offset, perhaps with animation
        AttachMarkerToGround();
        // Enable teleport marker
        if (!m_rTeleportMarker.activeSelf) {
            ToggleTeleportMarker(true);
        }
    }

    // Attaches the teleport marker to the 'ground', allowing it to move with objects // By Kerry
    private void AttachMarkerToGround() {
        // Determine a layermask
        int layerMask = LayerMask.NameToLayer("Player") & LayerMask.NameToLayer("AudioBGM") & LayerMask.NameToLayer("Tutorial") & LayerMask.NameToLayer("TagIgnore");
        layerMask = ~layerMask; // Bitwise Inverstion
        RaycastHit hit; // Raycast information

        // Perform raycast check
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 1.0f, layerMask, QueryTriggerInteraction.Ignore)) {
            // Parent the teleport marker
            m_rTeleportMarker.transform.SetParent(hit.transform);
        }
    }

    // This function is used by other objects to prevent the teleport marker from being used in certain situations // Kerry
    public void CancelTeleportMarker() {
        if (!m_rTeleportMarker) {
            return;
        }

        // Disable 
        m_rTeleportMarker.transform.SetParent(null); // Unparent
        m_rTeleportMarker.transform.rotation = Quaternion.identity; // Reset rotation
        ToggleTeleportMarker(false); // Disable visuals
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
        m_rTeleportMarker.transform.SetParent(null);
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
        Vector3 vecObjectPosition = m_rSwitchTarget.transform.position;
        StartCoroutine(TeleportToLocation(vecObjectPosition));
        StartCoroutine(m_rPAnimationController.GetSwitchMarker.GetComponent<SwitchTagController>().Switch(vecPlayerPosition));

        // Remove reference
        m_rSwitchTarget = null;
    }

    // Sets the player controller's switch target
    public void SetSwitchTarget(GameObject _switchTarget) {
        m_rSwitchTarget = _switchTarget;
    }

    // Align the player with the camera and indicate where the switch tag is being aimed
    private void AimSwitchTag() {
        //if (m_rSwitchTarget) {
        //    return;
        //}

        if (m_rSwitchButton.GetCurrentState() == AxisToButton.InputState.Pressed || Input.GetButton("SwitchTagKeyboard")) {//
            ToggleAiming(true);
            Vector3 vecCameraRotation = m_rCameraReference.transform.rotation.eulerAngles;
            // Line up with camera
            transform.rotation = Quaternion.Euler(0.0f, vecCameraRotation.y, 0.0f);
        }
        else {//if(Input.GetButtonUp(m_strSwitchButton) && m_rSwitchButton.GetCurrentState() == AxisToButton.InputState.Released)
            // Disable 
            ToggleAiming(false);
        }   
    }

    // Cancel the switch tag
    public void CancelSwitchTag() {
        if (!m_rSwitchTarget) {
            return;
        }
        SwitchTagController rSwitchTag = m_rPAnimationController.GetSwitchMarker.GetComponent<SwitchTagController>();
        if (rSwitchTag) {
            rSwitchTag.DetachFromObject();
            m_rSwitchTarget = null;
            // VFX / SFX feedback
        }

    }

    // Changes parameters for when the player is / is not aiming
    private void ToggleAiming(bool _bState) {
        if (m_bIsAiming == _bState) {
            return;
        }
        m_bIsAiming = _bState;
        m_rSwitchTagCrosshair.SetActive(m_bIsAiming);
        m_rAnimator.SetBool("IsAiming", m_bIsAiming);
    }

    // Handles map vision /// Kerry
    private void HandleMapVision() {
        // Toggle active state
        m_bMapVisionOn = !m_bMapVisionOn;

        /// The code below toggles map vision via zones
        //Zone.ToggleMapVision(m_bMapVisionOn);

        /// The code below uses the point of interest system
        if (m_rPointOfInterestController) {
            m_rPointOfInterestController.SetPointOfInterest(m_bMapVisionOn);
        }
    }

    // Terminates map vision only, used by external sources /// Kerry
    public void EndMapVision() {
        // Return if map vision is not on
        if (!m_bMapVisionOn) {
            return;
        }
        // Toggle mapvision
        HandleMapVision();
    }
    
    // Triggers the teleport particle effects
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

    // Handles the visual appearing of the teleport marker // Kerry
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
                BreakMarkerTether();
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

    // Break the teleport marker tether
    public void BreakMarkerTether() {
        ToggleTeleportMarker(false);
        m_rTeleportMarker.transform.SetParent(null);
        m_bTeleportThresholdWarning = false;
        // Play sound / VFX
        //m_rPlayerAudioController.TeleportThresholdBreak();
    }

    // Used to remove both switch tag and teleport tethers from the player
    public void BreakTethers() {
        // Break teleport marker
        BreakMarkerTether();
        // Break switch tag tether
        CancelSwitchTag();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        m_hitNormal = hit.normal;
    }

    public void SetIsOnSlipperyObject(bool _slippery)
    {
        m_bSlipperySlopeCollided = _slippery;
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

    public void ClearPlayerEvents()
    {
        //Set player on ground // Used when playing cinematics
        ToggleGlideScroll(false);
        m_rAnimator.SetBool("Glide", false);
        m_rAnimator.SetTrigger("Idle");
        m_rAnimator.SetFloat("JumpSpeed", 0.0f);
        if (m_rGlideTrails[0])
        {
            foreach (GameObject trail in m_rGlideTrails)
            {
                trail.SetActive(false);
            }
        }
        m_rAnimator.SetBool("Grounded", true);
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
            m_fWaterParticles.SetActive(true);
            m_fCurrentMovementSpeed = m_fMovementSpeed * m_fWaterSlowMultiplier;
            m_bIsWading = true;
        }
        else {
            m_fWaterParticles.SetActive(false);

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

    public Animator GetAnimator()
    {
        return m_rAnimator;
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
        if (m_rUI) {
            m_rUI.UpdateHealth(iHealth);
        }
    }

    // Turn the glide scroll on / off
    public void ToggleGlideScroll(bool _bState) {
        if (m_rGlideScroll) {
            m_rGlideScroll.SetActive(_bState);
            ToggleHipScroll(!_bState);
        }
    }

    // Turn the teleport scroll on / off
    public void ToggleTeleportScroll(bool _bState) {
        if (m_rTeleportScroll) {
            m_rTeleportScroll.SetActive(_bState);
            ToggleHipScroll(!_bState);
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

        // Prevent null reference exception if no respawn location has been set
        if (m_rRespawnLocation == null) {
            m_rRespawnLocation = GameObject.FindGameObjectWithTag("spawns").transform.position;
        }

        // Move player
        StartCoroutine(MovePlayer());
    }

    // Check if the player was damaged by a goon, stealing a map fragment from them if they have one
    private void StealMapFragment(EnemyController _Goon) {
        // Check if the player has swag to steal
        if(GameStats.s_iMapsBoard[GameStats.s_iLevelIndex] > 0) {
            // Award a map fragment to the goon
            --GameStats.s_iMapsBoard[GameStats.s_iLevelIndex];
            _Goon.ToggleMapFragment(true);
            GameEndController.CheckMapCollection();

            // Take the map fragment from the player's satchel
            m_rAnimator.SetTrigger("LoseMap");
        }
    }

    // Slam attack begin - first stage
    public void SlamAttackBegin() {
        m_bSlamAttack = true;
        ToggleFloatState(false);
        m_Velocity = Vector3.zero;
        m_bCanAttack = false;
        ToggleWeaponScroll(true);
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
        ToggleGlideScroll(false);

        m_bCanAttack = true;
        // Clear slam attack flag
        m_bSlamAttack = false;
        m_bPlummeting = false;
        // Player is grounded
        m_rAnimator.SetBool("Grounded", true);
        m_rAnimator.ResetTrigger("GroundSlam");
        ToggleWeaponScroll(false);
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
                    m_rDamagedAudio.PlayAudio();
                    //StartCoroutine(JuiceManager.Shake(m_rCameraReference.transform, 1.0f, 2.0f));
                    StealMapFragment(goon);
                }
                break;
            }

            default: break;
        }
    }

    private IEnumerator MovePlayer() {
        m_rCharacterController.enabled = false;
        yield return new WaitForEndOfFrame();
        transform.position = m_rRespawnLocation;
        m_rCharacterController.enabled = true;
    }

    // Toggles the hip scroll item
    public void ToggleHipScroll(bool _bState) {
        if (m_rHipScroll) {
            m_rHipScroll.SetActive(_bState);
        }
    }

    // Toggles the weapon scroll
    public void ToggleWeaponScroll(bool _bState) {
        m_rWeaponScroll.SetActive(_bState);
        m_rHipScroll.SetActive(!_bState);
        if(_bState)
        {
            m_rHipScroll.transform.localScale = Vector3.zero;
        }
        else
        {
            m_rHipScroll.transform.localScale = Vector3.one;
        }
    }
    public int getWeight()
    {
          return m_iWeight;
    }

    public void DisableCameraInput(bool _disable)
    {
        if (_disable)
        {
            m_rFreeLook.m_XAxis.m_InputAxisName = "";
            m_rFreeLook.m_YAxis.m_InputAxisName = "";
        }
        else
        {
            m_rFreeLook.m_XAxis.m_InputAxisName = m_strXAxisButton;
            m_rFreeLook.m_YAxis.m_InputAxisName = m_strYAxisButton;
        }
    }

    public AudioSource GetCollectableAudio()
    {
        return m_rCollectableAudio;
    }

    public void SetCineGroundCheckTrue()
    {
        m_bCineGroundCheck = true;
    }
    public void SetCineGroundCheckFalse()
    {
        m_bCineGroundCheck = false;
    }

    public void AddMapToSatchel() {
        m_rAnimator.SetTrigger("Maps");
    }

    [SerializeField] AudioMixer m_rMixer;

    /// <summary>
    /// Updates collectable audio loguc
    /// </summary>
    public void UpdateCollectableAudio()
    {
        //Only calculate timer if player is currently collecting crystals
        if (!m_bCurrentlyCollecting)
            return;

        //if(m_rLastCollectedAudio != null)
        //{
        //    if (m_fCurrentCollectionTime < 0.1f && m_rLastCollectedAudio.isPlaying)
        //    {
        //        float mixerVal = 0.0f;
        //        m_rMixer.GetFloat("Collectables", out mixerVal);
        //        print("SDHFDKJSHF: " + mixerVal);

        //        m_rMixer.SetFloat("Collectables", 0.5f);

        //        m_rMixer.GetFloat("Collectables", out mixerVal);
        //        print("newSDHFDKJSHF: " + mixerVal);

        //        //m_rMixer.SetFloat("Collectables", );
        //    }
        //    else
        //    {
        //        m_rMixer.SetFloat("Collectables", 0.0f);

        //    }
        //}
        //else
        //{
        //    m_rMixer.SetFloat("Collectables", 0.0f);

        //}

        m_fCurrentCollectionTime += Time.deltaTime;

        //IF collection time exceeds max time for nxt crystal to be collected, reset time
        if (m_fCurrentCollectionTime >= m_fMaximumCollectionTime)
        {
            //Reset the pitch, time and set currently Collecting as false so time counting does not occur when not needed.
            m_fCurrentPitch = m_fInitPitch;
            m_fCurrentCollectionTime = 0.0f;
            m_bCurrentlyCollecting = false;
        }
    }
    
    /// <summary>
    /// Plays the sound of a collectable through the audioSource reference passed through with an increased pitch. 
    /// Called through the Pickup Class. 
    /// </summary>
    /// <param name="_audio">AudioSource passed through from collectable collected</param>
    public void PlayCollectableAudio(ref AudioSource _audio)
    {
        //Set the new pitch in the referenced collectable audioplyer
        _audio.pitch = m_fCurrentPitch;

        m_bCurrentlyCollecting = true;
        m_fCurrentCollectionTime = 0.0f;

        //Increase pitch of audio
        if (m_fCurrentPitch < m_fMaxPitch && (m_fCurrentPitch + m_fIncreasePitch <= m_fMaxPitch))
        {
            //Only increase pitch if current pitch is less than max pitch, including when new pitch is calcualted
            m_fCurrentPitch += m_fIncreasePitch;
        }
        else if(m_fCurrentPitch >= m_fMaxPitch)
        {
            //Set current pitch as max pitch if current pitch is above max
            m_fCurrentPitch = m_fMaxPitch;
        }
        
        StartCoroutine(PlayNextTrackAfterSeconds(_audio, 0.0f));

        m_rLastCollectedAudio = _audio;
    }

    IEnumerator PlayNextTrackAfterSeconds(AudioSource _track, float _seconds)
    {
        yield return new WaitForSeconds(_seconds);
        _track.Play();
    }
}