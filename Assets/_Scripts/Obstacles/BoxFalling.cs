using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxFalling : MonoBehaviour
{
    public float m_fHp;
    public float m_fDamage;
    public bool m_bAir = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bAir)
        {
            m_fDamage += 0.01f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        m_bAir = false;
        m_fHp -= m_fDamage;
        if (m_fHp <= 0)
        {
            //spawn other stuff
            GetComponent<Destroyed>().enabled = true;
            //Destroy(gameObject);

        }
        m_fDamage = 0;
    }
    private void OnTriggerExit(Collider other)
    {
        m_bAir = true;
    }
}
