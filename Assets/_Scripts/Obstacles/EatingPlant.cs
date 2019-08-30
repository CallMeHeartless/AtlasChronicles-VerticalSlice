using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatingPlant : MonoBehaviour
{
    [SerializeField] private GameObject m_fStartPoint;
    List<GameObject> m_fEatingPoint = new List<GameObject>();
    [SerializeField] private bool m_bEating = false;
    [SerializeField] private bool m_bAgro = false;
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
       


        if (m_bEating)
        {
            
                if (Vector3.Distance(transform.GetChild(0).transform.position, m_fStartPoint.transform.position) < 0.1f)
                {

                    if (m_fTimer <= 0)
                    {
                        if (m_fEatingPoint.Count !=0)
                        {
                        m_bAgro = true;
                         }
                    }
                    else
                    {
                        m_fTimer -= Time.deltaTime;
                    }
                }
                else
                {
                    transform.GetChild(0).transform.position = Vector3.MoveTowards(transform.GetChild(0).transform.position, m_fStartPoint.transform.position, m_fSpeed);
                }
          
        }
        else
        {
            if (m_bAgro)
            {
                if (Vector3.Distance(transform.GetChild(0).transform.position, m_fEatingPoint[0].transform.position) < 1f)
                {

                    // m_fEatingPoint
                }
                else
                {
                    transform.GetChild(0).transform.position = Vector3.MoveTowards(transform.GetChild(0).transform.position, m_fEatingPoint[0].transform.position, m_fSpeed);
                }
            }
            else
            {
                if (Vector3.Distance(transform.GetChild(0).transform.position, m_fStartPoint.transform.position) < 1f)
                {

                    // m_fStartPoint
                }
                else
                {
                    transform.GetChild(0).transform.position = Vector3.MoveTowards(transform.GetChild(0).transform.position, m_fStartPoint.transform.position, m_fSpeed);
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        m_fEatingPoint.Add(other.gameObject);
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
    public void Eating()
    {
        if( m_fEatingPoint[0].CompareTag("Player"))
        {
            //m_fEatingPoint[0].GetComponent<PlayerController>().
        }
        else
	    {
            m_fEatingPoint.RemoveAt(0);
        }
      
        m_bEating = true;
        m_fTimer = m_fMaxTimer;
    }
}
