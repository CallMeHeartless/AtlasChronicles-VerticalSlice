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
    public bool RotateClockrise = false;

    public bool RotateX, RotateY, RotateZ;
    [SerializeField]
    private bool Shouldmove = false;
    [SerializeField]
    private int ShouldmoveLoctation;
    [SerializeField] private bool Blocked = false;
    // Start is called before the first frame update
    void Start()
    {
        Checker();
        //PlatfromNotMoving();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bMoving)
        {
            if (!Blocked)
            {
                moving();
            }
        }
        else
        {
            PlatfromNotMoving();
        }
    }

    void moving() {

        if (RotateX)
        {
            transform.Rotate((RotateClockrise ? Vector3.right : Vector3.left), m_fSpeed);
        }
        if (RotateY)
        {
            transform.Rotate((RotateClockrise ? Vector3.up : Vector3.down), m_fSpeed);
        }
        if (RotateZ)
        {
            transform.Rotate((RotateClockrise ? Vector3.forward : Vector3.back), m_fSpeed);
        }
        //check if Roation match Required Roation
        Checker();

    }
    void Checker()
    {
        float dis;
        if (transform.parent == null)
        {
            dis = Vector3.Distance(transform.eulerAngles, m_lRotationPoints[m_CurrentPoints]);
        }
        else
        {
            dis = Vector3.Distance(transform.eulerAngles - transform.parent.transform.eulerAngles, m_lRotationPoints[m_CurrentPoints]);
        }

        bool inRange = dis < m_fSpeed;

        if (inRange)
        {
            m_bMoving = false;
            m_fTimer = m_lMaxTimers[m_CurrentPoints];
            

        }
    }
    //Stopping object from rotating for some time
    void PlatfromNotMoving() {
        if (!m_bMoving)
        {
            if (m_fTimer <= 0)
            {
                if (m_CurrentPoints == m_lMaxTimers.Count - 1)
                {
                    m_CurrentPoints = 0;
                    
                }
                else
                {
                    m_CurrentPoints++;
                }
                m_bMoving = true;
                if (Shouldmove)
                {
                    if (ShouldmoveLoctation == m_CurrentPoints)
                    {
                        Debug.Log(true);
                        Blocked = true;
                    }
                }
            }
            else
            {
                m_fTimer -= Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
                Debug.Log(false);
                Blocked =false;
          
        }
    }
}