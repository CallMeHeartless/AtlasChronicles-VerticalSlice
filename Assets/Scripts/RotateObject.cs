using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float m_fSpeed;
    public bool m_bStops;
    public float[] m_fStoppingPoints;
    public float m_fRange;
    public int m_intCurrentPoint = 0;
    public Vector3 m_vec3RotationalPoint;
    bool m_bPause = false;
    private float m_fCurrentTimer;
    public float m_fMaxTimer;
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
            if ((m_fStoppingPoints[m_intCurrentPoint] <=(Mathf.Rad2Deg * transform.rotation.x*4.5)+ m_fRange) && (m_fStoppingPoints[m_intCurrentPoint] >= (Mathf.Rad2Deg *4.5f* transform.rotation.x) - m_fRange))
            {
                transform.rotation = Quaternion.Euler(new Vector3(m_fStoppingPoints[m_intCurrentPoint], m_vec3RotationalPoint.y, m_vec3RotationalPoint.z));
                m_bPause = true;
                m_fCurrentTimer = m_fMaxTimer;
                if (m_intCurrentPoint == m_fStoppingPoints.Length-1)
                {
                    m_intCurrentPoint = 0;
                }
                else
                {
                    m_intCurrentPoint++;
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
            transform.Rotate(Vector3.up, m_fSpeed);
        }

    }
}
