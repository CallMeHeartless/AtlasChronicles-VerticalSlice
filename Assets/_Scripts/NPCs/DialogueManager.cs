using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum SprController
{
    eCtrlX = 0,     // Attack
    eCtrlY = 1,     // Next Goal
    eCtrlA = 2,     // Jump
    eCtrlB = 3,     // Run
    eCtrlAxisL = 4, // Move
    eCtrlLB = 6,    // Teleport Marker
    eCtrlRB = 7,    // Teleport to marker
    eCtrlStart = 8, // Start
    eCtrlLT = 9,   // Switch Tag
    eCtrlRT = 10,   // Teleport to switch tag

    //eCtrlAxisR = 5, // Camera
}

public enum SprKeyboard
{
    eKeyLMouse = 11, // Attack
    eKeyF = 12,       // Next Goal
    eKeySpace = 13,   // Jump
    eKeyRMouse = 14, // Run
    eKeyAxisL = 15,   // Move
    eKeyQ = 16,       // Teleport Marker
    eKeyE = 17,       // Teleport to marker
    eKeyEsc = 18,     // Start
    eKeyCtrl = 19,    // Switch Tag
    eKeyC = 20,      // Teleport to switch tag
}

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
    private GameObject m_rContainerPanel;
    private NPCController m_rNPCReference;
    
    private Queue<string> m_sentences;
    private System.Array m_eKeySprites;


    private int queueCount = 0;
    private bool m_skipDialogue = false;
    private string m_strCurrentDialogue = "";
    private string m_strCurrentSentence = "";
    private string m_strNext = "Jump";
    private string m_strNext2 = "XBoxXButton";
    private string m_strStart = "XBoxStart";
    //private string m_strComplete = "Jump";

    private bool m_bTyping = false;
    private bool m_bConversing = false;
    public static bool s_bInputController = true;

    // Start is called before the first frame update
    void Start()
    {
        m_sentences = new Queue<string>();

        m_rNPCReference = FindObjectOfType<NPCController>();
        m_rDialoguePanel = GameObject.FindGameObjectWithTag("DialoguePanel");

        m_rContinueButton = m_rDialoguePanel.GetComponentInChildren<Button>(true);
        m_rDialogueText = m_rDialoguePanel.GetComponentInChildren<TextMeshProUGUI>(true);
        m_rDialogueText.gameObject.SetActive(false);
        m_rContainerAnimator = m_rDialoguePanel.GetComponentInChildren<Animator>(true);
        m_rContainerPanel = m_rContainerAnimator.gameObject;
        m_rContainerPanel.gameObject.SetActive(true);
        m_rContinueButton.onClick.AddListener(InteractSentence);

        m_eKeySprites = System.Enum.GetValues(typeof(SprKeyboard));
    }

    public void Update()
    {
        //Dont process input if game is paused
        if (GameState.GetPauseFlag())
        {
            return;
        }

        if (Input.GetButtonDown(m_strNext) || Input.GetButtonDown(m_strNext2))
        {
            InteractSentence();
        }

        if (Input.anyKeyDown)
        {
            s_bInputController = false;
        }

        if (Input.GetAxis("XBoxLT") > 0    || Input.GetAxis("XBoxRT") > 0
              || Input.GetAxis("XBoxHor") != 0  || Input.GetAxis("XBoxVert") != 0
              || Input.GetAxis("XBoxRHor") != 0 || Input.GetAxis("XBoxRVert") != 0)
              //  || Input.GetAxis("DPadX") != 0 || Input.GetAxis("DPadY") != 0)
        {
            //If LT, RT, horizontal, vertical, RHorizontal and RVertical 
            //buttons are pressed on controller
            s_bInputController = true;
        }
        
        //If any joystick keys are pressed on the xbox controller
        for (int i = 0; i < 20; i++)
        {
            if (Input.GetKeyDown("joystick 1 button " + i))
            {
                s_bInputController = true;

            }
        }


        print((s_bInputController ? "Controller" : "Key"));
    }

    /// <summary>
    /// Begin dialogue by queuing all sentences from the conversation
    /// </summary>
    /// <param name="_dialogue">The particular dialogue conversation to begin</param>
    public void StartDialogue(Dialogue _dialogue)
    {
        if (!m_rContainerAnimator.GetCurrentAnimatorStateInfo(0).IsName("HiddenContainer"))
        {
            return;
        }

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

        if(!s_bInputController)
        {
            ReplaceWithKeySprite(ref m_strCurrentSentence);
        }

        //Stop all active coroutines that happen to be running at the same time
        //Note: StopAllCoroutines only stops coroutines occuring on the same script
        StopAllCoroutines();

        //Check for the type of conversation and start typing the sentence
            //Dif types of convos might have different functionalities 
            //  In coordination with queueCount
        //if (m_strCurrentDialogue == "Welcome")
        //{
        StartCoroutine(TypeSentence(m_strCurrentSentence, m_rDialogueText));
        //}
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
        else if(m_bConversing && m_rContainerAnimator.GetCurrentAnimatorStateInfo(0).IsName("IdleContainer"))
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
        bool spriteEncountered = false;
        _textBox.text = "";
        m_bTyping = true;
        foreach (char letter in _sentence.ToCharArray())
        {
            if (letter == '<')
            {
                //Skip typing effect when a sprite is encountered by continuing the forloop and 
                //  completing the sprite before displaying it in the text box
                spriteEncountered = true;
            }

            if (letter == '>')
            {
                //Begin typing effect again when the end of the sprite call has been encountered.
                spriteEncountered = false;
            }

            _textBox.text += letter;

            //Skip the char interation if a sprite is encountered.
            if (spriteEncountered)
                continue;

            //Play a speaking sound while each letter is spoken
            m_speakAudio.PlayAudio();
            yield return null;
        }
        m_bTyping = false;
    }

    string ControllerSpriteToKey(SprController _spr)
    {
        int index = System.Array.IndexOf(System.Enum.GetValues(_spr.GetType()), _spr);

        for (int i = 0; i <= m_eKeySprites.Length; ++i)
        {
            if (index == i)
            {
                SprKeyboard key = (SprKeyboard)(m_eKeySprites.GetValue(i));
                return ((int)key).ToString();
            }
        }
        return "";
    }

    void ReplaceWithKeySprite(ref string _line)
    {
        //Check static  s_bInputController before this method
        string newFullString = "";

        if (_line.Contains("<sprite="))
        {
            bool numberReached = false;
            bool numberGathered = false;
            string replacementString = "";

            foreach (char letter in _line.ToCharArray())
            {
                if (letter == '>')
                {
                    //Begin typing effect again when the end of the sprite call has been encountered.
                    numberReached = false;
                }

                if (numberReached)
                {
                    replacementString += letter;
                    numberGathered = true;
                }


                if (!numberReached && !numberGathered)
                {
                    newFullString += letter;
                }

                if (letter == '=')
                {
                    numberReached = true;
                }

                //ready to replace sprite
                if (numberGathered && !numberReached)
                {
                    replacementString = ControllerSpriteToKey((SprController)StringToInt(replacementString));
                    newFullString += replacementString + ">";
                    replacementString = "";
                    numberGathered = false;
                }



            }
            _line = newFullString;
        }
    }

    int StringToInt(string _str)
    {
        int.TryParse(_str, out int intToReturn);
        return intToReturn;
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
