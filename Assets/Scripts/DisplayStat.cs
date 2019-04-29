using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStat : MonoBehaviour
{
    bool showStats = false;
    public GameObject maps;
    public GameObject note;
    public GameObject[] Hearts;
    public int HP;
    // Start is called before the first frame update
    void Start()
    {
        newHealth(HP);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            showStat();
        }

        //show all stats
        showStats = true;
        maps.SetActive(true);
        note.SetActive(true);
        maps.GetComponent<Text>().text = GameStats.MapsBoard[GameStats.LevelLoctation].ToString();
        note.GetComponent<Text>().text = GameStats.NoteBoard[GameStats.LevelLoctation].ToString();
    }
  
    void newHealth(int HP)
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

    void showStat()
    {
        if (!showStats)
        {
            showStats = true;
            maps.SetActive(true);
            note.SetActive(true);
            maps.GetComponent<Text>().text = GameStats.MapsBoard[GameStats.LevelLoctation].ToString();
            note.GetComponent<Text>().text = GameStats.NoteBoard[GameStats.LevelLoctation].ToString();
        }
        else
        {
            showStats = false;
            maps.SetActive(false);
            note.SetActive(false);
        }

    }
}
