using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPostion : MonoBehaviour
{
    public GameObject setLoctation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = setLoctation.transform.position;
    }
}
