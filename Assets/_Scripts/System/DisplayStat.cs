using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayStat : MonoBehaviour
{
    bool showStats = false;
    [SerializeField] GameObject m_rCollectableText;
    [SerializeField] public GameObject m_rMapCountText;
    [SerializeField] public GameObject[] m_rHearts;
    private GameObject[] m_rMapReferences;
    public int m_iHP = 4;
    // Start is called before the first frame update
    void Start()
    {
        NewHealth(m_iHP); // NIK //Set the player to have 4 health

        //Find all collectables placed in the level
        GameStats.s_iCollectableTotal[GameStats.s_iLevelIndex] = GameObject.FindGameObjectsWithTag("SecondaryPickup").Length;

        //Find all the references for the map fragments in the level
        m_rMapReferences = GameObject.FindGameObjectsWithTag("PrimaryPickup");

        //Assign the amount of map fragments in the level to GameStats
        GameStats.s_iMapsTotal[GameStats.s_iLevelIndex] = m_rMapReferences.Length;
        
        //Find all treasure chests in the level and count thhe number of secondary pickups that are contained in them
        GameObject[] chests = GameObject.FindGameObjectsWithTag("Box");
        for(int i = 0; i < chests.Length; ++ i)
        {
            GameObject[] prizes = chests[i].GetComponent<BreakableObject>().GetPrizes();
            for (int j = 0; j < prizes.Length; ++j)
            {
                if(prizes[j].CompareTag("SecondaryPickup"))
                {
                    ++GameStats.s_iCollectableTotal[GameStats.s_iLevelIndex];
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ShowStat();
        }

        //show all stats
        showStats = true;
        m_rCollectableText.SetActive(true);
        m_rMapCountText.SetActive(true);
        m_rCollectableText.GetComponent<TextMeshProUGUI>().text = GameStats.s_iCollectableBoard[GameStats.s_iLevelIndex].ToString() + "/" + GameStats.s_iCollectableTotal[GameStats.s_iLevelIndex];
        m_rMapCountText.GetComponent<TextMeshProUGUI>().text = GameStats.s_iMapsBoard[GameStats.s_iLevelIndex].ToString() + "/" + GameStats.s_iMapsTotal[GameStats.s_iLevelIndex];
    }
  
    public void NewHealth(int HP)
    {
        
        for (int i = 0; i < m_rHearts.Length; i++)
        {
            if (HP<= i)
            {
                m_rHearts[i].SetActive(false);
            }
            else
            {
                m_rHearts[i].SetActive(true);
            }
        }

    }

    void ShowStat()
    {
        if (!showStats)
        {
            showStats = true;
            m_rCollectableText.SetActive(true);
            m_rMapCountText.SetActive(true);
            m_rCollectableText.GetComponent<TextMeshProUGUI>().text = GameStats.s_iCollectableBoard[GameStats.s_iLevelIndex].ToString();
            m_rMapCountText.GetComponent<TextMeshProUGUI>().text = GameStats.s_iMapsBoard[GameStats.s_iLevelIndex].ToString();
        }
        else
        {
            showStats = false;
            m_rCollectableText.SetActive(false);
            m_rMapCountText.SetActive(false);
        }

    }
}
