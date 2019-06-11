using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayStat : MonoBehaviour
{
    bool showStats = false;
    public GameObject collectableText;
    public GameObject mapCountText;
    public GameObject[] Hearts;
    public int HP;
    // Start is called before the first frame update
    void Start()
    {
        //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().h;
        NewHealth(4);


        GameStats.s_iCollectableTotal[GameStats.s_iLevelIndex] = GameObject.FindGameObjectsWithTag("SecondaryPickup").Length;
        GameStats.s_iMapsTotal[GameStats.s_iLevelIndex] = GameObject.FindGameObjectsWithTag("PrimaryPickup").Length;

        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
        for(int i = 0; i < boxes.Length; ++ i)
        {
            GameObject[] prizes = boxes[i].GetComponent<BreakableObject>().GetPrizes();
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
        collectableText.SetActive(true);
        mapCountText.SetActive(true);
        collectableText.GetComponent<TextMeshProUGUI>().text = GameStats.s_iCollectableBoard[GameStats.s_iLevelIndex].ToString() + "/" + GameStats.s_iCollectableTotal[GameStats.s_iLevelIndex];
        mapCountText.GetComponent<TextMeshProUGUI>().text = GameStats.s_iMapsBoard[GameStats.s_iLevelIndex].ToString() + "/" + GameStats.s_iMapsTotal[GameStats.s_iLevelIndex];
    }
  
    public void NewHealth(int HP)
    {
        
        for (int i = 0; i < Hearts.Length; i++)
        {
            if (HP<= i)
            {
                Hearts[i].SetActive(false);
            }
            else
            {
                Hearts[i].SetActive(true);
            }
        }

    }

    void ShowStat()
    {
        if (!showStats)
        {
            showStats = true;
            collectableText.SetActive(true);
            mapCountText.SetActive(true);
            collectableText.GetComponent<TextMeshProUGUI>().text = GameStats.s_iCollectableBoard[GameStats.s_iLevelIndex].ToString();
            mapCountText.GetComponent<TextMeshProUGUI>().text = GameStats.s_iMapsBoard[GameStats.s_iLevelIndex].ToString();
        }
        else
        {
            showStats = false;
            collectableText.SetActive(false);
            mapCountText.SetActive(false);
        }

    }
}
