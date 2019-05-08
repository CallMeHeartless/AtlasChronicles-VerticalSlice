using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGivesOrb : MonoBehaviour
{
    public int m_intCurrentLevelItemCount =0;
    public int m_intLevelItem = 7;
    public GameObject m_gSecondaryItemIs;
   

    public void itemLevel()
    {
        m_intCurrentLevelItemCount++;
        if (m_intCurrentLevelItemCount == m_intLevelItem)
        {
           GameObject.Instantiate(m_gSecondaryItemIs, gameObject.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            
        }
    }
}
