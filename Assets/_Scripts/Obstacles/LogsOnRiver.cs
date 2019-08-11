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
    Vector3 currentAim;
    public float speed = 1f;
    float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        
        currentAim = m_rPoints[m_intCurrentPoint].transform.position;
        if (m_bAboveWater)
        {
            //currentAim += m_v3Riseheight;
            m_v3Currentheight = m_v3Riseheight;

        }
        else
        {
            //currentAim += m_v3Sinkheight;
            m_v3Currentheight = m_v3Sinkheight;
        }
        //Debug.Log(m_rPoints.Length);
       
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        //Debug.Log(transform.position.y + " and " + currentAim + " and " + transform.position.y);
        if (Vector3.Distance(transform.position, currentAim) < 1)
        {
            //Debug.Log(timeITakes);


            if (m_rPoints.Length - 1 == m_intCurrentPoint)
            {
                Destroy(transform.parent.gameObject);
                //destorySelf



            }
            else
            {
                time = 0;
                m_intCurrentPoint++;
                m_bchangingHieght = true;
                currentAim = m_rPoints[m_intCurrentPoint].transform.position;
                if (m_bAboveWater)
                {
                    //currentAim+= m_v3Sinkheight;
                    m_v3Currentheight = m_v3Sinkheight;
                    m_bAboveWater = false;
                }
                else
                {
                   // currentAim+= m_v3Riseheight;
                    m_v3Currentheight = m_v3Riseheight;
                    m_bAboveWater = true;
                }
                //changeHieght();
            }

        }

      
        m_v3LogsLoction = Vector3.MoveTowards(transform.position, currentAim, m_fSpeed);

        if (m_bchangingHieght)
        {
            changeHieght();
        }

        
        transform.position = m_v3LogsLoction;
        //    m_v3LogsLoction.y = m_fLogHeight.y;
        // = m_v3LogsLoction;
    }

    void changeHieght()
    {
       
        if (m_bAboveWater)
        {
            if ((transform.position.y < m_v3Riseheight.y))
            {
               
                //going up
                m_v3LogsLoction.y += .02f;
                currentAim.y = transform.position.y;
            }
            else
            {
               
                // m_v3Currentheight = Vector3.zero;
                currentAim.y = transform.position.y;
                m_bchangingHieght = false;
            }
        }
        else
        {
            //Debug.Log(transform.position.y);
            if ((transform.position.y > m_v3Sinkheight.y))
            {
               
                m_v3LogsLoction.y -= .02f;
            }
            else
            {
               
                //m_v3Currentheight = Vector3.zero;
                //m_v3Currentheight = new Vector3(0, -2, 0);
                currentAim.y = transform.position.y;
                m_bchangingHieght = false;
            }
        }
    }


    
}
