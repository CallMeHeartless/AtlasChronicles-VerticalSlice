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
        TranlastedMovement.x= Mathf.Round(TranlastedMovement.x*1000) / 2000;
        TranlastedMovement.y = Mathf.Round(TranlastedMovement.y * 1000) / 2000;
        TranlastedMovement.z = Mathf.Round(TranlastedMovement.z * 1000)/2000;
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    other.transform.parent = gameObject.transform;
    //}
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("contuie");
        other.GetComponent<PlayerController>().movement += TranlastedMovement;
    }
}
