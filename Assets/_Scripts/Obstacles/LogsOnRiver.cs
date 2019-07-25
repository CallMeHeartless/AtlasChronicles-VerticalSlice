using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogsOnRiver : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("movement")]
    public GameObject[] m_rPoints;
    public int m_intCurrentPoint = 0;
    public float m_fSpeed = 0.05f;
    public bool m_bAboveWater = true;
   

    [Header("testing Don't Touch")]
    public bool m_bchangingHieght = false;
    public Vector3 m_v3Sinkheight;
    public Vector3 m_v3Riseheight;
    public Vector3 m_v3Currentheight;

    float m_fYEffector;
    Vector3 m_v3LogsLoction;
    float m_fLogHeight;
    // Start is called before the first frame update
    void Start()
    {
        m_fYEffector = transform.position.y;
       // m_v3Sinkheight.y -= m_fYEffector;
        //m_v3Riseheight.y += m_fYEffector;
        Debug.Log(m_fYEffector);
        if (m_bAboveWater)
        {

            m_v3Currentheight = m_v3Riseheight;

        }
        else
        {
            m_v3Currentheight = m_v3Sinkheight;
        }
        //Debug.Log(m_rPoints.Length);

    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        //Debug.Log(transform.position.y + " and "+ (m_rPoints[m_intCurrentPoint].transform.position.y)+ " and " + m_v3Currentheight.y);
        if (Vector3.Distance(transform.position, m_rPoints[m_intCurrentPoint].transform.position+ m_v3Currentheight+ new Vector3(0, m_fYEffector,0)) < 1)
                {
                    //Debug.Log(timeITakes);

                   
                        if (m_rPoints.Length - 1 == m_intCurrentPoint)
                        {
                Destroy(transform.parent.gameObject);
                //destorySelf



            }
                        else
                        {
                            m_intCurrentPoint++;
                            m_bchangingHieght = true;
                            if (m_bAboveWater)
                            {
                    m_v3Currentheight = m_v3Sinkheight;
                    m_bAboveWater = false;
                            }
                            else
                            {
                    m_v3Currentheight = m_v3Riseheight ;
                    m_bAboveWater = true;
                            }
                            //changeHieght();
                        }

        }

        if (m_bchangingHieght)
        {
            changeHieght();
        }

        m_v3LogsLoction = Vector3.MoveTowards(transform.position, m_rPoints[m_intCurrentPoint].transform.position, m_fSpeed);
       //    m_v3LogsLoction.y = m_fLogHeight.y;
        = m_v3LogsLoction;
    }

   void changeHieght()
    {
        if (m_bAboveWater)
        {
            if ((transform.position.y < m_v3Riseheight.y))
            {
                //going up
                m_fLogHeight += .02f;
            }
            else
            {
                // m_v3Currentheight = Vector3.zero;
                m_bchangingHieght = false;
            }
        }
        else
        {
            //Debug.Log(transform.position.y);
            if ((transform.position.y > m_v3Sinkheight.y))
            {
                m_fLogHeight -= .02f;
            }
            else
            {

                //m_v3Currentheight = Vector3.zero;
                //m_v3Currentheight = new Vector3(0, -2, 0);
                m_bchangingHieght = false;
            }
        }
    }


    
}
