using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{

    public bool m_bMoving;
    public float m_fSpeed;

    public float m_fTimer;
    [SerializeField] private List<int> m_lMaxTimers;
    [SerializeField] private List<Vector3> m_lRotationPoints;
    private Vector3 NewRoation;
    public int m_CurrentPoints;

    public bool RotateX, RotateY, RotateZ;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (m_bMoving)
        {
            NewRoation = new Vector3 (0,0,0)    ;
            if (RotateX)
            {
                PlatfromMovingRoundX();
            }
            if (RotateY)
            {
                PlatfromMovingRoundY();
            }
            if (RotateZ)
            {
                PlatfromMovingRoundZ();
            }
          transform.localEulerAngles = NewRoation;

            //check if Roation match Required Roation
           Checker();
        }
        else
        {
            PlatfromNotMoving();
        }
        
    }
    void PlatfromMovingRoundX()
    {

        if (transform.localEulerAngles.x < m_lRotationPoints[m_CurrentPoints].x)
        {
            NewRoation += new Vector3( transform.localEulerAngles.x + m_fSpeed, 0,0);
        }
        else if (transform.localEulerAngles.x > m_lRotationPoints[m_CurrentPoints].x)
        {
            NewRoation += new Vector3( transform.localEulerAngles.x - m_fSpeed,0, 0);
        }
    }

        //moving the platform around Y aixes
        void PlatfromMovingRoundY()
    {

        if (transform.localEulerAngles.y < m_lRotationPoints[m_CurrentPoints].y)
        {
            NewRoation += new Vector3(0, transform.localEulerAngles.y + m_fSpeed, 0);
        }
        else if (transform.localEulerAngles.y > m_lRotationPoints[m_CurrentPoints].y)
        {
            NewRoation += new Vector3(0, transform.localEulerAngles.y - m_fSpeed, 0);
        }

    }
    void PlatfromMovingRoundZ()
    {

        if (transform.localEulerAngles.z < m_lRotationPoints[m_CurrentPoints].z)
        {
            NewRoation += new Vector3(0,0, transform.localEulerAngles.z + m_fSpeed);
        }
        else if (transform.localEulerAngles.z > m_lRotationPoints[m_CurrentPoints].z)
        {
            NewRoation += new Vector3(0,0, transform.localEulerAngles.z - m_fSpeed);
        }
    }
    void Checker()
    {

        float dis = Vector3.Distance(transform.eulerAngles, m_lRotationPoints[m_CurrentPoints]);
        Debug.Log(dis);
        bool inRange = dis < m_fSpeed;

        if (inRange)
            {
                m_bMoving = false;
                m_fTimer = m_lMaxTimers[m_CurrentPoints];
            }
        }
    //Stopping object from rotating for some time
    void PlatfromNotMoving(){
        if (!m_bMoving)
        {
            if (m_fTimer <= 0)
            {
                if (m_CurrentPoints == m_lMaxTimers.Count - 1)
                {
                    m_CurrentPoints = 0;
                    transform.localEulerAngles = new Vector3(0, 0, 0);
                }
                else
                {
                    m_CurrentPoints++;
                }
                m_bMoving = true;
            }
            else
            {
                m_fTimer -= Time.deltaTime;
            }
        }
    }
}