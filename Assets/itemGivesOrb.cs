using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemGivesOrb : MonoBehaviour
{
    public int CurrentLevelItemCount =0;
    public int LevelItem =7;
    public GameObject m_gSecondaryItemIs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void itemLevel()
    {
        CurrentLevelItemCount++;
        if (CurrentLevelItemCount == LevelItem)
        {
           GameObject.Instantiate(m_gSecondaryItemIs, gameObject.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            
        }
    }
}
