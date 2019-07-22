using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geyser : MonoBehaviour
{
    int OnState = 3;
    private float currentm_fTimer;
    
    public float m_fTimerMaxPoweringUp;
    public float m_fTimerMaxOn;
    public float m_fTimerMaxCooling;
    public float m_fTimerMaxOff;
    public float m_fspeedOn;
    public float m_fspeedCooling;
    private float m_fNumber;
    BoxCollider box;
    // Start is called before the first frame update
    void Start()
    {
       box = GetComponent<BoxCollider>();
        m_fNumber = m_fTimerMaxPoweringUp / m_fTimerMaxCooling;
    }

    // Update is called OnStatece per frame
    void Update()
    {
        Timers();
        Movement();
    }
        void Timers()
        {
            if (OnState == 1)
            {
                if (currentm_fTimer <= 0)
                {
                    currentm_fTimer = m_fTimerMaxOn;

                    OnState = 2;
                }
                else
                {
                    currentm_fTimer -= Time.deltaTime;
                }
            }
            else if (OnState == 2)
            {
                if (currentm_fTimer <= 0)
                {
                    currentm_fTimer = m_fTimerMaxCooling;

                    OnState = 3;
                }
                else
                {
                    currentm_fTimer -= Time.deltaTime;
                }
            }
            else if (OnState == 3)
            {
                if (currentm_fTimer <= 0)
                {
                    currentm_fTimer = m_fTimerMaxOff;

                    OnState = 4;
                }
                else
                {
                    currentm_fTimer -= Time.deltaTime;
                }
            }
            else if (OnState == 4)
            {
                if (currentm_fTimer <= 0)
                {
                    currentm_fTimer = m_fTimerMaxPoweringUp;

                    OnState = 1;
                }
                else
                {
                    currentm_fTimer -= Time.deltaTime;
                }
            }
        }
    void Movement() {
        if (OnState == 1)
        {
            box.size += new Vector3(0, 0.1f * m_fspeedOn * 2, 0);
            box.center += new Vector3(0, 0.1f * m_fspeedOn, 0);
            gameObject.transform.GetChild(0).transform.position += new Vector3(0, 0.1f * m_fspeedOn * 2, 0);
        }
        else if (OnState == 3)
        {
            if (box.size.y >= 1)
            {
                box.size -= new Vector3(0, 0.1f * m_fspeedOn * 2 * m_fNumber, 0);
                box.center -= new Vector3(0, 0.1f * m_fspeedOn * m_fNumber, 0);
            }
            gameObject.transform.GetChild(0).transform.position -= new Vector3(0, 0.1f * m_fspeedOn * m_fNumber * 2, 0);

        }

    }

}
