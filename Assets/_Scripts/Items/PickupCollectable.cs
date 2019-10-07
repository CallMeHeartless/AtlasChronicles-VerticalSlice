﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCollectable : Pickup
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        m_eType = PickupType.ECollectable;
        m_rPickupPic = GameObject.FindGameObjectWithTag("PickupPicUI");
    }

    /// <summary>
    /// Executes Pickup collection
    /// </summary>
    /// <author>Vivian</author>
    protected override void Collect()
    {
        m_rAudio.Play();
        GameStats.s_iCollectableBoard[GameStats.s_iLevelIndex]++;
        GameEndController.CheckMapCollection(); // Review later

        // Disable the magnetic component
        MagneticController magnetic = GetComponent<MagneticController>();
        if (magnetic) {
            magnetic.m_bIsMagnetised = false;
        }
       
        if (GameState.GetGameplayMode() == GameState.GameplayMode.ForTheMaps)
        {
            TimerUpdate.AddTime(5);
        }
        
    }
}
