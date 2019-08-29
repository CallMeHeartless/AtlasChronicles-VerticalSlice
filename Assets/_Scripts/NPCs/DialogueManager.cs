using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Vivian
/// </summary>
public class DialogueManager : MonoBehaviour
{
    [SerializeField] AudioPlayer m_speakAudio;

    private Button m_rContinueButton;
    private TextMeshProUGUI m_rDialogueText;
    private Animator m_rContainerAnimator;
    private GameObject m_rDialoguePanel;

    private Queue<string> m_sentences;

    private int queueCount = 0;
    private bool m_skipDialogue = false;
    private string m_strCurrentDialogue = "";
    private string m_strCurrentSentence = "";
    private string m_strNext = "XBoxXButton";
    //private string m_strComplete = "Jump";

    private bool m_bTyping = false;
    private bool m_bConversing = false;

    // Start is called before the first frame update
    void Start()
    {
        m_sentences = new Queue<string>();

        m_rDialoguePanel = GameObject.FindGameObjectWithTag("DialoguePanel");
        m_rContinueButton = m_rDialoguePanel.GetComponentInChildren<Button>();
        m_rDialogueText = m_rDialoguePanel.GetComponentInChildren<TextMeshProUGUI>();
        m_rContainerAnimator = m_rDialoguePanel.GetComponentInChildren<Animator>();

        m_rContinueButton.onClick.AddListener(InteractSentence);
    }

    public void Update()
    {
        if (Input.GetButtonDown(m_strNext))
        {
            InteractSentence();
        }

        //if (!m_skipDialogue)
        //{
        //    m_skipDialogue = true;
        //    BeginDialogue(false);
        //}
        //else
        //{
        //    SkipDialogue();
        //}
    }

    /// <summary>
    /// Begin dialogue by queuing all sentences from the conversation
    /// </summary>
    /// <param name="_dialogue">The particular dialogue conversation to begin</param>
    public void StartDialogue(Dialogue _dialogue)
    {
        if(!m_rContainerAnimator.GetCurrentAnimatorStateInfo(0).IsName("HiddenContainer"))
            return;

        //Hide text box so it is not displayed when the panel animation is running
        m_rDialogueText.gameObject.SetActive(false);
        //Set name of the current dialogue 
        m_strCurrentDialogue = _dialogue.m_strName;
        //Debug.Log("Starting conversation: " + m_strCurrentDialogue);

        //Clear anything that was previously in the sentence queue
        m_sentences.Clear();

        //Queue all the sentences to be parsed when interacted
        foreach (string sentence in _dialogue.m_sentences)
        {
            m_sentences.Enqueue(sentence);
        }

        //Begin the dialogue interaction sequence
        BeginDialogue(true);
    }

    /// <summary>
    /// Displays the next sentence in the conversation
    /// </summary>
    public void DisplayNextSentence()
    {
        //If there are no more sentences, end the dialogue sequence
        if (m_sentences.Count == 0)
        {
            queueCount = 0;
            BeginDialogue(false);
            return;
        }

        if (!m_rDialogueText.gameObject.activeSelf)
        {
            m_rDialogueText.gameObject.SetActive(true);
        }
        //Increase queuecount so we can know which line we are 
        //      up to if trying to add actions and so forth
        ++queueCount;

        //Retrieve first sentence of the queue while removing it at the same time
        m_strCurrentSentence = m_sentences.Dequeue();

        //Stop all active coroutines that happen to be running at the same time
        //Note: StopAllCoroutines only stops coroutines occuring on the same script
        StopAllCoroutines();

        //Check for the type of conversation and start typing the sentence
            //Dif types of convos might have different functionalities 
            //  In coordination with queueCount
        if (m_strCurrentDialogue == "Welcome")
        {
            StartCoroutine(TypeSentence(m_strCurrentSentence, m_rDialogueText));
        }
    }

    /// <summary>
    /// Getter to check if the NPC is currently talking/text is typing
    /// </summary>
    /// <returns></returns>
    public bool GetIsTalking()
    {
        return m_bTyping;
    }

    /// <summary>
    /// Getter to check if player is conversing with NPC
    /// </summary>
    /// <returns></returns>
    public bool GetIsConversing()
    {
        return m_bConversing;
    }

    /// <summary>
    /// Stops currently-typing text from writing and 
    /// fills text box with complete sentence instead
    /// </summary>
    void InteractSentence()
    {
        if (m_bTyping)
        {
            //Fill out the current sentence after stopping all current coroutines.
            StopAllCoroutines();
            m_rDialogueText.text = m_strCurrentSentence;
            m_bTyping = false;
        }
        else
        {
            DisplayNextSentence();
        }
    }

    /// <summary>
    /// Coroutine that types each letter/character of the sentence individually
    /// </summary>
    /// <param name="_sentence">Current sentence to type</param>
    /// <param name="_textBox">Current text box to write to (useful if multiple text boxes are required)</param>
    IEnumerator TypeSentence(string _sentence, TextMeshProUGUI _textBox)
    {
        _textBox.text = "";
        m_bTyping = true;
        foreach (char letter in _sentence.ToCharArray())
        {
            _textBox.text += letter;
            //Play a speaking sound while each letter is spoken
            m_speakAudio.PlayAudio();   
            yield return null;
        }
        m_bTyping = false;
    }

    /// <summary>
    /// Function that begins or ends a dialogue/conversation
    /// </summary>
    /// <param name="_begin">Variable that determines whether the conversation should end or not</param>
    void BeginDialogue(bool _begin)
    {
        if (_begin)
        {
            StopAllCoroutines();
            m_rContinueButton.gameObject.SetActive(true);
            m_rContainerAnimator.SetBool("Activate", true);
            m_bConversing = true;
            StartCoroutine(ActivateDialogue(true));
        }
        else
        {
            StopAllCoroutines();
            m_rContinueButton.gameObject.SetActive(false);
            m_rContainerAnimator.SetBool("Activate", false);
            StartCoroutine(ActivateDialogue(false));
        }
    }

    IEnumerator ActivateDialogue(bool _activate)
    {
        if (_activate)
        {
            while (!m_rContainerAnimator.GetCurrentAnimatorStateInfo(0).IsName("IdleContainer"))
            {
                yield return null;
            }
            DisplayNextSentence();
        }
        else
        {
            while (!m_rContainerAnimator.GetCurrentAnimatorStateInfo(0).IsName("DeactivateContainer"))
            {
                yield return null;
            }
            m_bConversing = false;
        }

        yield return null;
    }

    public void SkipDialogue()
    {

    } 

    void SwitchOffMusic()
    {
        //turn off all audio
        //m_speakAudio.Stop();
    }


}
