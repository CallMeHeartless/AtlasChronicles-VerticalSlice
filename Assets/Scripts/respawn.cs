using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn : MonoBehaviour
{
    public GameObject reset;
    public GameObject player;
    bool goBack = false;
    // Start is called before the first frame update
    void Start()
    {

        //reset = GameObject.FindGameObjectWithTag("spawns");
        player = GameObject.FindGameObjectWithTag("Player");   
    }

    // Update is called once per frame
    void Update()
    {
        if (goBack)
        {
            Debug.Log("fun");

           player.transform.position = reset.transform.position;
            goBack = false;
        }
    }
    public void playerRespawns()
    {
        goBack = true;
    }
}
