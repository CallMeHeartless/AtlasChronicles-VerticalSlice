using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class TutorialCollider : MonoBehaviour
{
    [SerializeField] bool m_bHideSelf = false;
    [SerializeField] int m_iTimesToDisplay = 1;
    private int m_iTimesDisplayed = 0;

    public UnityEvent OnTrigStay;
    [SerializeField] float timeUntilHide = 2.0f;
    public UnityEvent OnTrigExit;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            OnTrigStay.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke("HideTutorial", timeUntilHide);
        }
    }

    public void HideTutorial()
    {
        OnTrigExit.Invoke();
        if(m_iTimesDisplayed < m_iTimesToDisplay)
        {
            ++m_iTimesDisplayed;
        }
        else if (m_iTimesDisplayed >= m_iTimesToDisplay)
        {
            //If part of a group
            if (!m_bHideSelf)
            {
                gameObject.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                //If this is an individual tutorial that is not part of a group
                gameObject.SetActive(false);
            }
        }
    }
}
