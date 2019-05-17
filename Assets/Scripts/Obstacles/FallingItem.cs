using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingItem : MonoBehaviour
{
    public int m_intDamage;
    private bool m_bFirstHit = true;
    public bool m_bDestoryOnGround;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (m_bFirstHit == true)
            {
                collision.gameObject.GetComponent<PlayerController>().DamagePlayer(m_intDamage);
                if(GameObject.FindGameObjectWithTag("UI"))
                {
                    GameObject.FindGameObjectWithTag("UI").GetComponent<DisplayStat>().HP = GameObject.FindGameObjectWithTag("UI").GetComponent<DisplayStat>().HP - m_intDamage;
                    GameObject.FindGameObjectWithTag("UI").GetComponent<DisplayStat>().NewHealth(GameObject.FindGameObjectWithTag("UI").GetComponent<DisplayStat>().HP);
                }
                
                m_bFirstHit = false;
            }

        }
        else
        {
            if (m_bDestoryOnGround)
            {
                Destroy(gameObject);
            }
            else
            {
                if (collision.gameObject.CompareTag("DestoryArea"))
                {
                    Destroy(gameObject);
                }
            }
            
        }
    }
}
