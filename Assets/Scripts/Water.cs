using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    BoxCollider Box;
    [Header("delaying")]
    public float m_fDroppingLevel = 0.009f;
    private bool m_bQuicksandOn = false;
    private float m_fPauseDuration = 0;
    public float m_fCurrentPause = 2;
    public Vector3 m_vec3ResetWaterLevel;
    public float MaxdDropLevel = 1.7f;
    
    // Start is called before the first frame update
    void Start()
    {
        Box = gameObject.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bQuicksandOn)
        {


            if (m_fPauseDuration <= 0)
            {


                Box.center = m_vec3ResetWaterLevel;

                m_bQuicksandOn = false;
            }
            else
            {
                m_fPauseDuration -= Time.deltaTime;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        
  
        //Debug.Log("hit");

       // Debug.Log(other.tag);
       // if (other.CompareTag("Player"))
       // {
           // Debug.Log("dummy");
            Box.center -= new Vector3(0, m_fDroppingLevel, 0);
        m_bQuicksandOn = true;
        m_fPauseDuration = m_fCurrentPause;
        if (Box.center.y <= MaxdDropLevel)
        {
           // Debug.Log("Death");
            DamageMessage message = new DamageMessage();
            message.damage = 4;
            message.source = gameObject;
            other.GetComponent<DamageController>().ApplyDamage(message);
        }
       // }

    }
}
