using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Switchable : MonoBehaviour
{
    public int Weight =2;
    [SerializeField]
    public weighted.Color ObjectColor;
    [SerializeField]
    private bool m_bChangeMaterialOnTag = false;
    [SerializeField]
    private Material m_AlternateMaterial;
    [SerializeField]
    private Material m_Material;
    private Transform m_OriginalTransform;
    private Vector3 m_StartPosition;
    private EnemyController m_Enemy = null;
    [SerializeField]
    private bool m_bReturnAfterDelay = false;
    private bool m_bHasMoved = false;
    [SerializeField]
    private float m_fReturnTime = 3.0f;
    private float m_fReturnCount = 0.0f;
   

    private void Start() {
        //m_Material = GetComponent<MeshRenderer>().material;
        m_OriginalTransform = transform;
        m_StartPosition = transform.position;
        m_Enemy = GetComponent<EnemyController>();
    }

    private void Update() {
        // Process moving the object back to its start position (if relevent)
        if (m_bReturnAfterDelay && m_bHasMoved) {
            m_fReturnCount += Time.deltaTime;
            if(m_fReturnCount >= m_fReturnTime) {
                m_fReturnCount = 0.0f;
                m_bHasMoved = false;
                ReturnToStartPosition();
            }
        }
    }

    // Called to indicated the switchable object has been tagged
    public void Tag() {
        if (m_bChangeMaterialOnTag) {
            GetComponentInChildren<MeshRenderer>().material = m_AlternateMaterial;
        }
        // Check if this is an enemy
        if (m_Enemy) {
            m_Enemy.SetTag(true);
        }
    }

    // Called to indicate that the switchable object has had a tag removed
    public void DeTag() {
        if (m_bChangeMaterialOnTag) {
            GetComponentInChildren<MeshRenderer>().material = m_Material;
        }
        if (m_Enemy) {
            m_Enemy.SetTag(false);
        }
    }

    // Returns the switchable object to its original position
    public void ReturnToStartPosition() {
        if (GetComponent<NavMeshAgent>()) return;
        transform.position = m_StartPosition;
        transform.rotation = m_OriginalTransform.rotation;

        // Check for rigidbody
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (rigidbody) {
            rigidbody.velocity = Vector3.zero;
        }
        
    }

    // Returns all switchable objects to their original positions
    public static void ResetAllPositions(){
        Switchable[] switchables = GameObject.FindObjectsOfType<Switchable>();
        foreach(Switchable switchable in switchables){
            switchable.ReturnToStartPosition();
        }
    }

    // Used to indicate that a switchable object has been moved - used for objects on a timer
    public void MoveSwitchable() {
        m_bHasMoved = true;
    }
}
