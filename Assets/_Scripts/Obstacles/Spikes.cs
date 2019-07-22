using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public int m_iDamage;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
      
        if (other.CompareTag("Player"))
        {
            DamageMessage message = new DamageMessage();
            message.damage = m_iDamage;
            message.source = gameObject;
            other.GetComponent<DamageController>().ApplyDamage(message);
            GameObject.FindGameObjectWithTag("UI").GetComponent<DisplayStat>().HP = GameObject.FindGameObjectWithTag("UI").GetComponent<DisplayStat>().HP - m_iDamage;
            GameObject.FindGameObjectWithTag("UI").GetComponent<DisplayStat>().NewHealth(GameObject.FindGameObjectWithTag("UI").GetComponent<DisplayStat>().HP);
            

        }
        
    }
}
