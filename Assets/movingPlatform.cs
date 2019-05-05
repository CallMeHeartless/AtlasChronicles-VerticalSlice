using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("movement")]
    public GameObject[] m_rPoints;
    public int m_intCurrentPoint = 0;
    public float m_fSpeed;

    [Header("delaying")]
    public float m_fPauseDuration;
    public float m_fCurrentPause;

    [Header("destorying")]
    public bool m_bDestoryAtPoint;
    public int m_intBreakableAtPoint;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(m_rPoints.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_fCurrentPause <= 0)
        {
            transform.position = Vector3.MoveTowards(gameObject.transform.position, m_rPoints[m_intCurrentPoint].transform.position, m_fSpeed);

            
            
            if (Vector3.Distance(transform.position, m_rPoints[m_intCurrentPoint].transform.position) < 1)
            {
                if (m_rPoints.Length - 1 == m_intCurrentPoint)
                {
                    m_fCurrentPause = m_fPauseDuration;
                    m_intCurrentPoint = 0;
                }
                else
                {
                    m_fCurrentPause = m_fPauseDuration;
                    m_intCurrentPoint++;
                }

                if (m_bDestoryAtPoint)
                {
                    if (m_intBreakableAtPoint == m_intCurrentPoint)
                    {
                        if (gameObject.GetComponent<PlatformBreaking>() == null)
                        {
                            gameObject.AddComponent<PlatformBreaking>();
                        }
                        else
                        {
                            gameObject.GetComponent<PlatformBreaking>().enabled = true;
                        }

                        StartCoroutine(colapse());
                    }
                }
            }
        }
        else
        {
            m_fCurrentPause -= Time.deltaTime;
        }
        
    }

    IEnumerator colapse()
    {
        
        yield return new WaitForSeconds(.1f);
        gameObject.GetComponent<PlatformBreaking>().Copsate();
        gameObject.GetComponent<MovingPlatform>().enabled = false;
    }
}
