using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Npc : MonoBehaviour
{
    public Text uiText;
    public string[] texts;
    public bool talking =false;
    private int currentLine;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            uiText.enabled = true;
            talking = true;
            uiText.text = texts[0];
            currentLine = 1;
        }
        if (talking == true)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (currentLine == texts.Length)
                {
                    uiText.enabled = false;
                    talking = false;
                }
                else
                {
                   
                    uiText.text = texts[currentLine];
                    currentLine++;
                }
      
            }
        }
        
    }
}
