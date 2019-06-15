using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class TutorialCollider : MonoBehaviour
{
    [SerializeField] bool m_hideSelf = false;
    [SerializeField] bool m_showOnce = false;
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
        if(m_showOnce)
        {
            //If part of a group
            if(!m_hideSelf)
            {
                gameObject.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                //If individual
                gameObject.SetActive(false);
            }
        }
    }
}
