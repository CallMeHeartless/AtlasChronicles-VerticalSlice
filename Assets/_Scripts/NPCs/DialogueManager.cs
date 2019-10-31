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
    private System.Array m_eControllerSprites;

    private int queueCount = 0;
    private bool m_skipDialogue = false;
    private string m_strCurrentDialogue = "";
    private string m_strCurrentSentence = "";
    private string m_strNext = "AButton";
    private string m_strNext2 = "BButton";
    private string m_strNext3 = "XBoxXButton";
    private string m_strSkip = "YButton";

    private string m_strStart = "XBoxStart";
    //private string m_strComplete = "Jump";

    private bool m_bTyping = false;
    private bool m_bConversing = false;
    //public static bool s_bInputController = true;

    // Start is called before the first frame update
    void Start()
    {
        m_sentences = new Queue<string>();

        //Assign all references required
        m_rNPCReference = FindObjectOfType<NPCController>();
        m_rDialoguePanel = GameObject.FindGameObjectWithTag("DialoguePanel");
        m_rContinueButton = m_rDialoguePanel.GetComponentInChildren<Button>(true);
        m_rDialogueText = m_rDialoguePanel.GetComponentInChildren<TextMeshProUGUI>(true);
        m_rDialogueText.gameObject.SetActive(false);
        m_rContainerAnimator = m_rDialoguePanel.GetComponentInChildren<Animator>(true);
        m_rContainerPanel = m_rContainerAnimator.gameObject;
        m_rContainerPanel.gameObject.SetActive(true);
        m_rContinueButton.onClick.AddListener(InteractSentence);

        //Put keyboard sprites into an array so it is easier to correspond the controller sprites to it
        m_eKeySprites = System.Enum.GetValues(typeof(SprKeyboard));
        m_eControllerSprites = System.Enum.GetValues(typeof(SprController));
    }

    public void Update()
    {
        //Check whether the keyboard or controller was last pressed
        //InputChecker();
        //Dont process input if game is paused
        if (GameState.GetPauseFlag() || !m_bConversing)
            return;

        if (Input.GetButtonDown(m_strNext) 
            || Input.GetButtonDown(m_strNext2) 
            || Input.GetButtonDown(m_strNext3))
        {
            InteractSentence();
        }

        if(Input.GetButtonDown(m_strSkip) && m_bConversing)
        {
            BeginDialogue(false);
        }
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

        if(!InputManager.s_bInputController)
        {
            ReplaceWithKeySprite(ref m_strCurrentSentence);
        }

        //Stop all active coroutines that happen to be running at the same time
        //Note: StopAllCoroutines only stops coroutines occuring on the same script
        StopAllCoroutines();
        
        //Begin typing sentence one at a time
        StartCoroutine(TypeSentence(m_strCurrentSentence, m_rDialogueText));
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
    /// Coroutine that types each letter/character of the sentence individually.
    /// Sprite codes encountered will not be printed individually but all together at once so a sprite will display immediately
    /// </summary>
    /// <param name="_sentence">Current sentence to type</param>
    /// <param name="_textBox">Current text box to write to (useful if multiple text boxes are required)</param>
    IEnumerator TypeSentence(string _sentence, TextMeshProUGUI _textBox)
    {
        bool spriteEncountered = false;
        bool newWord = true;
        _textBox.text = "";
        m_bTyping = true;
        foreach (char letter in _sentence.ToCharArray())
        {
            if (newWord)
            {
                m_speakAudio.PlayAudio();
                newWord = false;
            }

            if(letter == ' ')
            {
                newWord = true;
            }

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

            //Add a letter to the text box
            _textBox.text += letter;

            //Skip the char iteration if a sprite is encountered.
            if (spriteEncountered)
                continue;

            //Play a speaking sound while each letter is spoken and then execute/push char into text box
            //Randomize chance to play audio 2/5 chances

            yield return null;
        }
        m_bTyping = false;
    }

    /// <summary>
    /// Converts a Controller sprite to a Keyboard/mouse sprite based on the index of the enums
    /// </summary>
    /// <param name="_spr">The SprCotnroller sprite to be converted to a key sprite</param>
    /// <returns>Returns the string of a keyboard sprite that is equivalent to that of the controller sprite</returns>
    string ControllerSpriteToKey(SprController _spr)
    {
        //Get the index of the given sprite in the list of controller sprites
        int index = System.Array.IndexOf(m_eControllerSprites, _spr);

        //Find the equivalent index of the sprite in SprKeyboard
        for (int i = 0; i <= m_eKeySprites.Length; ++i)
        {
            if (index == i)
            {
                //Get the enum of the key based on the index found.
                SprKeyboard key = (SprKeyboard)(m_eKeySprites.GetValue(i));
                //Return the integer value (not the index) of the SprKeyboard sprite as a string
                return ((int)key).ToString();
            }
        }
        return "";
    }

    /// <summary>
    /// Replace all Controller sprite codes with Key sprites within a single sentence
    /// </summary>
    /// <param name="_line">The reference to the line to be replaced</param>
    void ReplaceWithKeySprite(ref string _line)
    {
        //Check static  s_bInputController before this method
        string newFullString = "";

        //If sentence contains both the keyboard and controller sprites, do not replace sprites.
        if (_line.Contains("(Both)"))
        {
            _line.Replace("(Both)", "");
            return;
        }

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
                    //Once the number in the sprite code is reached, start appending chars
                    replacementString += letter;
                    numberGathered = true;
                }

                if (!numberReached && !numberGathered)
                {
                    //Continue appending the string as normal
                    newFullString += letter;
                }

                if (letter == '=')
                {
                    //The number char is next after the loop ends
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

    /// <summary>
    /// Converts string to int
    /// </summary>
    /// <param name="_str">The string to convert to an int</param>
    /// <returns>the integer version of a string</returns>
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
            //Starts up the dialogue functionality
            StopAllCoroutines();
            m_rContainerAnimator.SetBool("Activate", true);
            m_bConversing = true;

            StartCoroutine(ActivateDialogue(true));
        }
        else
        {
            //Ends the dialogue functionality
            StopAllCoroutines();
            m_rContinueButton.gameObject.SetActive(false);
            m_rContainerAnimator.SetBool("Activate", false);
            m_bConversing = false;
            StartCoroutine(ActivateDialogue(false));
        }
    }

    /// <summary>
    /// Begins the dialogue sequence or ends it
    /// </summary>
    /// <param name="_activate">decides whether to activate or deativate dialogue sequence</param>
    /// <returns>coroutine</returns>
    IEnumerator ActivateDialogue(bool _activate)
    {
        if (_activate)
        {
            //Activates next line in dialogue
            while (!m_rContainerAnimator.GetCurrentAnimatorStateInfo(0).IsName("IdleContainer"))
            {
                yield return null;
            }
            DisplayNextSentence();
        }
        else
        {
            //Ends dialogue segment
            while (!m_rContainerAnimator.GetCurrentAnimatorStateInfo(0).IsName("DeactivateContainer"))
            {
                yield return null;
            }
            m_bConversing = false;
        }

        yield return null;
    }
}
