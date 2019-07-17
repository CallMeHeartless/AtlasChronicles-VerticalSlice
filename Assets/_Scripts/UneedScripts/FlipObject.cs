using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipObject : MonoBehaviour
{
    public float m_fSpeed;
    public bool m_bStops;
    public float[] m_fStoppingPoints;
    public float m_fRange;
    public int m_intCurrentPoint = 0;
    private Vector3 m_vec3RotationalPoint = new Vector3(0,0,0);
   
    bool m_bPause = false;
    private float m_fCurrentTimer;
    public float m_fMaxTimer;

    float low,high;
    // Start is called before the first frame update
    void Start()
    {
        m_fCurrentTimer -= Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_fCurrentTimer<=0)
        {
            m_fCurrentTimer = m_fMaxTimer;
        }
        else
        {
            m_fCurrentTimer -= Time.deltaTime;

        }
    }
    
}
