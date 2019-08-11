using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    // External references

    [SerializeField] private bool m_bGoingX, m_bGoingY, m_bGoingZ;

    [Header("External References")]
    public Vector3 m_vec3StartingVeloicty;
    public float m_fForward;
    public float m_fBack;
    public float m_fSpeed =10;
    public JointMotor m_JointCaneHingeMotor;
    public Vector3 m_vec3Axies;
    public int m_Damage = 1;

    Rigidbody m_ridBody;
    
    // Start is called before the first frame update
    void Start()
    {
        m_ridBody = GetComponent<Rigidbody>();
        m_ridBody.angularVelocity = m_vec3StartingVeloicty;

        GetComponent<HingeJoint>().axis = m_vec3Axies;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bGoingX)
        {
            XMoving();
        }
        if (m_bGoingZ)
        {
            ZMoving();
        }
        if (m_bGoingY)
        {
            YMoving();
        }
    }
    void XMoving()
    {
        //swing cube forward
        if ((m_ridBody.angularVelocity.y < m_vec3StartingVeloicty.y))
        {
            if ((transform.rotation.x > m_fForward))
            {
                m_JointCaneHingeMotor.targetVelocity = -m_vec3StartingVeloicty.y;
                m_JointCaneHingeMotor.force = m_fSpeed;
                GetComponent<HingeJoint>().motor = m_JointCaneHingeMotor;
            }
            //switching
            else if (m_ridBody.angularVelocity.y > 0)
            {
                if ((transform.rotation.x < 0) && (transform.rotation.x > m_fBack))
                {
                    m_ridBody.angularVelocity = -m_vec3StartingVeloicty;
                }
            }
        }
        //swing cube backward
        if (m_ridBody.angularVelocity.y > -m_vec3StartingVeloicty.y)
        {
            if (transform.rotation.x < m_fBack)
            {
                m_JointCaneHingeMotor.targetVelocity = m_vec3StartingVeloicty.y;
                m_JointCaneHingeMotor.force = m_fSpeed;
                GetComponent<HingeJoint>().motor = m_JointCaneHingeMotor;
            }
            //switching
            else if (m_ridBody.angularVelocity.y < 0)
            {
                if ((transform.rotation.x > 0) && (transform.rotation.x < m_fForward))
                {
                    m_ridBody.angularVelocity = m_vec3StartingVeloicty;
                }
            }
        }
    }
    void YMoving()
        {
        //swing cube forward
            if ( (m_ridBody.angularVelocity.x < m_vec3StartingVeloicty.x))
            {
                if ((transform.rotation.y > m_fForward))
                {
                    m_JointCaneHingeMotor.targetVelocity = -m_vec3StartingVeloicty.x;
                    m_JointCaneHingeMotor.force = m_fSpeed;
                    GetComponent<HingeJoint>().motor = m_JointCaneHingeMotor;
                }
            //switching
            else if (m_ridBody.angularVelocity.x > 0)
                {
                    if ((transform.rotation.y < 0) && (transform.rotation.y > m_fBack))
                    {
                        m_ridBody.angularVelocity = -m_vec3StartingVeloicty;
                        }
                    }

            }
        //swing cube backward
        if (m_ridBody.angularVelocity.x > -m_vec3StartingVeloicty.x)
            {
                if (transform.rotation.y < m_fBack)
                {
                    m_JointCaneHingeMotor.targetVelocity = m_vec3StartingVeloicty.x;
                    m_JointCaneHingeMotor.force = m_fSpeed;
                    GetComponent<HingeJoint>().motor = m_JointCaneHingeMotor;
                }
            }
        //switching
        else if (m_ridBody.angularVelocity.x < 0)
            {

                if ((transform.rotation.y > 0) && (transform.rotation.y < m_fForward))
                {
                    m_ridBody.angularVelocity = m_vec3StartingVeloicty;
                }
            }
        }
    void ZMoving()
    {
        //swing cube forward
        if ((m_ridBody.angularVelocity.y < m_vec3StartingVeloicty.y))
        {
            if ((transform.rotation.z > m_fForward))
            {
                m_JointCaneHingeMotor.targetVelocity = -m_vec3StartingVeloicty.y;
                m_JointCaneHingeMotor.force = m_fSpeed;
                GetComponent<HingeJoint>().motor = m_JointCaneHingeMotor;
            }
            //switching
            else if (m_ridBody.angularVelocity.y > 0)
            {
                if ((transform.rotation.z < 0) && (transform.rotation.z > m_fBack))
                {
                    m_ridBody.angularVelocity = -m_vec3StartingVeloicty;
                }
            }
        }
        //swing cube backward
        if (m_ridBody.angularVelocity.y > -m_vec3StartingVeloicty.y)
        {
            if (transform.rotation.z < m_fBack)
            {
                m_JointCaneHingeMotor.targetVelocity = m_vec3StartingVeloicty.y;
                m_JointCaneHingeMotor.force = m_fSpeed;
                GetComponent<HingeJoint>().motor = m_JointCaneHingeMotor;
            }
            //switching
            else if (m_ridBody.angularVelocity.y < 0)
            {
                if ((transform.rotation.z > 0) && (transform.rotation.z < m_fForward))
                {
                    m_ridBody.angularVelocity = m_vec3StartingVeloicty;
                }
            }
        }
    }

    //hit player
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DamageMessage message = new DamageMessage();
            message.damage = m_Damage;
            message.source = gameObject;
            collision.gameObject.GetComponent<DamageController>().ApplyDamage(message);
        }
    }
}
