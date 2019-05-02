using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawning : MonoBehaviour
{
    public enum eResetType
    {
        breaking,
        breakingMoving,
        unstable
    }

   
    public eResetType m_eResetType;
    public float m_fTimer;
    public float m_fMaxTimer;

    // Start is called before the first frame update
    private void Start()
    {
        Starting();
    }

    public void Starting()
    {

        if (m_fMaxTimer == 0)
        {
            m_fMaxTimer = 2;
        }
        m_fTimer = m_fMaxTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_fTimer <= 0)
        {
            Reset();
        }
        else
        {
            m_fTimer -= Time.deltaTime;
        }
    }

    private void Reset()
    {
        switch (m_eResetType)
        {
            case eResetType.breaking:
                gameObject.transform.position = gameObject.transform.parent.position;
                gameObject.GetComponent<MeshRenderer>().enabled = true;
                gameObject.GetComponent<PlatformBreaking>().enabled = true;
                gameObject.GetComponent<PlatformBreaking>().Starting();

                gameObject.GetComponent<Respawning>().enabled = false;
                break;

            case eResetType.breakingMoving:
                gameObject.transform.position = gameObject.transform.parent.position;
                gameObject.GetComponent<MeshRenderer>().enabled = true;
                gameObject.GetComponent<MovingPlatform>().enabled = true;
                gameObject.GetComponent<MovingPlatform>().m_intCurrentPoint = 0; 

                gameObject.GetComponent<Respawning>().enabled = false;
                break;

            case eResetType.unstable:
                gameObject.transform.position = gameObject.transform.parent.position;
                gameObject.GetComponent<MeshRenderer>().enabled = true;
                gameObject.GetComponent<UnstablePlatform>().enabled = true;
                gameObject.GetComponent<UnstablePlatform>().Starting();

                gameObject.GetComponent<Respawning>().enabled = false;
                break;
            default:
                break;
        }
    }
}
