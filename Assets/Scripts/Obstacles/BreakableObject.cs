using System.Collections;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject m_rOriginalObject;
    [SerializeField] GameObject m_rBrokenObject;
    [SerializeField] GameObject[] m_rPrizes;

    [Header("Arc settings")]
    [SerializeField] AnimationCurve yCurve;
    [SerializeField] float m_speedOfArc = 2.0f;
    [SerializeField] float m_fRadius = 2.0f;

    Vector3[] m_rPrizeDestinations;
    float m_fCollectableHeight = 0.0f;

    private void Start()
    {
        //If there is a broken object
        if(m_rBrokenObject)
            m_rBrokenObject.SetActive(false);

        //Initialise a prize destination array based on the amount of prizes that exist
        m_rPrizeDestinations = new Vector3[m_rPrizes.Length];
        for (int i = 0; i < m_rPrizes.Length; ++i)
        {
            m_rPrizeDestinations[i] = m_rPrizes[i].transform.position;
        }
    }

    public void SwitchToBroken()
    {
        if(m_rOriginalObject && m_rBrokenObject)
        {
            //Activate the broken box
            m_rBrokenObject.SetActive(true);
            //Destroy the original box
            Destroy(m_rOriginalObject);
            //Instantiate a prize at the boxes position
            if(m_rPrizes.Length != 0)
                SpawnPrizesInCircle();
            //Make broken box disappear after 3 seconds
            Invoke("Disappear", 3.0f);
        }
    }

    private void SpawnPrizesInCircle()
    {
        //Calculate positions to spawn based on the number of prizes that exist
        for (int i = 0; i < m_rPrizes.Length; ++i)
        {
            //If the type of collectable is a pechapple, spawn them with a lower height (the prefab has a height value that is not set to zero)
            //Otherwise, spawn with a height of 1.0f
            if(m_rPrizes[i].name.Contains("Collectable"))
                m_fCollectableHeight = 0.0f;
            else
                m_fCollectableHeight = 1.0f;

            // Instantiate each prize in the chests position
            m_rPrizes[i] = Instantiate(m_rPrizes[i], transform.localPosition, Quaternion.identity);

            //Calculate the destinations of the prizes (to be spread out around the chest)
            float angle = 0.0f;
            Vector3 newPos = Vector3.zero;

            //If there is more than 1 prize, calculate a different position around a radius around the chest.
            //Else, spawn in the chests location.
            if (m_rPrizes.Length > 1)
            {
                angle = i * Mathf.PI * 2.0f / m_rPrizes.Length;
                newPos = new Vector3(Mathf.Cos(angle) * m_fRadius, m_fCollectableHeight, Mathf.Sin(angle) * m_fRadius);
            }

            //Disable colliders for prizes so they do not get collected immediately when they are spawned
            m_rPrizes[i].GetComponent<Collider>().enabled = false;

            //Set the newly calculated destinations into an array
            m_rPrizeDestinations[i] = transform.localPosition + newPos;
        }

        //Move the spawned prizes in an arc towards the calculated positions to simulate spreading
        StartCoroutine(Spread());
    }

    IEnumerator Spread()
    {
        float timeElapsed = 0.0f;
        Vector3 newVal = Vector3.zero;

        //If the curve lerp hasnt ended
        while (timeElapsed < yCurve[yCurve.length-1].time)
        {
            //Increase time based on the speed specified
            timeElapsed += Time.deltaTime * m_speedOfArc;

            //Lerp each prize towards their destinations (calculated previously)
            //Move the y value based on the curve defined (in an arc)
            for (int i = 0; i < m_rPrizes.Length; ++i)
            {
                m_rPrizes[i].transform.position = Vector3.Lerp(m_rPrizes[i].transform.position, m_rPrizeDestinations[i], timeElapsed);

                newVal = m_rPrizes[i].transform.position;
                newVal.y += yCurve.Evaluate(timeElapsed); //Set Y as the current position of on the curve

                m_rPrizes[i].transform.position = newVal;
            }
            yield return null;
        }

        //Enable colliders for prizes when they have arrived 
        //      at their destination so they can be collected
        for (int i = 0; i < m_rPrizes.Length; ++i)
        {
            m_rPrizes[i].GetComponent<Collider>().enabled = true;
        }

        yield return null;
    }
    
    void Disappear()
    {
        //Note: Didnt destroy this current object holder as the prizes do not complete 
        //their arc if the speed happens to be slower than when this Destroy method is Invoked
        Destroy(m_rBrokenObject);
    }
}
