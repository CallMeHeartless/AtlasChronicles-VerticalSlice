using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private bool m_bIsCollected = false;
    public GameObject m_rParticles;
    public GameObject m_rHome;
    private GameObject m_rPickupPic, m_rMapPic;
    private RectTransform m_rCanvasRect;
    private Camera m_cam;
    float m_fCollectionSpeed = 0.4f;

    public void Start()
    {
        if (m_rParticles != null)
        {
            m_rParticles.SetActive(false);
        }

        m_rPickupPic = GameObject.FindGameObjectWithTag("PickupPicUI");
        m_rMapPic = GameObject.FindGameObjectWithTag("MapPicUI");
        m_cam = GameObject.Find("Camera").GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        if(m_bIsCollected)
        {
            Vector3 screenPoint;
            if (gameObject.CompareTag("PrimaryPickup"))
            {
                screenPoint = m_rMapPic.transform.position + new Vector3(0, -10.0f, 5.0f);
            }
            else
            {
                screenPoint = m_rPickupPic.transform.position + new Vector3(0, 10.0f, 5.0f);
            }
            Vector3 worldPos = m_cam.ScreenToWorldPoint(screenPoint);
            transform.position = Vector3.MoveTowards(transform.position, worldPos, m_fCollectionSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !m_bIsCollected)
        {
            // Flag as collected
            m_bIsCollected = true;

            if (m_rHome != null)
            {
                m_rHome.GetComponent<Destroyed>().m_intSecondaryItem--;
            }
            if (gameObject.CompareTag("PrimaryPickup"))
            {    // Maps
                GetComponent<AudioSource>().Play();
                GameStats.s_iMapsBoard[GameStats.s_iLevelIndex]++;
                // Check for end of game
                GameEndController.CheckMapCollection();

                if (m_rMapPic)
                {
                    m_rMapPic.GetComponent<Animator>().SetTrigger("Animate");
                }
            }
            if (gameObject.CompareTag("SecondaryPickup"))
            {   // Level specific collectables
                GetComponent<AudioSource>().Play();
                GameStats.s_iCollectableBoard[GameStats.s_iLevelIndex]++;
                GameEndController.CheckMapCollection();

                if (m_rPickupPic)
                {
                    m_rPickupPic.GetComponent<Animator>().SetTrigger("Animate");
                }

            }

            // Turn on VFX
            if (m_rParticles)
            {
                m_rParticles.SetActive(true);
            }
            GetComponentInChildren<Animator>().SetTrigger("Collect");
        }
    }

}
