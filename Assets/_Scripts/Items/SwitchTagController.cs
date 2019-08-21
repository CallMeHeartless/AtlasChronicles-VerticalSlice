using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using cakeslice;

public class SwitchTagController : MonoBehaviour
{
    [SerializeField]
    private float m_fMoveSpeed = 20.0f;
    private bool m_bIsMoving = false;
    private Transform m_AttachedObject = null;
    [SerializeField]
    private Vector3 m_vecOffset;
    private PlayerController m_rPlayerReference;
    [SerializeField]
    private GameObject m_rInkSparkleTest;

    // Update is called once per frame
    void Update(){
        // Move the tag forward if it has been thrown
        if (m_bIsMoving) {
            transform.Translate(Vector3.forward * m_fMoveSpeed * Time.deltaTime);
        }
    }

    // Instructs the tag to fly forwards or not
    public void SetMoving(bool _bState) {
        m_bIsMoving = _bState;
    }

    void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Switchable>() && !m_AttachedObject) {
            // Attach to the object
            //m_rInkSparkle.SetActive(true);
            m_AttachedObject = other.transform;
            transform.position = m_AttachedObject.position + m_vecOffset;
            transform.SetParent(m_AttachedObject);
            m_bIsMoving = false;
            m_rPlayerReference.SetSwitchTarget(m_AttachedObject.gameObject);
            m_AttachedObject.GetComponent<Switchable>().Tag();

            GameObject sparkle = Instantiate(m_rInkSparkleTest, 
                new Vector3(transform.position.x, 
                            transform.position.y, 
                            transform.position.z - 5.0f), 
                Quaternion.identity);

            Destroy(sparkle, 0.5f);
        }
        else if (!other.CompareTag("Player") && !other.transform.root.CompareTag("Player")
            && other.gameObject.layer != LayerMask.NameToLayer("AudioBGM")
            && other.gameObject.layer != LayerMask.NameToLayer("Tutorial")
            && other.gameObject.layer != LayerMask.NameToLayer("Cinematic")
            && other.gameObject.layer != LayerMask.NameToLayer("TagIgnore")) {
            // Destroy animation? DO NOT DESTROY OBJECT
            GameObject sparkle = Instantiate(m_rInkSparkleTest, transform.position, Quaternion.identity);
            Destroy(sparkle, 0.5f);
            gameObject.SetActive(false);
            DetachFromObject();
        }
    }

    // Removes the switch tag from the object
    public void DetachFromObject() {
        transform.SetParent(null);
        if (m_rPlayerReference) {
            m_rPlayerReference.SetSwitchTarget(null);
        }
        if (m_AttachedObject) {
            m_AttachedObject.GetComponent<Switchable>().DeTag();
        }
       
        m_AttachedObject = null;
        gameObject.SetActive(false);
    }
    
    // Places the attached object at the input position, then detaches itself
    public IEnumerator Switch(Vector3 _vecSwitchPosition) {
        yield return new WaitForEndOfFrame();
        // VFX

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
