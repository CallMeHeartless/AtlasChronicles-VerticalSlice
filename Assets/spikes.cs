using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikes : MonoBehaviour
{
    public int m_intDamage;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
      
            if (other.CompareTag("Player"))
            {
                other.GetComponent<PlayerController>().DamagePlayer(m_intDamage);
                GameObject.FindGameObjectWithTag("UI").GetComponent<DisplayStat>().HP = GameObject.FindGameObjectWithTag("UI").GetComponent<DisplayStat>().HP - m_intDamage;
                GameObject.FindGameObjectWithTag("UI").GetComponent<DisplayStat>().NewHealth(GameObject.FindGameObjectWithTag("UI").GetComponent<DisplayStat>().HP);
            

            }
        
    }
}
