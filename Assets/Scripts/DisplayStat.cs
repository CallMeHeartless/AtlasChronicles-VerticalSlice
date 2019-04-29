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
        NewHealth(HP);
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
        collectableText.GetComponent<TextMeshProUGUI>().text = GameStats.NoteBoard[GameStats.LevelLoctation].ToString() + "/100";
        mapCountText.GetComponent<TextMeshProUGUI>().text = GameStats.MapsBoard[GameStats.LevelLoctation].ToString() + "/3";
    }
  
    void NewHealth(int HP)
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
            collectableText.GetComponent<TextMeshProUGUI>().text = GameStats.NoteBoard[GameStats.LevelLoctation].ToString();
            mapCountText.GetComponent<TextMeshProUGUI>().text = GameStats.MapsBoard[GameStats.LevelLoctation].ToString();
        }
        else
        {
            showStats = false;
            collectableText.SetActive(false);
            mapCountText.SetActive(false);
        }

    }
}
