using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public int m_iDamage;
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DamageMessage message = new DamageMessage();
            message.damage = m_iDamage;
            message.source = gameObject;
            other.GetComponent<DamageController>().ApplyDamage(message);
        }
    }
}
