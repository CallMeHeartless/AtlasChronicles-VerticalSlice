using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwitchTagController : MonoBehaviour
{
    [SerializeField]
    private float m_fMoveSpeed = 20.0f;
    private bool m_bIsMoving = false;
    private Transform m_AttachedObject = null;
    [SerializeField]
    private Vector3 m_vecOffset;
    private PlayerController m_rPlayerReference;
    [SerializeField] private float m_ftimer;
    [SerializeField] private float m_fMaxtimer = 5;
    private GameObject[] m_gTagOfGameObject;
    private int m_Tagslot=0;

    public void SetUp()
    {

        m_Tagslot = m_rPlayerReference.getUsedTeleport();
        if (m_fMaxtimer <= 0)
        {
            m_fMaxtimer = 2;
        }
        m_ftimer = m_fMaxtimer;
    }

    public void OrgialTag(GameObject[] _tag) {
        m_gTagOfGameObject = _tag;
    }
    // Update is called once per frame
    void Update() {

        if (m_ftimer <= 0)
        {
            if (m_bIsMoving)
            {
                //not attacthed to object so deactivate it
                gameObject.SetActive(false);
                m_rPlayerReference.SetTeleportCondiction(m_Tagslot);
            }
        }
        else
        {
            // Move the tag forward if it has been thrown
            if (m_bIsMoving)
            {
                transform.Translate(Vector3.forward * m_fMoveSpeed * Time.deltaTime);
            }
            m_ftimer -= Time.deltaTime;
        }


    }

    // Instructs the tag to fly forwards or not
    public void SetMoving(bool _bState) {
        m_bIsMoving = _bState;

    }

    void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Switchable>() && !m_AttachedObject) {
            // Attach to the object
            m_Tagslot = m_rPlayerReference.getUsedTeleport();
            m_AttachedObject = other.transform;
            transform.position = m_AttachedObject.position + m_vecOffset;
            transform.SetParent(m_AttachedObject);
            m_bIsMoving = false;
            m_rPlayerReference.SetSwitchTarget(m_AttachedObject.gameObject);
            m_AttachedObject.GetComponent<Switchable>().Tag();
        } else if (!other.CompareTag("Player")) {
            // Destroy animation? DO NOT DESTROY OBJECT
            //DetachFromObject();
        }
    }

    // Removes the switch tag from the object
    public void DetachFromObject() {
        transform.SetParent(null);
        if (m_AttachedObject) {
            m_AttachedObject.GetComponent<Switchable>().DeTag();
            m_rPlayerReference.ResetSwitchTarget(m_gTagOfGameObject[m_Tagslot]);
        }

        m_AttachedObject = null;
        gameObject.SetActive(false);
    }

    // Places the attached object at the input position, then detaches itself
    public IEnumerator Switch(Vector3 _vecSwitchPosition) {
        yield return new WaitForEndOfFrame();
        // VFX
        m_Tagslot = m_rPlayerReference.getUsedTeleport();
        // Handle objects with navmesh
        NavMeshAgent agent = m_AttachedObject.GetComponent<NavMeshAgent>();
        if (agent) {
            // Enemy warp animation
            //m_AttachedObject.GetComponentInChildren<>
            agent.Warp(_vecSwitchPosition);
        }
        else {
            m_AttachedObject.position = _vecSwitchPosition;
            Switchable switchable = m_AttachedObject.GetComponent<Switchable>();
            if (switchable) {
                switchable.MoveSwitchable();
            }
        }
        DetachFromObject();
    }

    // Manually sets the player controller reference
    public void SetPlayerReference(PlayerController _playerReference) {
        m_rPlayerReference = _playerReference;
    }
    
}
