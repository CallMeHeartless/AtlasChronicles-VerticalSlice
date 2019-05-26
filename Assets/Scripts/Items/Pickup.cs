using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private bool m_bIsCollected = false;
    public GameObject m_rParticles;
    public GameObject m_rHome;
    public void Start()
    {
        if (m_rParticles != null)
        {
            m_rParticles.SetActive(false);
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
                m_rHome.GetComponent<Destoryed>().m_intSecondaryItem--;
            }
            if (gameObject.CompareTag("PrimaryPickUp"))
            {    // Maps
                GetComponent<AudioSource>().Play();
                GameStats.MapsBoard[GameStats.LevelLoctation]++;
            }
            if (gameObject.CompareTag("SecondayPickUp"))
            {   // Level specific collectables
                GetComponent<AudioSource>().Play();
                GameStats.NoteBoard[GameStats.LevelLoctation]++;
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
