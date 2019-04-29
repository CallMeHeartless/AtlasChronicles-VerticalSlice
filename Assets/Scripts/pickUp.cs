using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUp : MonoBehaviour
{
    private bool m_bIsCollected = false;
    public GameObject m_particles;

    public void Start()
    {
        if(m_particles != null)
        {
            StopGOParticles(m_particles);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !m_bIsCollected){
            // Flag as collected
            m_bIsCollected = true;

            if (gameObject.CompareTag("PrimaryPickUp")){    //Collectables
                GameStats.MapsBoard[GameStats.LevelLoctation]++;
                PlayGOParticles(m_particles);
            }
            if (gameObject.CompareTag("SecondayPickUp")){   //Maps
                GameStats.NoteBoard[GameStats.LevelLoctation]++;
            }
            //gameObject.SetActive(false);
            GetComponentInChildren<Animator>().SetTrigger("Collect");
        }
    }

    public void DestroyPickup() {
        Destroy(gameObject);
    }

    private void PlayGOParticles(GameObject _particles)
    {
        //Plays all particle system components within a gameobject
        foreach(ParticleSystem g in _particles.GetComponentsInChildren<ParticleSystem>())
        {
            g.Play();
        }
    }

    private void StopGOParticles(GameObject _particles)
    {
        //Stops playing all particle system components within a gameobject
        foreach (ParticleSystem g in _particles.GetComponentsInChildren<ParticleSystem>())
        {
            g.Stop();
        }
    }
}
