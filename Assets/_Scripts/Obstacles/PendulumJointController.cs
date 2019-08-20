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
    [SerializeField]
    private bool m_bDamages = true;
    private bool m_bDamageReady = true;

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

        // Reset the ability for the pendulum to damage
        if(m_bDamages && !m_bDamageReady) {
            m_bDamageReady = true;
        }
    }

    // Handle collision events
    private void OnCollisionEnter(Collision collision) {
        // The pendulum should damage the player
        if (collision.gameObject.CompareTag("Player") && m_bDamages && m_bDamageReady) {
            // Create damage message
            DamageMessage damage = new DamageMessage();
            damage.damage = 1;
            damage.source = gameObject;
            //damage.direction

            // Apply damage
            collision.gameObject.GetComponent<DamageController>().ApplyDamage(damage);
            // Prevent consequtive damage
            m_bDamageReady = false;

        }

        // Handle collision with an object preventing the pendulum from reaching the end of its arc - force it to change direction
        else if(!collision.gameObject.CompareTag("Tag")) {
            ToggleDirection();
        }
    }
}
