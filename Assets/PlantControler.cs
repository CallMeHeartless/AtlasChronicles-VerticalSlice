using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Animator))]
public class PlantControler : MonoBehaviour
{
    //public bool BieRange;
    public bool LookAtRange;
    public Transform Player;
    public Quaternion currentPostion;
    public Vector3 XtraMovement;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (LookAtRange)
        {

            //transform.LookAt(Player,Vector3.up);


            //looking around
            var look = transform.position - Player.position;

            var rotation = Quaternion.LookRotation(look);
            //rotation = rotation * Quaternion.AngleAxis(XtraMovement.x, Vector3.left);
            rotation = rotation * Quaternion.AngleAxis(XtraMovement.y, Vector3.up);
            rotation = rotation * Quaternion.AngleAxis(XtraMovement.z, Vector3.forward);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 3);



            // rotation
            //transform.rotation = Quaternion.Slerp(WorldToLocal transform.rotation, rotation, Time.deltaTime*3);
            // Debug.Log(look);
            //rotation.t
            //currentPostion = transform.localRotation;


            //

            //transform.LookAt(Player);
            //transform.eulerAngles += XtraMovement;
            Debug.Log(transform.localRotation.y*Mathf.Rad2Deg);
            if (transform.rotation.y > 90)
            {
                Debug.Log("dummy");
                transform.eulerAngles= new Vector3(transform.localRotation.x, 180, transform.localRotation.z);
                // transform.Rotate(Vector3.up, Time.deltaTime * 10, Space.World);
            }

            if (transform.rotation.y < -90)
            {
                Debug.Log("ymmud");
                transform.eulerAngles = new Vector3(transform.localRotation.x, -180, transform.localRotation.z);
                // transform.Rotate(Vector3.down, Time.deltaTime * 10, Space.World);
            }


        }
    }
    private void OnTriggerEnter(Collider other)
    {
       // GetComponent<Animator>().SetTrigger("Attack");
    }
    public void Lookat(bool _Range)
    {
        //_Range ? LookAtRange = true : LookAtRange = true;
        if (_Range)
        {
            LookAtRange = true;
           
        }
        else
        {
            LookAtRange = false;
          
        }
    }
}
