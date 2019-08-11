using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialGuide : MonoBehaviour
{
    private GameObject m_rGuidePanel;
    private TextMeshProUGUI m_rText;

    // Start is called before the first frame update
    void Start()
    {
        //Unity apparently cant find tags if they are initially inactive so therefore i have to manually set it inactive at the start
        m_rGuidePanel = GameObject.FindWithTag("Guide");
        m_rText = m_rGuidePanel.GetComponentInChildren<TextMeshProUGUI>();
        m_rGuidePanel.SetActive(false);
    }

    public void SetGuidePanelActive(bool _active)
    {
        if(m_rGuidePanel && GameState.DoesPlayerHaveControl())
        {
            m_rGuidePanel.SetActive(_active);
        }
    }

    public void SetGuideText(string _guideText)
    {
        if (m_rGuidePanel && GameState.DoesPlayerHaveControl())
        {
            m_rText.SetText(_guideText);
        }
    }
}
