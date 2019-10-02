using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyT1 : MonoBehaviour
{
    enum EnemyBefavior
    {
        wonder, hunting,chasing
    }

    public GameObject endpoint;
    private GameObject player;
    private bool m_reached = true;
    private EnemyBefavior m_Action = EnemyBefavior.wonder;
    private bool PlayerIsClose;
    public float time;
    // Start is called before the first frame update
    void Start()
    {
       // player = GameObject.FindGameObjectWithTag("Player");
        gameObject.GetComponent<NavMeshAgent>().destination = endpoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_Action)
        {
            case EnemyBefavior.wonder:
                wondering();
                break;
            case EnemyBefavior.hunting:
                break;
            case EnemyBefavior.chasing:
                chase();
                break;
            default:
                break;
        }
    }

    void wondering()
    {
        if (Vector3.Distance(gameObject.transform.position, endpoint.transform.position) < 3)
        {
            if (m_reached)
            {
                Debug.Log("m_timeString");
                endpoint.GetComponent<newPostion>().StartCoroutine("Fade");
                m_reached = false;
            }
        }
        else
        {
            m_reached = true;
        }


        gameObject.GetComponent<NavMeshAgent>().destination = endpoint.transform.position;
    }
    void chase()
    {
       
        if (!PlayerIsClose)
        {
            if (time <= 0)
            {
                m_Action = EnemyBefavior.wonder;
            }
            else
            {
                time -= Time.deltaTime;
            }
        }
        gameObject.GetComponent<NavMeshAgent>().destination = player.transform.position;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            PlayerIsClose = true;
            player = other.gameObject;
            m_Action = EnemyBefavior.chasing;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerIsClose = false;
            time = 2;
           
        }
    }
   
}
