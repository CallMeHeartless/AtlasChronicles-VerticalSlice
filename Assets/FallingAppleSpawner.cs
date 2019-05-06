using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingAppleSpawner : MonoBehaviour
{
    private float currentTimer;
    public GameObject DroppingItem;
    public float timerMax;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTimer <= 0)
        {
            GameObject.Instantiate(DroppingItem, gameObject.transform.position, gameObject.transform.rotation);
            currentTimer= timerMax;
        }
        else
        {
            currentTimer -= Time.deltaTime;
        }
    }
}
