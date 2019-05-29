using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChilder : MonoBehaviour
{
    public Vector3 TranlastedMovement = new Vector3(0,0,0);
    public Vector3 currentLoctation = new Vector3(0, 0, 0);
    public Vector3 NewLoctation = new Vector3(0, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()   
    {
        NewLoctation = currentLoctation;
        currentLoctation = transform.position;
        TranlastedMovement = currentLoctation - NewLoctation;
        //TranlastedMovement.x= TranlastedMovement.x;
        //TranlastedMovement.y = TranlastedMovement.y  ;
        //TranlastedMovement.z = TranlastedMovement.z ;
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    other.transform.parent = gameObject.transform;
    //}
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("contuie");
            other.GetComponent<PlayerController>().movement += TranlastedMovement;
        }
        
    }
}
