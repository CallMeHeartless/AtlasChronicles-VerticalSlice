using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupMap : Pickup
{
    protected override void Start()
    {
        base.Start();
        m_eType = PickupType.EMap;
        m_rPickupPic = GameObject.FindGameObjectWithTag("MapPicUI");
        if(m_iZoneID == 0)
        {
            print("ERROR: UNASSIGNED MAP ZONE : " + name);
        }
    }
    
    /// <summary>
    /// Executes map collection which has two modes: stolen and cinematic
    /// </summary>
    /// <author>Vivian</author>
    protected override void Collect()
    {
        //Stolen maps will not activate a cinematic and will be collected like normal collectables
        // with different sounds, particles and affects a different UI element.
        if(m_bStolen)
        {
            m_rAudio.Play();
            //Animate 2D Map pic UI // Stretch
            if (m_rPickupPic)
            {
                m_rPickupPic.GetComponent<Animator>().SetTrigger("Animate");
            }
        }
        else
        {
            Destroy(gameObject, 0.1f);
            //Maps that are not stolen are basically zone specific maps that should activate a cinematic when triggered with
            CinematicManager.ActivateCinematicByID(0);
        }

        //Increase map count
        GameStats.s_iMapsBoard[GameStats.s_iLevelIndex]++;

        // Update map zone state
        Zone.CollectMapFragment(m_iZoneID);
        // Check for end of game
        GameEndController.CheckMapCollection();
    }

}
