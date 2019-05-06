using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public int m_intHeal = 1;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            m_intHeal = -m_intHeal;
            other.GetComponent<PlayerController>().DamagePlayer(m_intHeal);
            GameObject.FindGameObjectWithTag("UI").GetComponent<DisplayStat>().HP = GameObject.FindGameObjectWithTag("UI").GetComponent<DisplayStat>().HP - m_intHeal;
            GameObject.FindGameObjectWithTag("UI").GetComponent<DisplayStat>().NewHealth(GameObject.FindGameObjectWithTag("UI").GetComponent<DisplayStat>().HP);

            Destroy(gameObject);
        }

    }
}
