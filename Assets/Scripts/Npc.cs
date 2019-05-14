using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Npc : MonoBehaviour
{
    public Text m_textUiText;
    public string[] m_saTexts;
    public bool m_bTalking =false;
    private int m_iCurrentLine;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            m_textUiText.enabled = true;
            m_bTalking = true;
            m_textUiText.text = m_saTexts[0];
            m_iCurrentLine = 1;
        }
        if (m_bTalking == true)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (m_iCurrentLine == m_saTexts.Length)
                {
                    m_textUiText.enabled = false;
                    m_bTalking = false;
                }
                else
                {

                    m_textUiText.text = m_saTexts[m_iCurrentLine];
                    m_iCurrentLine++;
                }
      
            }
        }
        
    }
}
