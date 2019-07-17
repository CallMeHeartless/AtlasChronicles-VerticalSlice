using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(LineRenderer))]
public class ProjectileArc : MonoBehaviour
{
    // External References
    [SerializeField]
    private GameObject m_rCameraReference;
    [SerializeField]
    private CinemachineFreeLook m_rFreeLookReference;

    public LineRenderer m_rLineRenderer;
    [SerializeField]
    private float m_fMinAngle = 10.0f;
    [SerializeField]
    private float m_fMaxAngle = 45.0f;
    [SerializeField]
    private float m_fAngle = 45.0f;
    [SerializeField]
    private float m_fForwardVelocity = 10.0f;
    public int m_iSize;
    public int m_iMaxVelocity;
    private float m_fRotation = 0.0f;

    void Start(){
        // Find references in scene
        m_rFreeLookReference = GameObject.Find("Freelook Camera").GetComponent<CinemachineFreeLook>();
        if (!m_rFreeLookReference) {
            Debug.LogError("ERROR: Free look camera can not be found. Null reference exception");
        }
        m_rCameraReference = m_rFreeLookReference.transform.GetChild(0).gameObject;
    }

    void Update(){
        // Update the arc
        UpdateArcParameters();
        if ((m_iSize != 0)&&(Application.isPlaying)){
            SolveAndProjectArc();
        }

    }

    // Solve the line renderer's coordinates
    void SolveAndProjectArc()
    {
        Vector3[] newLoctation = new Vector3[m_iSize];
        for (int i = 0; i < m_iSize; i++)
        {
            newLoctation[i] = GetArcPoint((float)i/ (float)m_iSize, m_iMaxVelocity);
        }

        m_rLineRenderer.positionCount = newLoctation.Length;
        m_rLineRenderer.SetPositions(newLoctation);
    }

    // Calculates the next point in the spline
    Vector3 GetArcPoint(float i, float max){
        Vector3 currentPoint;
        float rad = Mathf.Deg2Rad * m_fAngle;
        currentPoint.x = i * max;
        currentPoint.y = currentPoint.x * Mathf.Tan(rad) - ((9.81f * currentPoint.x * currentPoint.x) / (2 * m_fForwardVelocity * m_fForwardVelocity * Mathf.Cos(rad) * Mathf.Cos(rad)));

        // Project according to the rotation
        if (m_fRotation != 0){
            currentPoint.z = i * max * Mathf.Cos(m_fRotation);
            currentPoint.x = i * max * Mathf.Sin(m_fRotation);
        }
        else{
            currentPoint.z = 0;
        }
        
        return currentPoint + transform.position;
    }

    // Determine the arc's form based on where the player is looking
    private void UpdateArcParameters() {
        // Get a reference to where the camera is looking to set the arc rotation
        float fCameraYRotation = m_rCameraReference.transform.rotation.eulerAngles.y;
        m_fRotation = fCameraYRotation * Mathf.Deg2Rad;

        float fFreeLookY = m_rFreeLookReference.m_YAxis.Value;
        m_fAngle = Mathf.Lerp(m_fMinAngle, m_fMaxAngle, 1.0f - fFreeLookY);
    }

    // Gets the rotation in degrees
    public float GetArcRotation() {
        return m_fRotation * Mathf.Rad2Deg;
    }

    // Manually sets the rotation (takes degrees)
    public void SetRotation(float _fRotation) {
        m_fRotation = _fRotation * Mathf.Deg2Rad;
    }
}

//if (Input.GetKeyDown(KeyCode.E)) {
//    GameObject newStone = GameObject.Instantiate(stone, transform.position, transform.rotation);
//    float anglechange = Mathf.Tan(Mathf.Deg2Rad * m_fAngle);
//    newStone.GetComponent<Rigidbody>().AddForce(new Vector3(-(m_fForwardVelocity * anglechange) + m_fForwardVelocity * increaser, m_fForwardVelocity * anglechange * increaser, 0), ForceMode.Acceleration);
//}