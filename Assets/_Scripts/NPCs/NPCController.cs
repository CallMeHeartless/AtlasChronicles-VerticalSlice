using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] private Animator m_rAnimator;
    private QuadLookAt m_rInfoBubble;
    private GameObject m_rPlayer;
    float m_fRustleCounter = 0.0f;
    bool m_bInteracting = false;
    bool m_bExited = false;
    bool m_bWithinRadius = false;

    // Start is called before the first frame update
    void Start()
    {
        m_rPlayer = GameObject.FindGameObjectWithTag("Player");
        m_rInfoBubble = GetComponentInChildren<QuadLookAt>();
        m_rInfoBubble.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(m_bWithinRadius)
        {
            if (!m_bInteracting)
            {
                if (m_fRustleCounter >= 8.0f)
                {
                    m_fRustleCounter = 0.0f;
                    RandomRustle();
                }
                else
                {
                    m_fRustleCounter += Time.deltaTime;
                }
            }

            //If X button pressed
            if (Input.GetButtonDown("XBoxXButton"))
            {
                Debug.Log("YEETS");
                m_bInteracting = true;
                m_rAnimator.SetTrigger("PopOut");
                m_rInfoBubble.gameObject.SetActive(false);
                //SmoothDampAngle
                //transform.LookAt(m_rPlayer.transform.position);
                //Activate dialogue
            }
        }
    }

    void RandomRustle()
    {
        int rand = Random.Range(0, 1);
        
        if(rand == 1)
        {
            m_rAnimator.SetTrigger("Rustle");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            m_bExited = false;
            m_rAnimator.SetTrigger("Rustle");
            //Show interaction button
            m_rInfoBubble.gameObject.SetActive(true);
            m_bWithinRadius = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(!m_bExited)
        {
            if (!m_rAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hidden"))
            {
                m_rAnimator.SetTrigger("PopIn");
            }
            m_rInfoBubble.gameObject.SetActive(false);
            m_bExited = true;
        }
    }
}
