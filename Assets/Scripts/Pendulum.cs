using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    Rigidbody m_ridBody;
    public Vector3 m_vec3StartingVeloicty;
    public float m_fForward;
    public float m_fBack;
    public float m_fSpeed =10;
    public JointMotor m_JointCaneHingeMotor;
    public int m_fDamage = 1;
   // public bool X, Y, Z = false;
    // Start is called before the first frame update
    void Start()
    {
        m_ridBody = GetComponent<Rigidbody>();
        m_ridBody.angularVelocity = m_vec3StartingVeloicty;
        
    }

    // Update is called once per frame
    void Update()
    {
       


            if (((transform.rotation.x > 0) && (transform.rotation.x < m_fForward))||((transform.rotation.z > 0) && (transform.rotation.z < m_fForward)) && (m_ridBody.angularVelocity.y > 0) && (m_ridBody.angularVelocity.y < m_vec3StartingVeloicty.y))
            {
               
                m_ridBody.angularVelocity = m_vec3StartingVeloicty;
            }
            if (((transform.rotation.x > m_fForward)||((transform.rotation.z > m_fForward))) && (m_ridBody.angularVelocity.y < m_vec3StartingVeloicty.y))
            {
            //Debug.Log("here1");
            m_JointCaneHingeMotor.targetVelocity = -m_vec3StartingVeloicty.y;
                m_JointCaneHingeMotor.force = m_fSpeed;
                GetComponent<HingeJoint>().motor = m_JointCaneHingeMotor;
            }
            if (((transform.rotation.x < 0) && (transform.rotation.x > m_fBack))||((transform.rotation.z < 0) && (transform.rotation.z > m_fBack)) && (m_ridBody.angularVelocity.y < 0) && (m_ridBody.angularVelocity.y > -m_vec3StartingVeloicty.y))
            {
                
                m_ridBody.angularVelocity = -1 * m_vec3StartingVeloicty;
            }
            if (((transform.rotation.x < m_fBack)||(transform.rotation.z < m_fBack)) && (m_ridBody.angularVelocity.y > -m_vec3StartingVeloicty.y))
            {
            //Debug.Log("here2");
            m_JointCaneHingeMotor.targetVelocity = m_vec3StartingVeloicty.y;
                m_JointCaneHingeMotor.force = m_fSpeed;
                GetComponent<HingeJoint>().motor = m_JointCaneHingeMotor;
            }
           
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DamageMessage message = new DamageMessage();
            message.damage = m_fDamage;
            message.source = gameObject;
            collision.gameObject.GetComponent<DamageController>().ApplyDamage(message);
            //collision.gameObject.GetComponent<Rigidbody>().AddForce((gameObject.transform.position - collision.gameObject.transform.position)*100, ForceMode.Impulse);
        }
    }
}
