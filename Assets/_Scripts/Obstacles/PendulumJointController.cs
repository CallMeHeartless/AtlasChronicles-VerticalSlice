using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
public class PendulumJointController : MonoBehaviour
{
    // Internal variables
    private HingeJoint m_rJoint;
    private float m_fTargetSpeed;
    private float m_fForce;

    // Start is called before the first frame update
    void Start(){
        m_rJoint = GetComponent<HingeJoint>();
        m_fTargetSpeed = m_rJoint.motor.targetVelocity;
        m_fForce = m_rJoint.motor.force;
    }
    
    // Update is called once per frame
    void Update(){
        // Change direction at edge of constraints
        if(m_rJoint.angle <= m_rJoint.limits.min || m_rJoint.angle >= m_rJoint.limits.max) {
            ToggleDirection();
        }
    }

    // Instructs the pendulum to change which direction it is swinging
    private void ToggleDirection() {
        // A new motor is required to set the target velocity property
        JointMotor motor = new JointMotor();
        motor.force = m_fForce; // Ensure the force is preserved
        if (m_rJoint.motor.targetVelocity < 0.0f ) { // Moving backwards
            motor.targetVelocity = m_fTargetSpeed;
            m_rJoint.motor = motor;
        } else if (m_rJoint.motor.targetVelocity > 0.0f) { // Moving forwards
            motor.targetVelocity = -m_fTargetSpeed;
            m_rJoint.motor = motor;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if(!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Tag")) {
            ToggleDirection();
        }
    }
}
