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
    public float LeftNumber;
    public float RightNumber;
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
            
            Vector3 targetDir = m_Player.position - transform.position;
            float angle;

            bool right; if (Vector3.Angle(transform.parent.parent.forward, targetDir) > 90f) right = false; else right = true;


            //Debug.Log(right);
            if (right)
            {
                angle = Vector3.Angle(transform.parent.parent.right, targetDir);
            }
            else
            {
                angle = -Vector3.Angle(transform.parent.parent.right, targetDir) ;
            }

            //Debug.Log(angle);
            // //angle = angle + 90;

            // //if ((angle < (transform.rotation.z - angle))&& (angle < 2))
            // //{
            // //    Debug.Log("over");
            // //}

            // //if ((angle > (transform.rotation.z - angle)) && (angle > 2))
            // //{
            // //    Debug.Log("under");
            // //}
            // //transform.rotation = transform.LookAt();

            //// float offPut = (transform.rotation.z - angle) - 180;

            // if ((angle >= 180)&& (angle <= -180))
            // {
            //     Debug.Log("in");
            // }
            // else
            // {
            // }


            var look = transform.position - m_Player.position;

            var rotationAction = Quaternion.LookRotation(look);
           // Debug.Log(rotationAction.z);
            float possableRotationPoint=look.z;

            // bool right; if (Vector3.Angle(transform.right, look) > 90f) right = false; else right = true;

            //if (right)
            //{
            //    if (look.z<0)//8.6
            //    {
            //        possableRotationPoint = possableRotationPoint - 360;
            //    }
            //    else
            //    {
            //        possableRotationPoint = possableRotationPoint + 360;
            //    }
            //}
            bool lok = false;

            if ((LeftNumber<0)&& (RightNumber > 0))
            {
                if ((angle >= LeftNumber) && (angle <= RightNumber))
                {
                    //Debug.Log("inrange");
                    lok = true;
                }
                }
            else
            {
                if ((angle >= LeftNumber) || (angle <= RightNumber))
                {
                    //Debug.Log("outrange");
                    lok = true;
                }
                }

            if (lok == true)
            {


                rotationAction = rotationAction * Quaternion.AngleAxis(m_v3XtraMovement.x, Vector3.left);
                rotationAction = rotationAction * Quaternion.AngleAxis(m_v3XtraMovement.y, Vector3.up);
                rotationAction = rotationAction * Quaternion.AngleAxis(m_v3XtraMovement.z, Vector3.forward);

                transform.rotation = Quaternion.Slerp(transform.rotation, rotationAction, Time.deltaTime * 3);
            }
            // rotation
            //transform.rotation = Quaternion.Slerp(WorldToLocal transform.rotation, rotation, Time.deltaTime*3);
            //Debug.Log(look);
            //rotation.t
            //m_currentPostion = transform.localRotation;


            //

            //transform.LookAt(m_Player);
            //transform.eulerAngles += m_v3XtraMovement;



            // Debug.Log(transform.rotation.eulerAngles.y);
            //to far left
            // Vector3 ForwardLoking = Vector3.

            //if (transform.rotation.eulerAngles.y > 90)
            //{
            //    Debug.Log("m_timeString");
            //    //transform.eulerAngles= new Vector3(transform.localRotation.x, 180, transform.localRotation.z);
            //    // transform.Rotate(Vector3.up, Time.deltaTime * 10, Space.World);
            //}

            ////to far right
            //if (transform.rotation.eulerAngles.y < -90)
            //{
            //    Debug.Log("ymmud");
            //    //transform.eulerAngles = new Vector3(transform.localRotation.x, -180, transform.localRotation.z);
            //    // transform.Rotate(Vector3.down, Time.deltaTime * 10, Space.World);
            //}


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
