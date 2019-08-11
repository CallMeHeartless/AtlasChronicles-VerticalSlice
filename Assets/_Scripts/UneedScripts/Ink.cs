using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ink : MonoBehaviour
{
    public float m_fInkAmount;
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
        GameObject.FindGameObjectWithTag("UI").gameObject.transform.GetChild(0).GetComponent<InkPounch>().AddedingInk(m_fInkAmount);
        Destroy(gameObject);
    }
}
