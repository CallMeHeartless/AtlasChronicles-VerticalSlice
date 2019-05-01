using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Breaking : MonoBehaviour
{
    private bool m_bCollatsing = false;
    public int m_intStanability;// howmany times the playey can land on it
    public float m_fSpeed;
    public float m_fTimer;
    public float m_fMaxTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(m_bCollatsing){
            if (m_fTimer <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                m_fTimer -= Time.deltaTime;
            }
           GetComponent<Rigidbody>().velocity = Vector3.down * m_fSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            m_intStanability--;
            if (m_intStanability == 0)
            {
                Copsate();
                StartCoroutine(destoration());
            }
            else
            {
                //do effect
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

        }
    }

    private void Copsate()
    {
       
    }
    IEnumerator destoration()
    {
        Debug.Log("help");
        //cumbling effect
        yield return new WaitForSeconds(2);
        m_fTimer = m_fMaxTimer;
        m_bCollatsing = true;
    }
}
