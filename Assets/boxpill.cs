using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxpill : MonoBehaviour
{
    public int maxBoxes =2;
    public int currentBoxes =0;
   
    List<GameObject> Boxes = new List<GameObject>();
    public GameObject box;
    private bool itemOn = false;
    // Start is called before the first frame update
    void Start()
    {
        //itemOn = 0;
        //GameObject.Instantiate(box, transform.position + new Vector3(0, 1, 0), transform.rotation); 
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (itemOn == false)
    //    {
    //        if (currentBoxes <= maxBoxes)
    //        {
    //            currentBoxes++;
    //            GameObject boxi = GameObject.Instantiate(box, transform.position + new Vector3(0, 1.1f, 0), transform.rotation);
    //            Boxes.Add(boxi);
    //        }
            
    //    }
    //    //itemOn = false;
    //    //Debug.Log("hit");
    //}
    private void OnTriggerStay(Collider other)
    {
        itemOn = true;
    }
    public void spawnBox()
    {
        if (currentBoxes <= maxBoxes)
        {
           
        }
        else
        {
           GameObject dummy =  Boxes[0];
            Boxes.Remove(dummy);
            Destroy(dummy);
        }

        currentBoxes++;
        GameObject boxi = GameObject.Instantiate(box, transform.position + new Vector3(0, 1.1f, 0), transform.rotation);
        Boxes.Add(boxi);
    }
   

}
