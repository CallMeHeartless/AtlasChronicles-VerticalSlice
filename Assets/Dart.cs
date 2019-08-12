using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour
{
    public int m_Damage;
    public float m_Speed;
    private Vector3 m_vec3Direction;
   
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, m_vec3Direction, m_Speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //damage player
            DamageMessage message = new DamageMessage();
            message.damage = m_Damage;
            message.source = gameObject;
            other.GetComponent<DamageController>().ApplyDamage(message);

            Destroy(gameObject);
        }
        else
        {
            if (other.CompareTag("Wall"))
            {
                Destroy(gameObject);
            }
        }
    }
    public void SetDirection(Vector3 _direction){
        m_vec3Direction = _direction;
    }
}
