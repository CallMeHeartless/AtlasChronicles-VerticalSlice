using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private GameObject m_rContinueButton;
    private TextMeshProUGUI m_rDialogueText;
    private Animator m_rContainerAnimator;
    private GameObject m_rDialoguePanel;
    [SerializeField] AudioPlayer m_speakAudio;

    private Queue<string> m_sentences;

    private int queueCount = 0;
    private bool m_skipDialogue = false;
    private string m_strCurrentDialogue = "";
    private string m_strCurrentSentence = "";
    private string m_strNext = "BButton";
    private string m_strComplete = "Jump";

    // Start is called before the first frame update
    void Start()
    {
        m_sentences = new Queue<string>();

        m_rDialoguePanel = GameObject.FindGameObjectWithTag("DialoguePanel");
        //m_rContinueButton = m_rDialoguePanel.GetComponentInChildren<Button>().gameObject;
        m_rDialogueText = m_rDialoguePanel.GetComponentInChildren<TextMeshProUGUI>();
        m_rContainerAnimator = m_rDialoguePanel.GetComponentInChildren<Animator>();
    }

    public void Update()
    {
        if (Input.GetButtonDown(m_strComplete))
        {
            FillSentence();
        }
        if (Input.GetButtonDown(m_strNext))
        {
            DisplayNextSentence();
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


    public void StartDialogue(Dialogue _dialogue)
    {
        m_strCurrentDialogue = _dialogue.m_strName;
        Debug.Log("Starting conversation: " + _dialogue.m_strName);

        m_sentences.Clear();

        foreach (string sentence in _dialogue.m_sentences)
        {
            m_sentences.Enqueue(sentence);
        }
        BeginDialogue(true);

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        //FillSentence();
        if (m_sentences.Count == 0)
        {
            queueCount = 0;
            BeginDialogue(false);
            return;
        }

        ++queueCount;

        m_strCurrentSentence = m_sentences.Dequeue();

        StopAllCoroutines();

        if (m_strCurrentDialogue == "Welcome")
        {
            StartCoroutine(TypeSentence(m_strCurrentSentence, m_rDialogueText));
        }
    }

    void FillSentence()
    {
        StopAllCoroutines();
        m_rDialogueText.text = m_strCurrentSentence;
    }

    IEnumerator TypeSentence(string _sentence, TextMeshProUGUI _textBox)
    {
        _textBox.text = "";
        foreach (char letter in _sentence.ToCharArray())
        {
            _textBox.text += letter;
            m_speakAudio.PlayAudio();
            yield return null;
        }
    }

    void BeginDialogue(bool _begin)
    {
        if (_begin)
        {
            Debug.Log("Beginning " + m_strCurrentDialogue);
            StopAllCoroutines();
            //m_rContinueButton.SetActive(true);
            m_rContainerAnimator.SetBool("Activate", true);
        }
        else
        {
            Debug.Log("End of " + m_strCurrentDialogue);
            StopAllCoroutines();
            //m_rContinueButton.SetActive(false);
            m_rContainerAnimator.SetBool("Activate", false);
        }
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
