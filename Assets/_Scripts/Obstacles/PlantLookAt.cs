using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantLookAt : MonoBehaviour
{
    private PlantControler Plant;
    // Start is called before the first frame update
    void Start()
    {
        Plant = transform.parent.GetComponent<PlantControler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Plant.Lookat(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Plant.Lookat(false);
        }
    }
}
