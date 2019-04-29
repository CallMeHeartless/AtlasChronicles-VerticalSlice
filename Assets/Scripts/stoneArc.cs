using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(LineRenderer))]
public class stoneArc : MonoBehaviour
{
    // External References
    [SerializeField]
    private GameObject m_CameraReference;
    [SerializeField]
    private CinemachineFreeLook m_FreeLookReference;

    public LineRenderer lineRend;
    public GameObject stone;
    [SerializeField]
    private float m_fMinAngle = 10.0f;
    [SerializeField]
    private float m_fMaxAngle = 45.0f;
    [SerializeField]
    private float m_fAngle = 45.0f;
    [SerializeField]
    private float m_fForwardVelocity = 10.0f;
    public int size;
    public float increaser;
    public int maxVel;
    private float m_fRotation = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //lineRend.GetComponent<LineRenderer>();
        m_CameraReference = GameObject.Find("Main Camera");
        m_FreeLookReference = GameObject.Find("Freelook Camera").GetComponent<CinemachineFreeLook>();

    }

    // Update is called once per frame
    void Update()
    {

        UpdateArcParameters();
        if ((size != 0)&&(Application.isPlaying))
        {
            SolveAndProjectArc();
        }
        //if (Input.GetKeyDown(KeyCode.E)) {
        //    GameObject newStone = GameObject.Instantiate(stone, transform.position, transform.rotation);
        //    float anglechange = Mathf.Tan(Mathf.Deg2Rad * m_fAngle);
        //    newStone.GetComponent<Rigidbody>().AddForce(new Vector3(-(m_fForwardVelocity * anglechange) + m_fForwardVelocity * increaser, m_fForwardVelocity * anglechange * increaser, 0), ForceMode.Acceleration);
        //}
    }

    // Solve the line renderer's coordinates
    void SolveAndProjectArc()
    {
        Vector3[] newLoctation = new Vector3[size];
        for (int i = 0; i < size; i++)
        {
            newLoctation[i] = GetArcPoint((float)i/ (float)size, maxVel);
        }
       

        lineRend.positionCount = newLoctation.Length;
        lineRend.SetPositions(newLoctation);
    }

    // Calculates the next point in the spline
    Vector3 GetArcPoint(float i, float max)
    {
        Vector3 currentPoint;
        float rad = Mathf.Deg2Rad * m_fAngle;
        currentPoint.x = i * max;
        currentPoint.y = currentPoint.x * Mathf.Tan(rad) - ((9.81f * currentPoint.x * currentPoint.x) / (2 * m_fForwardVelocity * m_fForwardVelocity * Mathf.Cos(rad) * Mathf.Cos(rad)));

        //rotate
        if (m_fRotation != 0)
        {
            //currentPoint.z = i * max * (m_fRotation / 90);
            //currentPoint.x = i * max * (90 / m_fRotation);
            currentPoint.z = i * max * Mathf.Cos(m_fRotation);
            currentPoint.x = i * max * Mathf.Sin(m_fRotation);
        }
        else
        {
            currentPoint.z = 0;
        }
        

        return currentPoint + transform.position;
    }

    // Determine the arc's form based on where the player is looking
    private void UpdateArcParameters() {
        // Get a reference to where the camera is looking to set the arc rotation
        float fCameraYRotation = m_CameraReference.transform.rotation.eulerAngles.y;
        m_fRotation = fCameraYRotation * Mathf.Deg2Rad;

        float fFreeLookY = m_FreeLookReference.m_YAxis.Value;
        m_fAngle = Mathf.Lerp(m_fMinAngle, m_fMaxAngle, 1.0f - fFreeLookY);
    }

    // Gets the rotation
    public float GetArcRotation() {
        return m_fRotation * Mathf.Rad2Deg;
    }

    public void SetRotation(float _fRotation) {
        m_fRotation = _fRotation * Mathf.Deg2Rad;
    }
}
