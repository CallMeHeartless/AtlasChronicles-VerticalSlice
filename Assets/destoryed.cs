using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destoryed : MonoBehaviour
{
    public GameObject m_gSecondaryItemIs;
    public GameObject m_gHeartItemIs;
    public int m_intSecondaryItem = 2;
    public float m_intForceOut;
    public int m_intForceUp = 1;
    public int m_intHearts = 1;
    public int m_intChancePercent = 50;
    // Start is called before the first frame update
    void Start()
    {

       int intRotationAroundCircle = 360 / m_intSecondaryItem;

        Vector3 fouce = new Vector3(m_intForceOut, m_intForceUp,0);
        Vector3 gap = new Vector3(0.5f, 0, 0);


        for (int i = 0; i < m_intSecondaryItem; i++)
        {
            GameObject gOrb = GameObject.Instantiate(m_gSecondaryItemIs, gameObject.transform.position+ gap+new Vector3(0,1,0), Quaternion.identity);
            gOrb.GetComponent<Rigidbody>().AddForce(fouce.x, fouce.y, fouce.z);
            fouce = Quaternion.AngleAxis(intRotationAroundCircle, Vector3.up) * fouce;
            gap = Quaternion.AngleAxis(intRotationAroundCircle, Vector3.up) * gap;

        }

        //if (m_intChancePercent != 0)
        //{
           
        //    if (Random.Range(0, 100) < m_intChancePercent)
        //    {
        //        GameObject gOrb = GameObject.Instantiate(m_gHeartItemIs, gameObject.transform.position, Quaternion.identity);
        //        gOrb.GetComponent<Rigidbody>().AddForce(0, fouce.y, 0);
        //    }
        //}
        Destroy(gameObject);
    }

 
}
