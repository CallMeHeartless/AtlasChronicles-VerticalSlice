using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    public float m_fTimer;
    public float m_fMaxTimer= 2;
    public GameObject m_gApple;
    [Tooltip("Only use this is using dart")] public Vector3 Direction;
    // Start is called before the first frame update
    void Start()
    {
        if (m_fMaxTimer <= 0)
        {
            m_fMaxTimer = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_fTimer <= 0)
        {
           
            GameObject item = Instantiate(m_gApple, transform.position, Quaternion.identity);
           // Debug.Log(item.GetComponentInChildren<LogsOnRiver>());
            if (item.GetComponentInChildren<LogsOnRiver>())
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    item.GetComponentInChildren<LogsOnRiver>().m_rPoints[i] = transform.GetChild(i).gameObject;
                }
               
            }
            if (item.GetComponent<Dart>())
            {
               
               item.GetComponentInChildren<Dart>().SetDirection(Direction);
              

            }
            m_fTimer = m_fMaxTimer;
        }
        else
        {
            m_fTimer -= Time.deltaTime;
        }
    }
}


