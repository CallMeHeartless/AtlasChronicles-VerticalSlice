using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public int m_iHeal = 1;
    
    //on collision heal player
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            other.GetComponent<DamageController>().ApplyHealing(m_iHeal);
           
            Destroy(gameObject);
        }

    }
}
