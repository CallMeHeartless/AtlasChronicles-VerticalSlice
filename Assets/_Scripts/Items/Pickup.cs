using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{
    EMap, ECollectable
}

public class Pickup : MonoBehaviour
{
    [SerializeField] protected int m_iZoneID = 0;
    [SerializeField] GameObject m_rParticles;

    protected bool m_bIsCollected = false;
    protected bool m_bStolen = false;

    protected GameObject m_rPickupPic;
    protected DisplayStat m_rDisplayStats;
    protected AudioSource m_rAudio;
    protected PickupType m_eType = PickupType.ECollectable;
    private Zone m_rParent;

    public static float s_fCurrentCollectionTime = 1.0f;
    public static float s_fMinimumCollectionTime = 1.0f;
    public static float s_fMaximumCollectionTime = 10.0f;

    public static float s_fCurrentPitch = 0.0f;
    public static bool s_bCurrentlyCollecting = false;
    private float m_fInitPitch = 1.0f;
    private float m_fMaxPitch = 3.0f;
    private float m_fIncreasePitch = 0.2f;

    protected virtual void Start()
    {
        if (m_rParticles != null)
        {
            m_rParticles.SetActive(false);
        }

        m_rDisplayStats = GameObject.FindGameObjectWithTag("UI").GetComponent<DisplayStat>();
        m_rAudio = GetComponent<AudioSource>();

        m_rParent = transform.root.GetComponent<Zone>();
        if (m_rParent) {
            m_iZoneID = m_rParent.GetZoneID();
            m_rParent.AddToZone(gameObject);
        }

        s_fCurrentPitch = m_fInitPitch;
    }

    private void Update()
    {
        //NOTE: MUST FIND WAY TO UPDATE FRM DIF CLASS THAT DOES NOT HAVE MULTIPLE INSTANCES OF THE PICKUP
        //if (!s_bCurrentlyCollecting)
        //    return;

        //s_fCurrentCollectionTime += Time.deltaTime;

        //if (s_fCurrentCollectionTime > s_fMaximumCollectionTime)
        //{
        //    print("resetted: " + s_fCurrentCollectionTime + "    " + s_fMaximumCollectionTime);

        //    s_fCurrentCollectionTime = 0.0f;
        //    s_fCurrentPitch = m_fInitPitch;
        //    s_bCurrentlyCollecting = false;
        //}
    }

    /// <summary>
    /// Virtual collect function.
    /// </summary>
    /// <author>Vivian</author>
    protected virtual void Collect() {}

    /// <summary>
    /// Gets which type the current pickup object is
    /// </summary>
    /// <returns>PickupType : EMap, ECollectable</returns>
    /// <author>Vivian</author>
    public PickupType GetPickupType()
    {
        return m_eType;
    }

    /// <summary>
    /// Activates collection if collided with player
    /// </summary>
    /// <author>Vivian</author>
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

            if(m_eType == PickupType.ECollectable)
            {
                s_fCurrentCollectionTime = 0.0f;
                PlayCollectableSound();
            }

            //Do not excute the rest of the function if the map has not been stolen
            if (m_eType == PickupType.EMap && !m_bStolen)
                return;

            // Turn on VFX for the given pickup
            if (m_rParticles)
            {
                m_rParticles.SetActive(true);
            }
            //print("Pickup Zone: " + m_iZoneID);
            //Activate pickup animation
            GetComponentInChildren<Animator>().SetTrigger("Collect");
        }
    }
    
    /// <summary>
    /// Checks if Pickup has been Goon
    /// </summary>
    /// <author>Vivian</author>
    public bool GetIsStolen()
    {
        return m_bStolen;
    }

    /// <summary>
    /// Setter to set if pickup has been stolen by a Goon
    /// </summary>
    /// <param name="_stolen">Has pickup been stolen by Goon</param>
    /// <author>Vivian</author>
    public void SetStolen(bool _stolen)
    {
        m_bStolen = _stolen;
    }

    /// <summary>
    /// Checks if map has been collected
    /// </summary>
    /// <returns>true if collected</returns>
    /// <author>Vivian</author>
    public bool GetCollected()
    {
        return m_bIsCollected;
    }

    public void PlayCollectableSound()
    {
        s_bCurrentlyCollecting = true;

        //IF collection time exceeds max time for nxt crystal to be collected, reset time
        if (s_fCurrentPitch < m_fMaxPitch)
        {
            s_fCurrentPitch += m_fIncreasePitch;
        }

        m_rAudio.pitch = s_fCurrentPitch;
        m_rAudio.Play();
    }
}