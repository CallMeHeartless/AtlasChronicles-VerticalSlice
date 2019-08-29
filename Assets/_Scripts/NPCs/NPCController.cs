using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] private Animator m_rAnimator;

    //Temp. must incorporate more
    DialogueActivator m_rWelcomeDialogue;

    private QuadLookAt m_rInfoBubble;
    private GameObject m_rPlayer;
    float m_fRustleCounter = 0.0f;
    bool m_bInteracting = false;
    bool m_bExited = false;
    bool m_bWithinRadius = false;
    bool m_bFirstEntry = true;

    private AnimatorClipInfo[] clipInfo;

    // Start is called before the first frame update
    void Start()
    {
        m_rPlayer = GameObject.FindGameObjectWithTag("Player");
        m_rInfoBubble = GetComponentInChildren<QuadLookAt>();
        m_rInfoBubble.gameObject.SetActive(false);
        m_rWelcomeDialogue = GetComponentInChildren<DialogueActivator>();


        m_bInteracting = false;
        m_bExited = true;
        m_bWithinRadius = false;
        m_bFirstEntry = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, m_rPlayer.transform.position) <= 5.0f)
        {
            m_bWithinRadius = true;
            if (!m_bInteracting)
            {
                if (m_bFirstEntry)
                {
                    m_bExited = false;
                    m_rAnimator.ResetTrigger("PopIn");
                    m_rAnimator.SetTrigger("Rustle");
                    //Show interaction button
                    m_rInfoBubble.gameObject.SetActive(true);
                    m_bWithinRadius = true;
                    m_bFirstEntry = false;
                }

                if (m_fRustleCounter >= 8.0f)
                {
                    m_fRustleCounter = 0.0f;
                    m_rAnimator.SetTrigger("Rustle");
                }
                else
                {
                    m_fRustleCounter += Time.deltaTime;
                }
            }

            //If X button pressed
            if (Input.GetButtonDown("XBoxXButton") && !m_bInteracting)
            {
                //Debug.Log("YEETS");
                m_bInteracting = true;
                m_rAnimator.SetTrigger("PopOut");
                m_rInfoBubble.gameObject.SetActive(false);
                m_rWelcomeDialogue.TriggerDialogue();
                //SmoothDampAngle
                //transform.LookAt(m_rPlayer.transform.position);
                //Activate dialogue
            }
        }
        else
        {
            m_bWithinRadius = false;

            if (!m_rAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hidden")
                && !m_rAnimator.GetCurrentAnimatorStateInfo(0).IsName("PopIn")
                && !m_rAnimator.GetCurrentAnimatorStateInfo(0).IsName("Rustle"))
            {
                m_rAnimator.ResetTrigger("Rustle");
                m_rAnimator.ResetTrigger("PopIn");
                m_rAnimator.SetTrigger("PopIn");

                //print("CurrentState: " + GetCurrentClipName());
                m_bExited = true;
                m_bFirstEntry = true;
                m_bInteracting = false;
            }


            if (m_rInfoBubble.gameObject.activeSelf)
            {
                m_rInfoBubble.gameObject.SetActive(false);
            }

            m_bExited = true;
            m_bFirstEntry = true;
            m_bInteracting = false;
            //if (!m_bExited)
            //{
            //    if (!m_rAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hidden")
            //    && !m_rAnimator.GetCurrentAnimatorStateInfo(0).IsName("PopIn"))
            //    {
            //        m_rAnimator.SetTrigger("PopIn");
            //    }
            //    m_rInfoBubble.gameObject.SetActive(false);
            //    m_bExited = true;
            //    m_bFirstEntry = true;
            //    m_bInteracting = false;

            //    m_rAnimator.ResetTrigger("ResetTrigger");

            //}
        }
    }

    public string GetCurrentClipName()
    {
        clipInfo = m_rAnimator.GetCurrentAnimatorClipInfo(0);
        return clipInfo[0].clip.name;
    }

    void RandomRustle()
    {
        int rand = Random.Range(0, 1);
        
        if(rand == 1)
        {
            m_rAnimator.SetTrigger("Rustle");
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.CompareTag("Player"))
    //    {
    //        m_bExited = false;
    //        m_rAnimator.ResetTrigger("PopIn");
    //        m_rAnimator.ResetTrigger("PopOut");
    //        m_rAnimator.ResetTrigger("Rustle");

    //        m_rAnimator.SetTrigger("Rustle");
    //        //Show interaction button
    //        m_rInfoBubble.gameObject.SetActive(true);
    //        m_bWithinRadius = true;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if(!m_bExited)
    //    {
    //        print("wot");
    //        if (!m_rAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hidden")
    //         && !m_rAnimator.GetCurrentAnimatorStateInfo(0).IsName("PopIn")
    //         && !m_rAnimator.GetCurrentAnimatorStateInfo(0).IsName("Rustle"))
    //        {
    //            m_rAnimator.ResetTrigger("Rustle");
    //            m_rAnimator.ResetTrigger("PopIn");
    //            m_rAnimator.ResetTrigger("PopOut");

    //            m_rAnimator.SetTrigger("PopIn");
    //        }
    //        m_rInfoBubble.gameObject.SetActive(false);
    //        m_bExited = true;
    //        m_bWithinRadius = false;
    //    }
    //}
}
