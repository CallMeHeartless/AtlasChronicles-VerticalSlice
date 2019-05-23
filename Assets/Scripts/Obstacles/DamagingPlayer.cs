using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingPlayer : MonoBehaviour
{
    public int m_iDamage;
    [Header("DestoryingSelf")]
    public bool m_bDestoryafterFinishing = true;
    public float m_fTimerToDes;
    public float m_fMaxTimerToDes = 2;
    public float m_fTimerTo;
    public float m_fMaxTimerTo = 2;
    public Vector3 m_vec3TravelDirection;
    // Start is called before the first frame update
    void Start()
    {
        if (m_fMaxTimerToDes == 0)
        {
            m_fMaxTimerToDes = 2;
        }
        m_fTimerToDes = m_fMaxTimerToDes;
        if (m_fMaxTimerTo == 0)
        {
            m_fMaxTimerTo = 2;
        }
        m_fTimerTo = m_fMaxTimerTo;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_fTimerTo <= 0)
        {
            if (m_bDestoryafterFinishing)
            {
                GetComponent<Rigidbody>().useGravity = true;
                if (m_fTimerToDes <= 0)
                {
                    Destroy(gameObject);
                }
                else
                {
                    m_fTimerToDes -= Time.deltaTime;
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            gameObject.transform.Translate(m_vec3TravelDirection);
            m_fTimerTo -= Time.deltaTime;
        }
    }

    //this will need to be looked agian
    private void OnTriggerEnter(Collider other)
    {
        if (!(m_fTimerTo <= 0))
        {
            if (other.CompareTag("Player"))
            {
                //other.GetComponent<PlayerController>().DamagePlayer(m_intDamage);
                DamageMessage message = new DamageMessage();
                message.damage = m_iDamage;
                message.source = gameObject;
                other.GetComponent<DamageController>().ApplyDamage(message);
                GameObject.FindGameObjectWithTag("UI").GetComponent<DisplayStat>().HP = GameObject.FindGameObjectWithTag("UI").GetComponent<DisplayStat>().HP - m_iDamage;
                GameObject.FindGameObjectWithTag("UI").GetComponent<DisplayStat>().NewHealth(GameObject.FindGameObjectWithTag("UI").GetComponent<DisplayStat>().HP);
                m_fTimerTo = 0;

            }
        }
    }
}
