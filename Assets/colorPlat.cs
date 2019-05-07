using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorPlat : MonoBehaviour
{
    public Material[] MaterialColor = new Material[5]; 
    public GameObject effectingObject;
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
    public match type;
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<MeshRenderer>().material = MaterialColor[(sbyte)CurrentColor];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            changeColor();
        }
    }
    // Update is called once per frame
    void CorrectColor()
    {
        if (setColor == CurrentColor)
        {
            switch (type)
            {
                case match.door:
                    //passName  TRUE
                    break;
                case match.lift:
                    //passName  TRUE
                    break;
                case match.Switch:
                    //passName  TRUE
                    break;
                default:
                    break;
            }

        }
        else
        {
            switch (type)
            {
                case match.door:
                    //passName FALSE
                    break;
                case match.lift:
                    //passName FALSE
                    break;
                case match.Switch:
                    //passName FALSE
                    break;
                default:
                    break;
            }

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
        GetComponentInChildren<MeshRenderer>().material = MaterialColor[(sbyte)CurrentColor];
        CorrectColor();
    }
}
