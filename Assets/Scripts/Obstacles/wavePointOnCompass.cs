using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wavePointOnCompass : MonoBehaviour
{
    public GameObject marker;
    public Vector3 turn;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (marker.transform.position - transform.position), out hit))
        {
            Debug.DrawRay(transform.position, (marker.transform.position - transform.position), Color.green);
            turn = Vector3.Cross(Vector3.up, gameObject.transform.position-marker.transform.position);



            Vector3 screenPoint = cam.WorldToViewportPoint(marker.transform.position);
            //bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
            //Debug.Log(onScreen);
            if (screenPoint.z > 0)
            {


                if (screenPoint.x < 0)
                {
                    //set postion to the right
                    gameObject.GetComponent<RectTransform>().position = new Vector3(940, 984, 0);
                }
                else
                {
                    if (screenPoint.x > 1)
                    {

                        gameObject.GetComponent<RectTransform>().position = new Vector3(1113, 984, 0);
                        //set postion to the left
                    }
                    else
                    {
                        float number = 932 + (screenPoint.x * 178);
                        gameObject.GetComponent<RectTransform>().position = new Vector3(number, 984, 0);
                    }
                }
            }
            else
            {
                if (screenPoint.x < 0.5)
                {
                    //set postion to the right

                    gameObject.GetComponent<RectTransform>().position = new Vector3(1113, 984, 0);
                }
                else
                {
                    gameObject.GetComponent<RectTransform>().position = new Vector3(940, 984, 0);
                }

            }
            //Debug.Log(screenPoint.x );
        }
    }
}
