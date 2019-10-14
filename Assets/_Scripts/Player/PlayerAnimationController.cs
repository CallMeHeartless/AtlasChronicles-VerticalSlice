using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    // External References
    [SerializeField]
    private GameObject m_SwitchMarkerPrefab;
    [SerializeField]
    private TeleportTetherController m_rTeleportTetherController;
    private GameObject m_SwitchMarker;
    private Animator m_rAnimator;

    private GameObject m_AttackCollider;
    [SerializeField]
    private GameObject m_HandCollider;
    private PlayerController m_rPlayerController;
    [SerializeField]
    private MeleeAttack m_rAttack;
    [SerializeField]
    private ParticleSystem m_rGroundSlamParticles;
    [SerializeField]
    private ParticleSystem m_rLeftFootDustParticles;
    [SerializeField]
    private ParticleSystem m_rRightFootDustParticles;

    // Start is called before the first frame update
    void Start()
    {
        m_rAnimator = GetComponent<Animator>();

        if(m_rAnimator == null) {
            Debug.LogError("ERROR: Animator could not be found on PlayerAnimationController GameObject.");
        }

        m_rPlayerController = transform.root.GetComponent<PlayerController>();
        if(m_rPlayerController == null) {
            Debug.LogError("ERROR: PlayerAnimationController could not find parent PlayerContoller.");
        }
        if (m_SwitchMarkerPrefab) {
            m_SwitchMarker = GameObject.Instantiate(m_SwitchMarkerPrefab);
            m_SwitchMarker.SetActive(false);
            SwitchTagController switchTag = m_SwitchMarker.GetComponent<SwitchTagController>();
            switchTag.SetPlayerReference(transform.root.GetComponent<PlayerController>());
            if (m_rTeleportTetherController) {
                switchTag.teleportTether = m_rTeleportTetherController;
            }
        } else {
            Debug.LogError("ERROR: PlayerAnimationController - Switch Marker Prefab not set. Null reference exception");
        }
    }

    // Throws the switch tag
    public void ThrowSwitchTag() {
        // Remove from any existing objects
        m_SwitchMarker.GetComponent<SwitchTagController>().DetachFromObject();
        // Update transform
        m_SwitchMarker.transform.position = m_HandCollider.transform.position;
        m_SwitchMarker.transform.rotation = transform.rotation;

        // Make active
        m_SwitchMarker.SetActive(true);
        m_SwitchMarker.GetComponent<SwitchTagController>().SetMoving(true);
    }

    // Removes the switch tag
    public void RemoveSwitchTag() {
        m_SwitchMarker.GetComponent<SwitchTagController>().DetachFromObject();
        m_SwitchMarker.SetActive(false);
    }


    public GameObject GetSwitchMarker {
        get {
            return m_SwitchMarker;
        }
    }

    public void PlaceTeleportMarker() {

    }

    public void PlayStep() {
        if(m_rPlayerController)
        {
            m_rPlayerController.HandleFootsteps();
        }
    }

    public void PlayLandDust()
    {
        m_rLeftFootDustParticles.Play();
        m_rRightFootDustParticles.Play();
    }

    public void PlayFootDust(int _leftFoot)
    {
        if (!m_rPlayerController)
            return;
        if (!m_rPlayerController.GetComponent<CharacterController>().isGrounded || m_rPlayerController.GetIsWading())
            return;

        if(_leftFoot == 0)
        {
            m_rLeftFootDustParticles.Play();
        }
        else
        {
            m_rRightFootDustParticles.Play();
        }
    }

    public void StartAttack() {
        m_rAttack.m_bIsActive = true;
        m_rPlayerController.ToggleWeaponScroll(true);
    }

    public void EndAttack() {
        m_rAttack.m_bIsActive = false;
        m_rAnimator.SetTrigger("AttackReturn");
        //m_rPlayerController.ToggleWeaponScroll(false);
    }

    public void AttackScrollOff() {
        m_rPlayerController.ToggleWeaponScroll(false);
    }

    public void AttackCooldown() {
        StartCoroutine(m_rPlayerController.AttackCooldown());
    }

    public void DisableTeleportScroll() {
        if (m_rPlayerController)
            m_rPlayerController.ToggleTeleportScroll(false);
    }

    // Disable player control during teleportation
    public void BeginTeleportation() {
        GameState.SetPlayerTeleportingFlag(true);
        if (m_rPlayerController) {
            m_rPlayerController.ToggleTeleportScroll(true);
        }
    }

    // External cue to perform the teleportation transition
    public void TeleportationTransition() {
        if (m_rPlayerController) {
            m_rPlayerController.TeleportationTransition();
        } else {
            Debug.LogError("CRITICAL ERROR: PlayerAnimationController is missing PlayerController reference.");
        }

    }

    // Trigger first part of slam attack
    public void SlamAttackBegin() {
        if (m_rPlayerController)
            m_rPlayerController.SlamAttackBegin();
    }

    // Trigger middle of slam attack
    public void SlamAttackMiddle() {
        if (m_rPlayerController)
            m_rPlayerController.SlamAttackMiddle();
    }

    // Trigger slam attack finish
    public void SlamAttackReset() {
        if (m_rPlayerController)
            m_rPlayerController.SlamAttackReset();
    }

    // Play slam particles
    public void PlaySlamParticles() {
        m_rGroundSlamParticles.Play();
    }

    public void ActivateScroll(int _activate)
    {
        if (m_rPlayerController)
            m_rPlayerController.ToggleGlideScroll((_activate == 1 ? true : false));
    }
}
