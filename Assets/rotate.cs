using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
   
    public bool m_bMoving;
    public float m_fSpeed;
    
    public float m_fTimer;
    [SerializeField] private List<int> m_lMaxTimers;
    [SerializeField] private List<int> m_lRotationPoints;
    public int m_CurrentPoints;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bMoving)
        {
            if (transform.localEulerAngles.y < m_lRotationPoints[m_CurrentPoints])
            {
                transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y + m_fSpeed, 0);
            }
            else if (transform.localEulerAngles.y > m_lRotationPoints[m_CurrentPoints])
            {
                transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y - m_fSpeed, 0);
            }

            if ((transform.localEulerAngles.y <= m_lRotationPoints[m_CurrentPoints] + 1)&& (transform.localEulerAngles.y >= m_lRotationPoints[m_CurrentPoints] - 1))
            {
                m_bMoving = false;
                m_fTimer = m_lMaxTimers[m_CurrentPoints];
            }
        }

        if (!m_bMoving)
        {
            if (m_fTimer<=0)
            {
                if (m_CurrentPoints == m_lMaxTimers.Count-1)
                {
                    m_CurrentPoints = 0;
                    transform.localEulerAngles = new Vector3(0, 0, 0);
                }
                else
                {
                    m_CurrentPoints++;
                }
                m_bMoving =true;
            }
            else
            {
                m_fTimer -= Time.deltaTime;
            }
        }
    }
}
