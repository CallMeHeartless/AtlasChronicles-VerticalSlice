using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyTrap : MonoBehaviour
{
    public bool m_PlayOn = false;
    public bool m_bCollapsing = false;
    public int m_intIntisalStanability = 1;
    public int m_intStanability = 99;// howmany times the playey can land on it
    public float m_fSpeed = 0.1f;
    public float m_fTimer;
    public float m_fMaxTimer = 2;
    public Material m_mBite;
    public Material m_mWait;

    float speed = 0.05f; //how fast it shakes
    bool collapse = false;

    // Start is called before the first frame update
    private void Start()
    {
        Starting();
    }
    public void Starting()
    {
        if (!m_PlayOn)
        {
            m_intStanability = m_intIntisalStanability;
            if (m_intIntisalStanability >= 0)
            {
                m_intIntisalStanability = 1;
            }
        }
      
        if (m_fMaxTimer >= 0)
        {
            m_fMaxTimer = 2;
        }
        m_fTimer = m_fMaxTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_PlayOn)
        {
            flyTrapAuto();
        }
        else
        {
            PlayerOnFlyTrap();
        }

        if (collapse)
        {
            //eat
            //collapse = false;
            GetComponent<Renderer>().material = m_mBite;
            StartCoroutine("destoration");
            m_bCollapsing = false;
            m_fTimer = 3;
        }

    }
    void flyTrapAuto()
    {
        if (m_fTimer <= 0)
        {

            collapse = true;
            
           

        }
        else
        {
            m_fTimer -= Time.deltaTime;
        }
        if (m_fTimer <= 0.5)
        {
            //shake 
        }

       
    }

    void PlayerOnFlyTrap() {

       

        if (m_bCollapsing)
        {
            if (m_fTimer <= 0.5)
            {
              
                //shake 
            }
            if (m_fTimer <= 0)
            {

                collapse = true;
               
                //shaking

            }
            else
            {
                m_fTimer -= Time.deltaTime;
            }

        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            if (collapse)
            {

                DamageMessage message = new DamageMessage();
                message.damage = 4;
                message.source = gameObject;
                other.GetComponent<DamageController>().ApplyDamage(message);


            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            if (collapse)
            {

                DamageMessage message = new DamageMessage();
                message.damage = 4;
                message.source = gameObject;
                other.GetComponent<DamageController>().ApplyDamage(message);


            }
            else
            {
                Debug.Log("fly trap");
                other.gameObject.GetComponent<PlayerController>().SetOnMovingPlatform(true);
                m_intStanability--;
                if (m_intStanability == 0)
                {
                    m_bCollapsing = true;

                }
                else
                {
                    //do effect
                }
            }
        }
    }
    

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //print("woops");
            other.gameObject.GetComponent<PlayerController>().SetOnMovingPlatform(false);
        }
    }


    IEnumerator destoration()
    {
        //Debug.Log("help");
        //cumbling effect
        yield return new WaitForSeconds(.8f);
        m_fTimer = 2;
        m_intStanability = m_intIntisalStanability;
        collapse = false;
        GetComponent<Renderer>().material = m_mWait;

    }
}
