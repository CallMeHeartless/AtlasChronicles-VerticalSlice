using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPop : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        
            gameObject.transform.parent.gameObject.GetComponent<BoxCollider>().enabled = true;
            Destroy(this);
        
    }
}
