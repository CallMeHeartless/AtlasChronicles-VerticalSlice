using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Water : MonoBehaviour
{
    BoxCollider Box;
    [Header("delaying")]
    public float m_fDroppingLevel = 0.009f;
    private bool m_bQuicksandOn = false;
    private float m_fPauseDuration = 0;
    public float m_fCurrentPause = 2;
    public Vector3 m_vec3ResetWaterLevel;
    public float m_fMaxdDropLevel = 1.7f;
    public bool m_bWaterTouchHurts = true;
    public bool m_bFirstTouch = true;
    private float m_fInitialColliderHeight = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Box = gameObject.GetComponent<BoxCollider>();
        m_fInitialColliderHeight = Box.center.y;
        m_vec3ResetWaterLevel.y += m_fInitialColliderHeight;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bQuicksandOn)
        {
            if (m_fPauseDuration <= 0)
            {
                Box.center = m_vec3ResetWaterLevel;
                m_bFirstTouch = true;
                m_bQuicksandOn = false;
            }
            else
            {
                m_fPauseDuration -= Time.deltaTime;
            }
        }
    }

    private void OnCollisionStay(Collision other)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {


        //Debug.Log("hit");

        // Debug.Log(other.tag);
        // if (other.CompareTag("Player"))
        // {
        // Debug.Log("m_timeString");

        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().ToggleWading(true);

            Box.center -= new Vector3(0, m_fDroppingLevel, 0);
            m_bQuicksandOn = true;
            m_fPauseDuration = m_fCurrentPause;
            if (Box.center.y <= m_fMaxdDropLevel)
            {
                //Debug.Log("Death");
                DamageMessage message = new DamageMessage();
                message.damage = 4;
                message.source = gameObject;
                other.GetComponent<DamageController>().ApplyDamage(message);
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (m_bWaterTouchHurts)
            {
                if (m_bFirstTouch)
                {
                    //Debug.Log("huret");
                    DamageMessage message = new DamageMessage();
                    message.damage = 1;
                    message.source = gameObject;
                    other.GetComponent<DamageController>().ApplyDamage(message);
                    m_bFirstTouch = false;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().ToggleWading(false);
        }
    }
}
   
