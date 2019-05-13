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
        if (gameObject.GetComponent<RespawnController>() == null)
        {
            gameObject.AddComponent<RespawnController>();
            //GetComponent<RespawnController>().m_rRespawnPoint= StartPoint;
        }
       
    }
}
