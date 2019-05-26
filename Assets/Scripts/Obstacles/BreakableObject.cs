using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [SerializeField] GameObject m_rOriginalObject;
    [SerializeField] GameObject m_rBrokenObject;

    private void Start()
    {
        if(m_rBrokenObject)
            m_rBrokenObject.SetActive(false);
    }

    public void SwitchToBroken()
    {
        if(m_rBrokenObject && m_rBrokenObject)
        {
            m_rBrokenObject.SetActive(true);
            Destroy(m_rOriginalObject);
            Invoke("Disappear", 3.0f);
        }
    }

    void Disappear()
    {
        Destroy(gameObject);
    }
}
