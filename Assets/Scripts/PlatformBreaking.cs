using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBreaking : MonoBehaviour
{
    public bool m_bCollatsing = false;
    public int m_intIntisalStanability =1;
    public int m_intStanability = 99;// howmany times the playey can land on it
    public float m_fSpeed = 0.1f;
    public float m_fTimer;
    public float m_fMaxTimer = 2;



    // Start is called before the first frame update
    private void Start()
    {
        Starting();
    }
    public void Starting()
    {
        reform();
        m_intStanability = m_intIntisalStanability;
        if (m_fMaxTimer == 0 )
        {
            m_fMaxTimer = 2;
        }
        m_fTimer = m_fMaxTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_bCollatsing){
            if (m_fTimer <= 0)
            {
                if (gameObject.GetComponent<Respawning>() == null)
                {
                    gameObject.AddComponent<Respawning>();
                    gameObject.GetComponent<Respawning>().m_eResetType = Respawning.eResetType.breaking;
                }
                else
                {
                    gameObject.GetComponent<Respawning>().enabled = true;
                }
                gameObject.GetComponent<Respawning>().Starting();

                gameObject.GetComponent<MeshRenderer>().enabled = false;
                gameObject.GetComponent<PlatformBreaking>().enabled = false;
                //gameObject.SetActive(false);
                //Destroy(gameObject);
            }
            else
            {
                m_fTimer -= Time.deltaTime;
            }
           transform.Translate( Vector3.down * m_fSpeed);
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
                
                StartCoroutine(destoration());
            }
            else
            {
                //do effect
            }
        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {

    //    }
    //}

    public void Copsate()
    {
        m_bCollatsing = true;
    }
    public void reform()
    {
        m_bCollatsing = false;
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
