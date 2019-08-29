using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTeleporter : MonoBehaviour
{
    private NPCController m_rNPC;
    private bool m_bTeleportActivated = false;
    private bool m_bNPCPresent = true;

    // Start is called before the first frame update
    void Start()
    {
        m_rNPC = transform.parent.gameObject.GetComponentInChildren<NPCController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !CheckNPCIsChild())
        {
            m_rNPC.TeleportToDestination(this.transform);
        }
    }

    bool CheckNPCIsChild()
    {
        return ((gameObject.GetComponentInChildren<NPCController>() != null) ? true : false);
    }
}
