using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [SerializeField] GameObject m_rBrokenObject;

    public void SwitchToBroken()
    {
        if(m_rBrokenObject)
        {
            Instantiate(m_rBrokenObject, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
