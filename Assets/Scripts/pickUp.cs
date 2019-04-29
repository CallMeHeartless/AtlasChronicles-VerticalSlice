using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUp : MonoBehaviour
{
    private bool m_bIsCollected = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !m_bIsCollected){
            // Flag as collected
            m_bIsCollected = true;

            if (gameObject.CompareTag("PrimaryPickUp")){
                GameStats.MapsBoard[GameStats.LevelLoctation]++;
            }
            else{
                if (gameObject.CompareTag("SecondayPickUp")){
                    GameStats.NoteBoard[GameStats.LevelLoctation]++;
                    
                }
            }
            //gameObject.SetActive(false);
            GetComponentInChildren<Animator>().SetTrigger("Collect");
        }
    }

    public void DestroyPickup() {
        Destroy(gameObject);
    }


}
