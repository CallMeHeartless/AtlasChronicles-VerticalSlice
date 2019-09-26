using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantLookAt : MonoBehaviour
{
    private PlantControler m_Plant;
    // Start is called before the first frame update
    void Start()
    {
        m_Plant = transform.parent.GetComponent<PlantControler>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_Plant.Lookat(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_Plant.Lookat(false);
        }
    }
}
