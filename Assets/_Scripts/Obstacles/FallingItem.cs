using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingItem : MonoBehaviour
{
    public int m_iDamage;
    private bool m_bFirstHit = true;
    public bool m_bDestoryOnGround;

   // public float m_Timer;
   // public float m_MaxTimer;
    // Start is called before the first frame update
    private void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (m_bFirstHit == true)
            {
                //collision.gameObject.GetComponent<PlayerController>().DamagePlayer(m_intDamage);
                DamageMessage message = new DamageMessage();
                message.damage = m_iDamage;
                message.source = this.gameObject;
                collision.gameObject.GetComponent<DamageController>().ApplyDamage(message);
                if(GameObject.FindGameObjectWithTag("UI"))
                {
                    GameObject.FindGameObjectWithTag("UI").GetComponent<DisplayStat>().m_iHP = GameObject.FindGameObjectWithTag("UI").GetComponent<DisplayStat>().m_iHP - m_iDamage;
                    GameObject.FindGameObjectWithTag("UI").GetComponent<DisplayStat>().NewHealth(GameObject.FindGameObjectWithTag("UI").GetComponent<DisplayStat>().m_iHP);
                }
                
                m_bFirstHit = false;
            }

        }
        else
        {
           // Debug.Log("hit");
            if (m_bDestoryOnGround)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                if (collision.gameObject.CompareTag("Ground"))
                {
                    Destroy(transform.parent.gameObject);
                }
            }
            
        }
    }
}
