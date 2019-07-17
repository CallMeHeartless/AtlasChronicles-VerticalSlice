using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private bool m_bIsCollected = false;
    public GameObject m_rParticles;
    public GameObject m_rHome;
    private GameObject m_PickupPic, m_MapPic;

    public void Start()
    {
        if (m_rParticles != null)
        {
            m_rParticles.SetActive(false);
        }

        m_PickupPic = GameObject.FindGameObjectWithTag("PickupPicUI");
        m_MapPic = GameObject.FindGameObjectWithTag("MapPicUI");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !m_bIsCollected)
        {
            // Flag as collected
            m_bIsCollected = true;

            if (m_rHome != null)
            {
                m_rHome.GetComponent<Destoryed>().m_intSecondaryItem--;
            }
            if (gameObject.CompareTag("PrimaryPickup"))
            {    // Maps
                GetComponent<AudioSource>().Play();
                GameStats.s_iMapsBoard[GameStats.s_iLevelIndex]++;
                // Check for end of game
                GameEndController.CheckMapCollection();

                if (m_MapPic)
                {
                    m_MapPic.GetComponent<Animator>().SetTrigger("Animate");
                }
            }
            if (gameObject.CompareTag("SecondaryPickup"))
            {   // Level specific collectables
                GetComponent<AudioSource>().Play();
                GameStats.s_iCollectableBoard[GameStats.s_iLevelIndex]++;
                GameEndController.CheckMapCollection();

                if (m_PickupPic)
                {
                    m_PickupPic.GetComponent<Animator>().SetTrigger("Animate");
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
