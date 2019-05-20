using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switchable : MonoBehaviour
{
    [SerializeField]
    private bool m_bChangeMaterialOnTag = false;
    [SerializeField]
    private Material m_AlternateMaterial;
    [SerializeField]
    private Material m_Material;
    private Transform m_OriginalTransform;

    private void Start() {
        //m_Material = GetComponent<MeshRenderer>().material;
        m_OriginalTransform = transform;
    }

    // Called to indicated the switchable object has been tagged
    public void Tag() {
        if (m_bChangeMaterialOnTag) {
            GetComponentInChildren<MeshRenderer>().material = m_AlternateMaterial;
        }
    }

    // Called to indicate that the switchable object has had a tag removed
    public void DeTag() {
        if (m_bChangeMaterialOnTag) {
            GetComponentInChildren<MeshRenderer>().material = m_Material;
        }
    }

    // Returns the switchable object to its original position
    public void ReturnToStartPosition() {
        transform.position = m_OriginalTransform.position;
        transform.rotation = m_OriginalTransform.rotation;

        // Check for rigidbody
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (rigidbody) {
            rigidbody.velocity = Vector3.zero;
        }
        
    }

}
