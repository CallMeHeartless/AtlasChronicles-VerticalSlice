using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [SerializeField] GameObject m_rOriginalObject;
    [SerializeField] GameObject m_rBrokenObject;
    [SerializeField] GameObject m_rPrize;

    private void Start()
    {
        if(m_rBrokenObject)
            m_rBrokenObject.SetActive(false);
    }

    public void SwitchToBroken()
    {
        if(m_rBrokenObject && m_rBrokenObject)
        {
            //Activate the broken box
            m_rBrokenObject.SetActive(true);
            //Destroy the original box
            Destroy(m_rOriginalObject);
            //Instantiate a prize at the boxes position
            if(m_rPrize)
                Instantiate(m_rPrize, transform.position, transform.rotation);
            //Make broken box disappear after 3 seconds
            Invoke("Disappear", 3.0f);
        }
    }

    void Disappear()
    {
        Destroy(gameObject);
    }
}
