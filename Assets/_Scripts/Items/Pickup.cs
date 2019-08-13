using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{
    EMap, ECollectable
}

public class Pickup : MonoBehaviour
{
    [SerializeField] protected int m_regionID = 0;
    [SerializeField] GameObject m_rParticles;

    protected bool m_bIsCollected = false;
    protected bool m_bStolen = false;

    protected GameObject m_rPickupPic;
    protected DisplayStat m_rDisplayStats;
    protected AudioSource m_rAudio;
    protected PickupType m_eType = PickupType.ECollectable;

    protected virtual void Start()
    {
        if (m_rParticles != null)
        {
            m_rParticles.SetActive(false);
        }

        m_rDisplayStats = GameObject.FindGameObjectWithTag("UI").GetComponent<DisplayStat>();
        m_rAudio = GetComponent<AudioSource>();
    }

    protected virtual void Collect() {}
    public PickupType GetPickupType()
    {
        return m_eType;
    }

    /******************************************************************
     * OnTriggerEnter: Activates collection if collided with player
     * Author: Vivian
     ******************************************************************/
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !m_bIsCollected)
        {
            m_bIsCollected = true;

            //Shows UI Game Panel immediately when the pickup has been collected
            m_rDisplayStats.HideUIGamePanel(false);

            //Call virtual function for children to execute their own unique ways of collection
            Collect();

            //Hide UI Game Panel a few seconds after the pickup has been collected
            m_rDisplayStats.HideUIGamePanel(true);

            //Do not excute the rest of the function if the map has not been stolen
            if (m_eType == PickupType.EMap && !m_bStolen)
                return;

            // Turn on VFX for the given pickup
            if (m_rParticles)
            {
                m_rParticles.SetActive(true);
            }

            //Activate pickup animation
            GetComponentInChildren<Animator>().SetTrigger("Collect");
        }
    }


    /******************************************************************
     * GetIsStolen: Activates collection if collided with player
     * Author: Vivian
     ******************************************************************/
    public bool GetIsStolen()
    {
        return m_bStolen;
    }

    public void SetStolen(bool _stolen)
    {
        m_bStolen = _stolen;
    }
}