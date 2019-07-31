using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    // External References
    [SerializeField]
    private GameObject m_SwitchMarkerPrefab;
    private GameObject m_SwitchMarker;

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
        m_rPlayerController = transform.root.GetComponent<PlayerController>();
        if (m_SwitchMarkerPrefab) {
            m_SwitchMarker = GameObject.Instantiate(m_SwitchMarkerPrefab);
            m_SwitchMarker.SetActive(false);
            m_SwitchMarker.GetComponent<SwitchTagController>().SetPlayerReference(transform.root.GetComponent<PlayerController>());
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
        m_SwitchMarker.transform.rotation = transform.root.rotation;

        // Make active
        m_SwitchMarker.SetActive(true);
        m_SwitchMarker.GetComponent<SwitchTagController>().SetMoving(true);
        m_SwitchMarker.GetComponent<SwitchTagController>().SetUp();
    }
    public void SetOrgialLoctation(GameObject[] _tag)
    {
        m_SwitchMarker.GetComponent<SwitchTagController>().OrgialTag(_tag);
    }
    // Removes the switch tag
    public void RemoveSwitchTag() {
        m_SwitchMarker.GetComponent<SwitchTagController>().DetachFromObject();
    }


    public GameObject GetSwitchMarker {
        get {
            return m_SwitchMarker;
        }

    }

    public void PlaceTeleportMarker() {

    }

    public void PlayStep() {
        if (m_rPlayerController)
        {
            m_rPlayerController.HandleFootsteps();
            //m_rFootDustParticles.Play();
        }
    }

    public void PlayLandDust()
    {
        m_rLeftFootDustParticles.Play();

        m_rRightFootDustParticles.Play();
    }

    public void PlayFootDust(int _leftFoot)
    {
        if (!m_rPlayerController.GetComponent<CharacterController>().isGrounded || m_rPlayerController.GetIsWading())
            return;

        if (_leftFoot == 0)
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
        m_rPlayerController.ToggleHipScroll(false);
    }

    public void EndAttack() {
        m_rAttack.m_bIsActive = false;
        m_rPlayerController.ToggleWeaponScroll(false);
        m_rPlayerController.ToggleHipScroll(true);
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
        if (m_rPlayerController)
            m_rPlayerController.TeleportationTransition();
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
   
}
