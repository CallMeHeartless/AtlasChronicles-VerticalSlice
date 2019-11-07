using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] private Animator m_rAnimator;

    DialogueActivator m_rDialogueZone;

    private QuadLookAt m_rInfoBubble;
    private GameObject m_rPlayer;
    private GameObject m_rUIGamePanel;
    private DisplayStat m_rUIDisplayStats;
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
        //Get all references
        m_rPlayer = GameObject.FindGameObjectWithTag("Player");
        m_rInfoBubble = GetComponentInChildren<QuadLookAt>();
        m_rInfoBubble.gameObject.SetActive(false);
        m_rDialogueZone = gameObject.transform.parent.GetComponent<DialogueActivator>();
        m_rUIGamePanel = GameObject.FindGameObjectWithTag("UIGamePanel");
        m_rUIDisplayStats = GameObject.FindGameObjectWithTag("UI").GetComponent<DisplayStat>();
        m_rNLModel = m_rAnimator.gameObject;

        //Set up initial values
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
            //m_rUIGamePanel.SetActive(false);

            if (!m_bTalking && m_rDialogueZone.GetIsTalking())
            {
                //If interacting and npc has yet to start animating the talk animation, talk
                m_rAnimator.SetTrigger("Talk");
                m_bTalking = true;
            }
            else if (m_bTalking && !m_rDialogueZone.GetIsTalking())
            {
                //If the animation is talking but dialogue has ended, set anim to idle
                m_rAnimator.SetTrigger("Idle");
                m_bTalking = false;
            }

            if (!m_rDialogueZone.GetIsConversing())
            {
                //If conversation has ended, 'hide' npc and switch game mode back to playable state
                HideNovLonesome();
                GameState.SetCinematicFlag(false);
                //m_rUIGamePanel.SetActive(true);
                m_bInteracting = false;
                m_bExited = true;
                m_bFirstEntry = true;
            }
            return;
        }

        //Handle when to pop up the NPC from the ground if it does not yet exist at that position
        if (Vector3.Distance(transform.position, m_rPlayer.transform.position) <= 5.0f
            && !m_rAnimator.GetCurrentAnimatorStateInfo(0).IsName("TeleportArrive")
            && m_rDialogueZone.GetIsContainerHidden())
        {
            m_bWithinRadius = true;

            if (m_bFirstEntry)
            {
                //When npc first appears, activate animation and interaction bubble
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
                //Activates a rustling animation 
                m_fRustleCounter = 0.0f;
                m_rAnimator.SetTrigger("Rustle");
            }
            else
            {
                m_fRustleCounter += Time.deltaTime;
            }


            //If Activation button pressed, activate dialogue
            if (Input.GetButtonDown("BButton") && !m_bInteracting && GameState.DoesPlayerHaveControl()
                && (m_rAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hidden") || m_rAnimator.GetCurrentAnimatorStateInfo(0).IsName("Rustle")))
            {
                m_bInteracting = true;
                m_rAnimator.SetTrigger("PopOut");
                m_rInfoBubble.gameObject.SetActive(false);
                m_rDialogueZone.TriggerDialogue();
                GameState.SetCinematicFlag(true);
                //m_rUIGamePanel.SetActive(false);
                //m_rUIDisplayStats.HideUIGamePanel(true);
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

            //If the npc is out of it's bush, allow hiding animation
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

    /// <summary>
    /// Hides the NPC character and resets all necessary variables
    /// </summary>
    public void HideNovLonesome()
    {
        //Hide the npc
        m_bWithinRadius = false;

        m_rAnimator.ResetTrigger("Rustle");
        m_rAnimator.ResetTrigger("Talk");
        m_rAnimator.ResetTrigger("PopIn");
        m_rAnimator.SetTrigger("PopIn");

        m_bExited = true;
        m_bFirstEntry = true;
        m_bInteracting = false;
    }

    /// <summary>
    /// Rotates npc towards player
    /// </summary>
    /// <param name="_toRotate">Game object to rotate</param>
    /// <param name="_target">Target to rotate towards</param>
    void RotateTowardsPos(Transform _toRotate, Transform _target)
    {
        //Rotate the npc to face towards the player
        Vector3 targetRotation = new Vector3(
            _target.transform.position.x,
            _toRotate.transform.position.y,
            _target.transform.position.z
        );

        _toRotate.transform.LookAt(targetRotation);
    }

    /// <summary>
    /// Teleport npc to given destination
    /// </summary>
    /// <param name="_location">Destination to teleport to</param>
    public void TeleportToDestination(Transform _location)
    {
        //Teleport the npc to the nearest dialogue location the player has entered
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

    /// <summary>
    /// Randomly rustles an idle november lonesome
    /// </summary>
    void RandomRustle()
    {
        //Activates a rustle randomly
        int rand = Random.Range(0, 1);
        
        if(rand == 1)
        {
            m_rAnimator.SetTrigger("Rustle");
        }
    }
}
