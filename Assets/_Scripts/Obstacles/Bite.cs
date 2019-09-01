using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bite : MonoBehaviour
{
   public int m_iDamage;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
   
        if (other.CompareTag("Player"))
        {
            DamageMessage message = new DamageMessage();
            message.damage = m_iDamage;
            message.source = this.gameObject;
            other.GetComponent<DamageController>().ApplyDamage(message);

            transform.parent.GetComponent<EatingPlant>().Eating();
        }
        Debug.Log("hrel");
    }
}
