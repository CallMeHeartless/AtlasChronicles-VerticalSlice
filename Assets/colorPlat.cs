using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorPlat : MonoBehaviour
{
    public enum Colors
    {
        blue,green,pink,red,yellow
    }
    public enum match
    {
        door,lift,Switch
    }
    public string PassName;
    public Colors CurrentColor;
    public Colors setColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (setColor == CurrentColor)
        {

        } 
    }

    void changeColor()
    {
        if (Colors.yellow== CurrentColor)
        {
            CurrentColor = 0;
        }
        else
        {

            CurrentColor += 1;
        }
    }
}
