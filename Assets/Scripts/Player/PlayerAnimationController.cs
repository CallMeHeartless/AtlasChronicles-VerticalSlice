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
    private PlayerController m_PlayerController;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerController = transform.root.GetComponent<PlayerController>();
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

        // DEBUG
        //print("Rotation: " + m_SwitchMarker.transform.rotation.eulerAngles + " Forward: " + m_SwitchMarker.transform.forward);
        //print("Forward: " + m_SwitchMarker.transform.forward);

        // Make active
        m_SwitchMarker.SetActive(true);
        m_SwitchMarker.GetComponent<SwitchTagController>().SetMoving(true);
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
        m_PlayerController.HandleFootsteps();
    }

    public void PlayGlideStart(bool _start)
    {
        m_PlayerController.PlayGliderSound(_start);
    }
}
