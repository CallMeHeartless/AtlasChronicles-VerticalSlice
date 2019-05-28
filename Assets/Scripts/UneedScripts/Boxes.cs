using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxes : MonoBehaviour
{
  
    public enum BoxType
    {
        Standend,Slam,InkSlam,Map,InkMap,Teleport,Mark,InkMark,Enemy,Fall,Water,Weapon

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
                //}

        }
                break;
            case BoxType.InkSlam:
                if (other.CompareTag("Player"))//check ink
                {
                    //if (other.GetComponent<PlayerController>().m_bGroundPound)
                    //{
                        string Attack = "Slam";
                        if (GameObject.FindGameObjectWithTag("UI").transform.GetChild(4).GetComponent<InkPounch>().UseInk(GameObject.FindGameObjectWithTag("UI").transform.GetChild(4).GetComponent<InkPounch>().m_fInkSlamAmount,Attack))
                        {
                            GetComponent<Destoryed>().enabled = true;
                            GetComponent<Boxes>().enabled = false;
                        }

                   // }
                }
                break;
            case BoxType.Map:
                if (other.CompareTag("PlayerWeapon"))
                {
                    
                        GetComponent<Destoryed>().enabled = true;
                        GetComponent<Boxes>().enabled = false;
                    

                }
                break;
            case BoxType.InkMap:
                if (other.CompareTag("PlayerWeapon"))//check ink
                {

                    if (GameObject.FindGameObjectWithTag("UI").transform.GetChild(4).GetComponent<InkPounch>().UseInk(GameObject.FindGameObjectWithTag("UI").transform.GetChild(4).GetComponent<InkPounch>().m_fInkMapAttackAmount, "Attack"))
                    {
                        GetComponent<Destoryed>().enabled = true;
                            GetComponent<Boxes>().enabled = false;
                        }

                    
                }
                break;
            case BoxType.Mark:
                if (other.CompareTag("Tag"))//SwitchTag Prefab
                {
                    GetComponent<Destoryed>().enabled = true;
                    GetComponent<Boxes>().enabled = false;
                }
                break;
            case BoxType.InkMark:
                if (other.CompareTag("Tag"))//check ink
                {
                    if (true)
                    {
                        GetComponent<Destoryed>().enabled = true;
                        GetComponent<Boxes>().enabled = false;
                    }
                }
                break;
            case BoxType.Enemy:
                if (other.CompareTag("EnemyWeapon"))//end enemy
                {
                    GetComponent<Destoryed>().enabled = true;
                    GetComponent<Boxes>().enabled = false;
                }
                break;
            case BoxType.Water:
                if (other.CompareTag("Water"))
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
