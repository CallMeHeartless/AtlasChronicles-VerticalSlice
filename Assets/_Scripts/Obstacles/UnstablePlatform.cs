using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstablePlatform : MonoBehaviour
{
    private bool m_bCollatsing = false;
    public float m_fTimerToCol;
    public float m_fMaxTimerToCol = 2;
    public float m_fTimerToDes;
    public float m_fMaxTimerToDes = 2;
    public float m_fSpeed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        Starting();
    }
    public void Starting()
    {
        reform();
        if (m_fMaxTimerToCol == 0)
        {
            m_fMaxTimerToCol = 2;
        }
        m_fTimerToCol = m_fMaxTimerToCol;
        if (m_fMaxTimerToDes == 0)
        {
            m_fMaxTimerToDes = 2;
        }
        m_fTimerToDes = m_fMaxTimerToDes;
    }
    // Update is called once per frame
    void Update()
    {
        if (m_bCollatsing)
        {
            if (m_fTimerToDes <= 0)
            {


                if (gameObject.GetComponent<Respawning>() == null)
                {
                    gameObject.AddComponent<Respawning>();
                    gameObject.GetComponent<Respawning>().m_eResetType = Respawning.eResetType.unstable;
                }
                else
                {
                    gameObject.GetComponent<Respawning>().enabled = true;
                }
                gameObject.GetComponent<Respawning>().Starting();

                gameObject.GetComponent<MeshRenderer>().enabled = false;
                gameObject.GetComponent<UnstablePlatform>().enabled = false;
            }
            else
            {
                m_fTimerToDes -= Time.deltaTime;
            }
            transform.Translate(Vector3.down * m_fSpeed);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (m_fTimerToCol <= 0)
            {
                m_bCollatsing = true;
            }
            else
            {
                m_fTimerToCol -= Time.deltaTime;
            }
        }
    }

    public void reform()
    {
        m_bCollatsing = false;
    }
}
