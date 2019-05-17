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

    public void Tag() {
        if (m_bChangeMaterialOnTag) {
            GetComponentInChildren<MeshRenderer>().material = m_AlternateMaterial;
        }
    }

    public void DeTag() {
        if (m_bChangeMaterialOnTag) {
            GetComponentInChildren<MeshRenderer>().material = m_Material;
        }
    }

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
