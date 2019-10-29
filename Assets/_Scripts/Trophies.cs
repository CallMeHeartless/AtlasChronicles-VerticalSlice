using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trophies : MonoBehaviour
{
     public Sprite[] m_STrophie;
     private int m_CurrentTrophie;
    // Start is called before the first frame update
    void Start()
    {
        m_CurrentTrophie = 3;
        gameObject.GetComponent<Image>().sprite = m_STrophie[m_CurrentTrophie];
    }
    /*___________________________________________________
    * Job: change the trophie to a lower version
    * Ceratior: Nicholas
    ______________________________________________________*/
    
    public void  DecreaseTrophie()
    {
        Debug.Log(m_CurrentTrophie);
        if (m_CurrentTrophie!=0)
        {
            m_CurrentTrophie--;
            Records.m_CurrentPlace = m_CurrentTrophie;
            gameObject.GetComponent<Image>().sprite = m_STrophie[m_CurrentTrophie];
        }
        else
        {
            gameObject.SetActive(false);
            Debug.Log("trophie is as low as it can go");
        }
      
    }

    //save trophie
}
