using System.Collections;
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

        // Update the ink gauge
        InkGauge rInkGauge = InkGauge.GetInstance();
        if (rInkGauge)
        {
            rInkGauge.IncrementGaugeLimit();
        }
        if (gameObject.CompareTag("secondaryPickup"))
        {
            if (GameState.GetGameplayMode() == GameState.GameplayMode.ForTheMaps)
            {
                TimerUpdate.AddTime(5);
            }
        }
    }
}
