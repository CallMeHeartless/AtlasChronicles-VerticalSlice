using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxPop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        
            gameObject.transform.parent.gameObject.GetComponent<BoxCollider>().enabled = true;
            Destroy(this);
        
    }
}
