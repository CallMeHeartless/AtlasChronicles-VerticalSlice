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

    // Update is called once per frame

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
    }
}
