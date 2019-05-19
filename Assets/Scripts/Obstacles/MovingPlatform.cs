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

    [Header("testing Don't Touch")]
    public float m_fPauseBreak = 0;
    public bool m_fCurrentBreak = false;
    public float timeITakes = 0;
    public float jounlylength;
    public float destionlength;


    [Header("shinking")]
    public bool Shink = false;
    public float ShinkSize = 1;
    private BoxCollider box;

    [Header("destorying")]
    public bool m_bDestoryAtPoint;
    public int m_intBreakableAtPoint;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(m_rPoints.Length);
        if (Shink)
        {
            box = GetComponent<BoxCollider>();
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (m_fCurrentPause <= 0)
        {
            transform.position = Vector3.MoveTowards(gameObject.transform.position, m_rPoints[m_intCurrentPoint].transform.position, m_fSpeed);
            timeITakes += Time.deltaTime;


            if (m_fCurrentBreak == true)
            {
                if (m_fPauseBreak>= 0)
                {


                    if (m_intCurrentPoint == 0)
                    {
                        box.center += new Vector3(0.1f * ShinkSize, 0, 0);
                    }
                    else
                    {
                        box.center += new Vector3(-0.1f * ShinkSize, 0, 0);
                    }
                    m_fPauseBreak -= Time.deltaTime;

                }
                else
                {
                    m_fCurrentBreak = false;
                }
            }
            else
            {
                if (Shink)
                {
                    //shinking2();
                    shinking();
                }
            }
           


                if (Vector3.Distance(transform.position, m_rPoints[m_intCurrentPoint].transform.position) < 1)
            {
                Debug.Log(timeITakes);
                timeITakes = 0;
                if (m_rPoints.Length - 1 == m_intCurrentPoint)
                {
                    m_fCurrentPause = m_fPauseDuration;
                    m_intCurrentPoint = 0;
                    box.size = new Vector3(1, 1, 1);
                    box.center = new Vector3(0, 0, 0);
                }
                else
                {
                    m_fCurrentPause = m_fPauseDuration;
                    m_intCurrentPoint++;
                }

                if (m_fPauseBreak>0)
                {
                    m_fCurrentBreak = true;
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

    void shinking2(){
        //gameObject.transform.localScale += new Vector3(0.1f * ShinkSize * 2, 0, 0);
    }


    void shinking()
    {
       
            if (m_intCurrentPoint == 0)
            {
                if (-ShinkSize > 0)
                {
                   // Debug.Log("in");
                    if (box.size.x+ ShinkSize >= 0)
                    {
                        //set to zero
                        box.size += new Vector3(0.1f * ShinkSize * 2, 0, 0);
                        box.center += new Vector3(0.1f * ShinkSize, 0, 0);
                    }
                    else
                    {
                        box.size = new Vector3(0, 1, 1);
                        box.center += new Vector3(0.1f * ShinkSize, 0, 0);
                        m_fPauseBreak += Time.deltaTime;
                        
                    }
                }
                else
                {
                    box.size += new Vector3(0.1f * ShinkSize * 2, 0, 0);
                    box.center += new Vector3(0.1f * ShinkSize, 0, 0);

                }

            }
            else
            {
                if (ShinkSize > 0)
                {
                    //Debug.Log("in");
                    if (box.size.x+ShinkSize >= 0)
                    {
                        //set to zero
                        box.size += new Vector3(-0.1f * ShinkSize * 2, 0, 0);
                        box.center += new Vector3(-0.1f * ShinkSize, 0, 0);
                    }
                    else
                    {
                        box.size = new Vector3(0, 1, 1);
                        box.center += new Vector3(-0.1f * ShinkSize, 0, 0);
                        m_fPauseBreak += Time.deltaTime;
                        
                    }
                }
                else
                {
                    box.size += new Vector3(-0.1f * ShinkSize * 2, 0, 0);
                    box.center += new Vector3(-0.1f * ShinkSize, 0, 0);

                }
            }
        
    }
}
