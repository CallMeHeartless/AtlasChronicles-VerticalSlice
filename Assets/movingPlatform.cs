using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    public GameObject[] m_rPoints;
    public int m_intCurrentPoint = 0;
    public float m_fSpeed;
    public float m_fPauseDuration;
    public float m_fCurrentPause;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(m_rPoints.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_fCurrentPause <= 0)
        {
            transform.position = Vector3.MoveTowards(gameObject.transform.position, m_rPoints[m_intCurrentPoint].transform.position, m_fSpeed);

            if (Vector3.Distance(transform.position, m_rPoints[m_intCurrentPoint].transform.position) < 1)
            {
                if (m_rPoints.Length - 1 == m_intCurrentPoint)
                {
                    m_fCurrentPause = m_fPauseDuration;
                    m_intCurrentPoint = 0;
                }
                else
                {
                    m_fCurrentPause = m_fPauseDuration;
                    m_intCurrentPoint++;
                }
            }
        }
        else
        {
            m_fCurrentPause -= Time.deltaTime;
        }
        
    }
}
