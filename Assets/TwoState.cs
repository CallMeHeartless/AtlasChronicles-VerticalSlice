using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoState : MonoBehaviour
{
    public enum ControlEnum
    {
        low,high,
        ground, water,lava,
        on,off
    };
    bool OnPoint1 = true;
    private float currentTimer;
    private  ControlEnum currentEffect;
    public ControlEnum point1;
    public float timerMax1;
    public ControlEnum point2;
    public float timerMax2;

    public GameObject place1,place2;
    public float m_fSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OnPoint1 == true)
        {
            if (currentTimer <= 0)
            {
                currentTimer = timerMax2;
                currentEffect = point2;
                OnPoint1 = false;
            }
            else
            {
                currentTimer -= Time.deltaTime;
            }
        }
        else
        {
            if (currentTimer <= 0)
            {
                currentTimer = timerMax1;
                currentEffect = point1;
                OnPoint1 = true;
            }
            else
            {
                currentTimer -= Time.deltaTime;
            }
        }
        //add effects and movement
        switch (currentEffect)
        {
            case ControlEnum.low:
                transform.position = Vector3.MoveTowards(gameObject.transform.position, place1.transform.position, m_fSpeed);

                break;
            case ControlEnum.high:
                transform.position = Vector3.MoveTowards(gameObject.transform.position, place2.transform.position, m_fSpeed);

                break;
            case ControlEnum.ground:
                //ground box on
                break;
            case ControlEnum.water:
                //ground box off
                break;

            case ControlEnum.lava:

                break;

            case ControlEnum.on:
                gameObject.transform.GetChild(0).gameObject.active = true;
                break;

            case ControlEnum.off:
                gameObject.transform.GetChild(0).gameObject.active = false;
                break;
            default:
                break;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (currentEffect)
        {
            case ControlEnum.low:

                break;
            case ControlEnum.high:

                break;
            case ControlEnum.ground:
               
                break;
            case ControlEnum.water:
                //add water preinty 
                
                break;
            case ControlEnum.lava:
                //launch player
                break;
            default:
                break;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        switch (currentEffect)
        {
            case ControlEnum.low:

                break;
            case ControlEnum.high:

                break;
            case ControlEnum.ground:
                
                break;
            case ControlEnum.water:
                //add water preinty 
               
                break;
            case ControlEnum.lava:
                //launch player
                break;
            default:
                break;
        }
    }
}
