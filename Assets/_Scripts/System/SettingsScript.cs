using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using MessageSystem;

public class SettingsScript : MonoBehaviour
{
    [SerializeField] Image[] m_tabs;
    [SerializeField] GameObject[] m_groups;

    int m_currentGroup = -1;

    [SerializeField] Color m_inactiveColour;
    [SerializeField] Color m_highlightedColour;
    [SerializeField] AudioSource m_rButtonMove;

    public UnityEvent OnBPressed;

    private void Start()
    {
        if (m_tabs != null && m_groups != null)
        {
            //Check for active tab
            for (int i = 0; i < m_tabs.Length; ++i)
            {
                if (m_groups[i].activeSelf)
                {
                    m_currentGroup = i;
                    m_tabs[m_currentGroup].color = m_highlightedColour;
                    m_groups[m_currentGroup].SetActive(true);
                }
            }

            if(m_currentGroup == -1)
            {
                m_currentGroup = 0;
                m_tabs[m_currentGroup].color = m_highlightedColour;
                m_groups[m_currentGroup].SetActive(true);
                return;
            }
            
            //deactivate any other active tabs
            for (int i = 0; i < m_tabs.Length; ++i)
            {
                if (m_groups[i].activeSelf && i != m_currentGroup)
                {
                    m_tabs[m_currentGroup].color = m_inactiveColour;
                    m_groups[m_currentGroup].SetActive(false);
                }
            }
        }
    }

    private void Update()
    {
        if (m_tabs == null || m_groups == null)
        {
            return;
        }

        if (Input.GetButtonDown("L1"))
        {
            MoveTabLeft(true);
        }
        else if (Input.GetButtonDown("R1"))
        {
            MoveTabLeft(false);
        }
        else if (Input.GetButtonDown("BButton"))
        {
            OnBPressed.Invoke(); //Back
        }
    }

    public void PlayClick()
    {
        if(m_rButtonMove.enabled)
        {
            m_rButtonMove.Play();
        }
    }

    public void MoveTabLeft(bool _moveLeft)
    {
        if (m_tabs == null && m_groups == null)
        {
            return;
        }

        if (_moveLeft)
        {
            if (m_currentGroup > 0)
            {
                //Hide current group
                m_tabs[m_currentGroup].color = m_inactiveColour;
                m_groups[m_currentGroup].SetActive(false);

                --m_currentGroup;

                //Activate previous group
                m_tabs[m_currentGroup].color = m_highlightedColour;
                m_groups[m_currentGroup].SetActive(true);
            }
        }
        else
        {
            if (m_currentGroup < m_tabs.Length - 1)
            {
                //Hide current group
                m_tabs[m_currentGroup].color = m_inactiveColour;
                m_groups[m_currentGroup].SetActive(false);

                ++m_currentGroup;

                //Activate previous group
                m_tabs[m_currentGroup].color = m_highlightedColour;
                m_groups[m_currentGroup].SetActive(true);
            }
        }
        m_rButtonMove.Play();
    }
}
