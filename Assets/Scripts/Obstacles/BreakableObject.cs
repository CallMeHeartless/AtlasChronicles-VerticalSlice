using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [SerializeField] GameObject m_rOriginalObject;
    [SerializeField] GameObject m_rBrokenObject;
    [SerializeField] GameObject m_rPrize;
    [SerializeField] GameObject[] m_rPrizes;
    [SerializeField] float m_fRadius = 2.0f;
    float m_fCollectableHeight = 0.0f;
    private void Start()
    {
        if(m_rBrokenObject)
            m_rBrokenObject.SetActive(false);
    }

    public void SwitchToBroken()
    {
        if(m_rBrokenObject && m_rBrokenObject)
        {
            //Activate the broken box
            m_rBrokenObject.SetActive(true);
            //Destroy the original box
            Destroy(m_rOriginalObject);
            //Instantiate a prize at the boxes position
            SpawnPrizesInCircle();
            //if(m_rPrize)
            //    Instantiate(m_rPrize, transform.position, transform.rotation);
            //Make broken box disappear after 3 seconds
            Invoke("Disappear", 3.0f);
        }
    }

    private void SpawnPrizesInCircle()
    {
        //Calculate positions to spawn based on the number of prizes that exist
        //m_fRadius

        //Spawn prizes
        for (int i = 0; i < m_rPrizes.Length; ++i)
        {
            if(m_rPrizes[i].name.Contains("Collectable"))
                m_fCollectableHeight = 0.0f;
            else
                m_fCollectableHeight = 1.0f;

            float angle = 0.0f;
            Vector3 newPos = Vector3.zero;

            if (m_rPrizes.Length > 1)
            {
                angle = i * Mathf.PI * 2.0f / m_rPrizes.Length;
                newPos = new Vector3(Mathf.Cos(angle) * m_fRadius, m_fCollectableHeight, Mathf.Sin(angle) * m_fRadius);
            }
           
            GameObject go = Instantiate(m_rPrizes[i], transform.localPosition+newPos, Quaternion.identity);

            //Instantiate(m_rPrizes[i], transform.position, transform.rotation);
        }
    }

    void Disappear()
    {
        Destroy(gameObject);
    }
}
