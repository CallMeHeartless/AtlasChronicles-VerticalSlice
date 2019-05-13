using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingAppleSpawner : MonoBehaviour
{
    private float m_fCurrentTimer;
    public GameObject m_gDroppingItem;
    public float m_fTimerMax = 1;
   
    // Update is called once per frame
    void Update()
    {
        if (m_fCurrentTimer <= 0)
        {
            GameObject.Instantiate(m_gDroppingItem, gameObject.transform.position, gameObject.transform.rotation);
            m_fCurrentTimer = m_fTimerMax;
        }
        else
        {
            m_fCurrentTimer -= Time.deltaTime;
        }
    }
}
