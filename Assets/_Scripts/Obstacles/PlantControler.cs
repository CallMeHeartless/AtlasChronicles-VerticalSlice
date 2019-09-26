using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Animator))]
public class PlantControler : MonoBehaviour
{
    //public bool BieRange;
    public bool m_bLookAtRange;
    public Transform m_Player;
    public Quaternion m_currentPostion;
    public Vector3 m_v3XtraMovement;

    // Start is called before the first frame update
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bLookAtRange)
        {

            //transform.LookAt(m_Player,Vector3.up);


            //looking around

            //set roation to look at m_Player with adding the offsets
            var look = transform.position - m_Player.position;

            var rotation = Quaternion.LookRotation(look);
            rotation = rotation * Quaternion.AngleAxis(m_v3XtraMovement.x, Vector3.left);
            rotation = rotation * Quaternion.AngleAxis(m_v3XtraMovement.y, Vector3.up);
            rotation = rotation * Quaternion.AngleAxis(m_v3XtraMovement.z, Vector3.forward);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 3);



            // rotation
            //transform.rotation = Quaternion.Slerp(WorldToLocal transform.rotation, rotation, Time.deltaTime*3);
            //Debug.Log(look);
            //rotation.t
            //m_currentPostion = transform.localRotation;


            //

            //transform.LookAt(m_Player);
            //transform.eulerAngles += m_v3XtraMovement;



            Debug.Log(transform.localEulerAngles.y);
            //to far left
            if (transform.localRotation.y < 90)
            {
                Debug.Log("dummy");
                //transform.eulerAngles= new Vector3(transform.localRotation.x, 180, transform.localRotation.z);
                // transform.Rotate(Vector3.up, Time.deltaTime * 10, Space.World);
            }

            //to far right
            if (transform.localRotation.y > -90)
            {
                Debug.Log("ymmud");
                //transform.eulerAngles = new Vector3(transform.localRotation.x, -180, transform.localRotation.z);
                // transform.Rotate(Vector3.down, Time.deltaTime * 10, Space.World);
            }


        }
        else
        {

        }
    }
    //inrange to attack
    private void OnTriggerStay(Collider other)
    {
     transform.parent.transform.parent.GetComponent<Animator>().SetTrigger("Attack");
    }
    //inrange to notiche the m_Player
    public void Lookat(bool _Range)
    {
       //m_Player in range for plant to find m_Player
        if (_Range)
        {
            m_bLookAtRange = true;
           
        }
        else
        {
            m_bLookAtRange = false;
          
        }
    }
}
