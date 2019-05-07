using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool[] doorLocks;
    public GameObject doorOpen;
    public GameObject doorClosed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bool dumu = true;
        for (int i = 0; i < doorLocks.Length; i++)
        {
            if (doorLocks[i] == false)
            {
                dumu = false;
            }
        }
        if (dumu == true)
        {
            //doorOpen
        }
        else
        {
            // doorClosed
        }
    }

    void SwitchChanged(int Location, bool correct){
        switch (Location)
        {
            case 0:
                doorLocks[Location] = correct;
                break;
            case 1:
                doorLocks[Location] = correct;
                break;
            case 2:
                doorLocks[Location] = correct;
                break;
            case 3:
                doorLocks[Location] = correct;
                break;
            default:
                break;
        }
    }
}
