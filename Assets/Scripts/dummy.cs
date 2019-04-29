using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummy : MonoBehaviour
{
    public GameObject StartPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<respawn>() == null)
        {
            gameObject.AddComponent<respawn>();
            GetComponent<respawn>().reset= StartPoint;
        }
       
    }
}
