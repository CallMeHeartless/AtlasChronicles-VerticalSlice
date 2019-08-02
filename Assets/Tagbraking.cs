using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tagbraking : MonoBehaviour
{
    private int m_TagLoctation;
    private PlayerController m_fPlayerController;
    [SerializeField] private GameObject m_fInnerRing;
    [SerializeField] private GameObject m_fOuterRing;
    private void Start()
    {
        m_fPlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

    }
    private void OnBecameVisible()
    {
        Debug.Log("help");
        cicle(25,true);
        cicle(35, false);
       
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
    public void SetColor(Color _Iner, Color _Outer)
    {

        m_fInnerRing.GetComponent<LineRenderer>().material.color = _Iner;

        m_fOuterRing.GetComponent<LineRenderer>().material.color = _Outer;

    }
    public void OutOfRange(bool _InRange)
    {

        m_fOuterRing.gameObject.SetActive(_InRange);

    }
    public void OutOfRangeInner(bool _InRange)
    {

        m_fInnerRing.gameObject.SetActive(_InRange);

    }
    void cicle(int _InRadus,bool InRadus)
    {
        var points = new Vector3[361];
       
        if (InRadus)
        {
            for (int i = 0; i < 361; i++)
            {
                var rad = Mathf.Deg2Rad * (i * 360f / 360);
                points[i] = new Vector3(Mathf.Sin(rad) * _InRadus, 0, Mathf.Cos(rad) * _InRadus) + gameObject.transform.position;
            }
            Debug.Log(points.Length);
            m_fInnerRing.GetComponent<LineRenderer>().positionCount = points.Length;
            m_fInnerRing.GetComponent<LineRenderer>().SetPositions(points);
        }
        else
        {
          
            for (int i = 0; i < 361; i++)
            {
                var rad = Mathf.Deg2Rad * (i * 360f / 360);
                points[i] = new Vector3(Mathf.Sin(rad) * _InRadus, 0, Mathf.Cos(rad) * _InRadus) + gameObject.transform.position;
            }
            Debug.Log(points.Length);
            m_fOuterRing.GetComponent<LineRenderer>().positionCount = points.Length;
            m_fOuterRing.GetComponent<LineRenderer>().SetPositions(points);
        }


      
       
    }
}
