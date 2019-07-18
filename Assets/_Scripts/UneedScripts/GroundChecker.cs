using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public float StopTimer = 0.5f;
    public bool Stopped = false;
    public float GrowTimer = 0.7f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Stopped == false)
        {


            if (StopTimer <= 0)
            {
                gameObject.GetComponent<Rigidbody>().useGravity = false;
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                Stopped = true;
            }
            else
            {
                StopTimer -= Time.deltaTime;
            }
        }
        else
        {
            if (GrowTimer <= 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
                Destroy(this);
            }
            else
            {
                transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
                GrowTimer -= Time.deltaTime;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }
}
