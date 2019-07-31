using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tagbraking : MonoBehaviour
{
    private int m_TagLoctation;
    private PlayerController m_fPlayerController;
    private void Start()
    {
        m_fPlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

    }
    private void OnBecameVisible()
    {
        cicle(25);
    }
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            //something is on the tag so deactivate it
            m_fPlayerController.SetTeleportCondiction(m_TagLoctation);
            gameObject.SetActive(false);
        }
    }
    public void SetTag(int Tag){
        m_TagLoctation = Tag;
    }
    void cicle(int _radus)
    {
       
        LineRenderer NewCricle = gameObject.GetComponent<LineRenderer>();
        var points = new Vector3[361];

        for (int i = 0; i < 361; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / 360);
            points[i] = new Vector3(Mathf.Sin(rad) * _radus, 0, Mathf.Cos(rad) * _radus) +gameObject.transform.position;
        }
        NewCricle.positionCount = points.Length;
        NewCricle.SetPositions(points);
        Debug.Log(points.Length);
    }
}
