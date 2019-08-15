using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildActivator : MonoBehaviour
{
    [SerializeField] bool m_bActivateChildren = false;
    [SerializeField] GameObject[] m_rChildren;
    
    void Start()
    {
        //Activate or deactivate children on start
        foreach (GameObject item in m_rChildren)
        {
            item.SetActive(m_bActivateChildren);
        }
        
    }

    /******************************************************************
     * SetChildrenActive: Sets children active/inactive depending on parameter
     * Author: Vivian
     ******************************************************************/
    public void SetChildrenActive(bool _activate)
    {
        //Sets all children active
        foreach (GameObject item in m_rChildren)
        {
            item.SetActive(_activate);
        }
    }
}
