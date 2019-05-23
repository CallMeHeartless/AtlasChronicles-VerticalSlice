using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{

    [Header("RotatingPoints")]
    [SerializeField] bool m_rotateClockwise = false;
    [SerializeField] float m_fSpeed = 0.3f;
    [SerializeField] Transform[] m_rotatingPoints;
    [SerializeField] Transform[] m_pivotPoints;

    //Commented out for now
    //[Header("Stopping at points")]
    private bool m_bStops = false;
    private float[] m_fStoppingPoints;
    private float m_fRange;
    private int m_iCurrentPoint = 0;
    private Vector3 m_vec3RotationalPoint = new Vector3(0, 0, 0);
    private bool m_bPause = false;
    private float m_fCurrentTimer;
    private float m_fMaxTimer;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bStops == true)
        {
            Debug.Log(Mathf.Rad2Deg *transform.rotation.x*4.5);
            if ((m_fStoppingPoints[m_iCurrentPoint] <=(Mathf.Rad2Deg * transform.rotation.x*4.5)+ m_fRange) && (m_fStoppingPoints[m_iCurrentPoint] >= (Mathf.Rad2Deg *4.5f* transform.rotation.x) - m_fRange))
            {
                transform.rotation = Quaternion.Euler(new Vector3(m_fStoppingPoints[m_iCurrentPoint], m_vec3RotationalPoint.y, m_vec3RotationalPoint.z));
                m_bPause = true;
                m_fCurrentTimer = m_fMaxTimer;
                if (m_iCurrentPoint == m_fStoppingPoints.Length-1)
                {
                    m_iCurrentPoint = 0;
                }
                else
                {
                    m_iCurrentPoint++;
                }
            }

            if (m_bPause == true)
            {
                if (m_fCurrentTimer <= 0)
                {
                    m_bPause = false;
                    transform.Rotate(Vector3.up, m_fSpeed);
                }
                else
                {
                    m_fCurrentTimer -= Time.deltaTime;
                }
            }
            else
            {
                transform.Rotate(Vector3.up, m_fSpeed);
            }

        }
        else
        {
            transform.Rotate((m_rotateClockwise ? Vector3.up : -Vector3.up), m_fSpeed);
            SetPivotPositions();
        }

    }

    void SetPivotPositions()
    {
        if (m_pivotPoints.Length == m_pivotPoints.Length)
        {
            for (int i = 0; i < m_pivotPoints.Length; ++i)
            {
                m_pivotPoints[i].position = m_rotatingPoints[i].position;
            }
        }

    }
}
