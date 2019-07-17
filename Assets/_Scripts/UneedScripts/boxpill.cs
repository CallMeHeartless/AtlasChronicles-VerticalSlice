using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxpill : MonoBehaviour
{
    public int m_iMaxBoxes =2;
    public int m_iCurrentBoxes =0;
   
    List<GameObject> m_GLBoxes = new List<GameObject>();
    public GameObject m_GBox;
    private bool m_bItemOn = false;
    
    //something is blocking it
    private void OnTriggerStay(Collider other)
    {
        m_bItemOn = true;
    }

    //spawning a box
    public void spawnBox()
    {
        if (m_iCurrentBoxes <= m_iMaxBoxes)
        {
           
        }
        else
        {
           //remove the first game object if their is to many
           GameObject dummy = m_GLBoxes[0];
            m_GLBoxes.Remove(dummy);
            Destroy(dummy);
        }
        //spawn box
        m_iCurrentBoxes++;
        GameObject boxi = GameObject.Instantiate(m_GBox, transform.position + new Vector3(0, 1.1f, 0), transform.rotation);
        m_GLBoxes.Add(boxi);
    }
   

}
