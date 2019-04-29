using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killbox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit");
        if (other.CompareTag("Player"))
        {
            Debug.Log("yes");
            other.GetComponent<respawn>().playerRespawns();
        }
        else
        {
            Debug.Log("no");
        }
    }
}
