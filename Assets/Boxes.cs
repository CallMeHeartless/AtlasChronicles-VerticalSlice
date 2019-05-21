using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxes : MonoBehaviour
{
    public enum BoxType
    {
        Standend,Slam,InkSlam,Teleport,Mark,InkMark,Enemy,Fall,Water,Weapon

    }
    public BoxType BoxKind = BoxType.Standend;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (BoxKind)
        {
            case BoxType.Standend:
                if (other.CompareTag(""))
                {
                    GetComponent<Destoryed>().enabled = true;
                    GetComponent<Boxes>().enabled = false;
                }
                break;
            case BoxType.Slam:
                if (other.CompareTag("Player"))
                {
                   // if (other.GetComponent<PlayerController>().m_bGroundPound)
                   // {
                        GetComponent<Destoryed>().enabled = true;
                        GetComponent<Boxes>().enabled = false;
                   // }
                   
                }
                break;
            case BoxType.InkSlam:
                if (other.CompareTag("Player"))//check ink
                {
                    //if (other.GetComponent<PlayerController>().m_bGroundPound)
                    //{
                        if (true)
                        {
                            GetComponent<Destoryed>().enabled = true;
                            GetComponent<Boxes>().enabled = false;
                        }
                       
                    //}
                }
                break;
           
            case BoxType.Mark:
                if (other.CompareTag(""))
                {
                    GetComponent<Destoryed>().enabled = true;
                    GetComponent<Boxes>().enabled = false;
                }
                break;
            case BoxType.InkMark:
                if (other.CompareTag(""))//check ink
                {
                    if (true)
                    {
                        GetComponent<Destoryed>().enabled = true;
                        GetComponent<Boxes>().enabled = false;
                    }
                }
                break;
            case BoxType.Enemy:
                if (other.CompareTag(""))
                {
                    GetComponent<Destoryed>().enabled = true;
                    GetComponent<Boxes>().enabled = false;
                }
                break;
            case BoxType.Water:
                if (other.CompareTag(""))
                {
                    GetComponent<Destoryed>().enabled = true;
                    GetComponent<Boxes>().enabled = false;
                }
                break;
            default:
                break;
        }
    }
}
