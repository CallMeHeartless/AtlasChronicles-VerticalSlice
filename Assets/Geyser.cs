using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geyser : MonoBehaviour
{
    int OnState = 3;
    private float currentTimer;
    
    public float timerMaxPoweringUp;
    public float timerMaxOn;
    public float timerMaxCooling;
    public float timerMaxOff;
    public float m_fspeedOn;
    public float m_fspeedCooling;
    private float number;
    BoxCollider box;
    // Start is called before the first frame update
    void Start()
    {
       box = GetComponent<BoxCollider>();
        number = timerMaxPoweringUp / timerMaxCooling;
    }

    // Update is called OnStatece per frame
    void Update()
    {
        if (OnState == 1)
        {
            if (currentTimer <= 0)
            {
                currentTimer = timerMaxOn;

                OnState = 2;
            }
            else
            {
                currentTimer -= Time.deltaTime;
            }
        }
        else if (OnState == 2)
        {
            if (currentTimer <= 0)
            {
                currentTimer = timerMaxCooling;
               
                OnState = 3;
            }
            else
            {
                currentTimer -= Time.deltaTime;
            }
        }
        else if (OnState == 3)
        {
            if (currentTimer <= 0)
            {
                currentTimer = timerMaxOff;
               
                OnState = 4;
            }
            else
            {
                currentTimer -= Time.deltaTime;
            }
        }
        else if (OnState == 4)
        {
            if (currentTimer <= 0)
            {
                currentTimer = timerMaxPoweringUp;

                OnState = 1;
            }
            else
            {
                currentTimer -= Time.deltaTime;
            }
        }

        if (OnState == 1)
        {
            box.size += new  Vector3(0, 0.1f*m_fspeedOn*2 ,0);
            box.center += new Vector3(0, 0.1f* m_fspeedOn, 0);
            gameObject.transform.GetChild(0).transform.position += new Vector3(0, 0.1f * m_fspeedOn* 2, 0);
        }
        else if(OnState == 3)
        {
            if (box.size.y >= 1)
            {
                box.size -= new Vector3(0, 0.1f * m_fspeedOn * 2* number, 0);
                box.center -= new Vector3(0, 0.1f * m_fspeedOn * number, 0);
            }
            gameObject.transform.GetChild(0).transform.position -= new Vector3(0, 0.1f * m_fspeedOn*number* 2, 0);

        }
    }
}
