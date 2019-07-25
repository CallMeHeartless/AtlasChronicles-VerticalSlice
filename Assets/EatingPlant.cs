using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatingPlant : MonoBehaviour
{
    private GameObject m_fStartPoint;
    private GameObject m_fEatingPoint;
    private bool m_bEating = false;
    private bool m_bAgro = false;
    private float m_fTimer;
    private float m_fMaxTimer;
    public float m_fSpeed = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        if (m_fMaxTimer >= 0)
        {
            m_fMaxTimer = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bAgro)
        {
            if (Vector3.Distance(transform.position, m_fEatingPoint.transform.position) < 1) {

               // m_fEatingPoint
                    }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, m_fEatingPoint.transform.position, m_fSpeed);
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, m_fStartPoint.transform.position) < 1)
            {

               // m_fStartPoint
                    }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, m_fStartPoint.transform.position, m_fSpeed);
            }
        }


        if (m_bEating)
        {
            if (m_fTimer <= 0)
            {
                m_fTimer = m_fMaxTimer;
            }
            else
            {
                m_fTimer -= Time.deltaTime;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        m_fEatingPoint = other.gameObject;
        if (!m_bEating)
        {
            m_bAgro = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        m_fEatingPoint = null;
        if (!m_bEating)
        {
            m_bAgro = false;
        }
    }
    public EatingPlant()
    {
        m_bEating = true;
    }
}
