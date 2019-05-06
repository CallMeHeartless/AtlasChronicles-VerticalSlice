using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingitem : MonoBehaviour
{
    public int m_intDamage;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().DamagePlayer(m_intDamage);
            GameObject.FindGameObjectWithTag("UI").GetComponent<DisplayStat>().HP = GameObject.FindGameObjectWithTag("UI").GetComponent<DisplayStat>().HP - m_intDamage;
            GameObject.FindGameObjectWithTag("UI").GetComponent<DisplayStat>().NewHealth(GameObject.FindGameObjectWithTag("UI").GetComponent<DisplayStat>().HP);

        }
        else
        {
            Destroy(gameObject);
        }
    }
}
