using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    public float m_fTimer;
    public float m_fMaxTimer= 2;
    public GameObject Apple;
    // Start is called before the first frame update
    void Start()
    {
        if (m_fMaxTimer <= 0)
        {
            m_fMaxTimer = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_fTimer <= 0)
        {
            Instantiate(Apple, transform.position, Quaternion.identity);
            m_fTimer = m_fMaxTimer;
        }
        else
        {
            m_fTimer -= Time.deltaTime;
        }
    }
}
