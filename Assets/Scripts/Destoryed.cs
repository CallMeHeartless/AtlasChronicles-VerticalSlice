using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destoryed : MonoBehaviour
{
    public bool BoxRespawns = true;

    [Header("Secondary Items")]
    public GameObject m_gSecondaryItemIs;
   
    public int m_intSecondaryItem = 2;
    public float m_intForceOut;
    public int m_intForceUp = 1;

    [Header("Hearts")]
    public GameObject m_gHeartItemIs;
    public int m_intHearts = 1;
    public int m_intChancePercent = 50;

    // [Header("Secondary Items")]
    //public GameObject RespawnChest;
    //public GameObject Self = (GameObject)Instantiate(chest);
    // Start is called before the first frame update
    
    void Update()
    {
        if (BoxRespawns == true)
        {


            gameObject.tag = "Broken";
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<Boxes>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            gameObject.GetComponent<RespawnBox>().enabled = true;

        }

        if (m_intSecondaryItem != 0)
        {


            int intRotationAroundCircle = 360 / m_intSecondaryItem;

            Vector3 fouce = new Vector3(m_intForceOut, m_intForceUp, 0);
            Vector3 gap = new Vector3(0.5f, 0, 0);


            for (int i = 0; i < m_intSecondaryItem; i++)
            {
                GameObject gOrb = GameObject.Instantiate(m_gSecondaryItemIs, gameObject.transform.position + gap + new Vector3(0, 1, 0), Quaternion.identity);
                gOrb.GetComponent<Rigidbody>().AddForce(fouce.x, fouce.y, fouce.z);
                fouce = Quaternion.AngleAxis(intRotationAroundCircle, Vector3.up) * fouce;
                gap = Quaternion.AngleAxis(intRotationAroundCircle, Vector3.up) * gap;
                gOrb.GetComponent<Pickup>().m_rHome = gameObject;

            }
            //m_intSecondaryItem = 0;
        }
        gameObject.GetComponent<Destoryed>().enabled = false;
    }

 
}
