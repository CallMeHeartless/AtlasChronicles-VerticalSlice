using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] private Animator m_rAnimator;

    //Temp. must incorporate more
    DialogueActivator m_rDialogueZone;

    private QuadLookAt m_rInfoBubble;
    private GameObject m_rPlayer;
    private GameObject m_rUIGamePanel;
    private GameObject m_rNLModel;

    float m_fRustleCounter = 0.0f;
    bool m_bInteracting = false;
    bool m_bExited = false;
    bool m_bWithinRadius = false;
    bool m_bFirstEntry = true;
    bool m_bTalking = false;
    bool m_bMoving = false;

    Vector3 m_vecNLRotation = new Vector3(0.0f, -90.0f, 0.0f);

    private AnimatorClipInfo[] clipInfo;

    // Start is called before the first frame update
    void Start()
    {
        m_rPlayer = GameObject.FindGameObjectWithTag("Player");
        m_rInfoBubble = GetComponentInChildren<QuadLookAt>();
        m_rInfoBubble.gameObject.SetActive(false);
        m_rDialogueZone = gameObject.transform.parent.GetComponent<DialogueActivator>();
        m_rUIGamePanel = GameObject.FindGameObjectWithTag("UIGamePanel");
        m_rNLModel = m_rAnimator.gameObject;

        m_bTalking = false;
        m_bInteracting = false;
        m_bExited = true;
        m_bWithinRadius = false;
        m_bFirstEntry = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bInteracting)
        {
            if (!m_bTalking && m_rDialogueZone.GetIsTalking())
            {
                m_rAnimator.SetTrigger("Talk");
                m_bTalking = true;
            }
            else if (m_bTalking && !m_rDialogueZone.GetIsTalking())
            {
                m_rAnimator.SetTrigger("Idle");
                m_bTalking = false;
            }

            if (!m_rDialogueZone.GetIsConversing())
            {
                HideNovLonesome();
                GameState.SetCinematicFlag(false);
                m_rUIGamePanel.SetActive(true);
                m_bInteracting = false;
            }
            return;
        }

        if (Vector3.Distance(transform.position, m_rPlayer.transform.position) <= 5.0f 
            && !m_rAnimator.GetCurrentAnimatorStateInfo(0).IsName("TeleportArrive"))
        {
            m_bWithinRadius = true;
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

            if (m_fRustleCounter >= 5.0f)
            {
                m_fRustleCounter = 0.0f;
                m_rAnimator.SetTrigger("Rustle");
            }
            else
            {
                m_fRustleCounter += Time.deltaTime;
            }


            //If X button pressed
            if (Input.GetButtonDown("XBoxXButton") && !m_bInteracting && GameState.DoesPlayerHaveControl()
                && (m_rAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hidden") || m_rAnimator.GetCurrentAnimatorStateInfo(0).IsName("Rustle")))
            {
                m_bInteracting = true;
                m_rAnimator.SetTrigger("PopOut");
                m_rInfoBubble.gameObject.SetActive(false);
                m_rDialogueZone.TriggerDialogue();
                GameState.SetCinematicFlag(true);
                m_rUIGamePanel.SetActive(false);

                //Rotate both player and novlonesome towards each other
                RotateTowardsPos(this.transform, m_rPlayer.transform);
                RotateTowardsPos(m_rPlayer.transform, this.transform);
                m_rNLModel.transform.localRotation = Quaternion.Euler(m_vecNLRotation);
            }
        }
        else
        {
            if (m_rAnimator.GetCurrentAnimatorStateInfo(0).IsName("TeleportArrive"))
                return;

            if (!m_rAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hidden")
            && !m_rAnimator.GetCurrentAnimatorStateInfo(0).IsName("PopIn")
            && !m_rAnimator.GetCurrentAnimatorStateInfo(0).IsName("Rustle"))
            {
                HideNovLonesome();
            }
            m_rInfoBubble.gameObject.SetActive(false);
            m_bExited = true;
            m_bFirstEntry = true;
            m_bInteracting = false;
            
        }
    }

    public void HideNovLonesome()
    {
        m_bWithinRadius = false;

        m_rAnimator.ResetTrigger("Rustle");
        m_rAnimator.ResetTrigger("Talk");
        m_rAnimator.ResetTrigger("PopIn");
        m_rAnimator.SetTrigger("PopIn");

        m_bExited = true;
        m_bFirstEntry = true;
        m_bInteracting = false;
    }

    void RotateTowardsPos(Transform _toRotate, Transform _target)
    {

        Vector3 targetRotation = new Vector3(
            _target.transform.position.x,
            _toRotate.transform.position.y,
            _target.transform.position.z
        );

        _toRotate.transform.LookAt(targetRotation);
    }

    public void TeleportToDestination(Transform _location)
    {
        this.transform.parent = _location;

        //Reassign dialogue zone
        m_rDialogueZone = transform.parent.GetComponent<DialogueActivator>();
        this.transform.localPosition = Vector3.zero;

        m_rAnimator.SetTrigger("Teleport");

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
}
