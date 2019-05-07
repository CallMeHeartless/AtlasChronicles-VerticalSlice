using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPostion : MonoBehaviour
{
    public GameObject m_gSetLoctation;
    
    // Update is called once per frame
    void Update()
    {
        transform.position = m_gSetLoctation.transform.position;
    }
}
