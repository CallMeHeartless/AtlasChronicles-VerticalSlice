using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoState : MonoBehaviour
{
    public enum ControlEnum
    {
        Low,High,
        Ground, Water,Lava,
        On,Off
    };
    bool m_bOnPoint1 = true;
    private float m_fCurrentTimer;
    private  ControlEnum m_conCurrentEffect;
    public ControlEnum m_conPoint1;
    public float m_fTimerMax1;
    public ControlEnum m_conPoint2;
    public float m_fTimerMax2;

    public GameObject m_gPlace1,m_gPlace2;
    public float m_fSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bOnPoint1 == true)
        {
            if (m_fCurrentTimer <= 0)
            {
                m_fCurrentTimer = m_fTimerMax2;
                m_conCurrentEffect = m_conPoint2;
                m_bOnPoint1 = false;
            }
            else
            {
                m_fCurrentTimer -= Time.deltaTime;
            }
        }
        else
        {
            if (m_fCurrentTimer <= 0)
            {
                m_fCurrentTimer = m_fTimerMax1;
                m_conCurrentEffect = m_conPoint1;
                m_bOnPoint1 = true;
            }
            else
            {
                m_fCurrentTimer -= Time.deltaTime;
            }
        }
        //add effects and movement
        switch (m_conCurrentEffect)
        {
            case ControlEnum.Low:
                transform.position = Vector3.MoveTowards(gameObject.transform.position, m_gPlace1.transform.position, m_fSpeed);

                break;
            case ControlEnum.High:
                transform.position = Vector3.MoveTowards(gameObject.transform.position, m_gPlace2.transform.position, m_fSpeed);

                break;
            case ControlEnum.Ground:
                //ground box on
                break;
            case ControlEnum.Water:
                //ground box off
                break;

            case ControlEnum.Lava:

                break;

            case ControlEnum.On:
                gameObject.transform.GetChild(0).gameObject.active = true;
                break;

            case ControlEnum.Off:
                gameObject.transform.GetChild(0).gameObject.active = false;
                break;
            default:
                break;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (m_conCurrentEffect)
        {
            case ControlEnum.Low:

                break;
            case ControlEnum.High:

                break;
            case ControlEnum.Ground:
               
                break;
            case ControlEnum.Water:
                //add water preinty 
                
                break;
            case ControlEnum.Lava:
                //launch player
                break;
            default:
                break;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        switch (m_conCurrentEffect)
        {
            case ControlEnum.Low:

                break;
            case ControlEnum.High:

                break;
            case ControlEnum.Ground:
                
                break;
            case ControlEnum.Water:
                //add water preinty 
               
                break;
            case ControlEnum.Lava:
                //launch player
                break;
            default:
                break;
        }
    }
}
