using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDestorySetUp : MonoBehaviour
{
    public enum BoxType
    {
        Hit,EnemyHit,Telport,Spike,Water,Slam,Tag
    }
    public BoxType ThisBoxType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (BoxType.Telport == ThisBoxType)
        {
            //teloport
            gameObject.GetComponent<Destoryed>().enabled = true;
        } 
    }
    private void OnCollisionEnter(Collision collision)
    {
        switch (ThisBoxType)
        {
            case BoxType.Hit:
                if (collision.gameObject.CompareTag("Weapon"))
                {
                    gameObject.GetComponent<Destoryed>().enabled = true;
                }
                break;
            case BoxType.EnemyHit:
                if (collision.gameObject.CompareTag("Enemy"))
                {
                    gameObject.GetComponent<Destoryed>().enabled = true;
                }
                break;
           
            case BoxType.Spike:
                if (collision.gameObject.CompareTag("Spike"))
                {
                    gameObject.GetComponent<Destoryed>().enabled = true;
                }
                break;
            case BoxType.Water:
                if (collision.gameObject.CompareTag("Water"))
                {
                    gameObject.GetComponent<Destoryed>().enabled = true;
                }
                break;
            case BoxType.Slam:
                if (collision.gameObject.CompareTag("Player"))
                {
                    //slam check
                    gameObject.GetComponent<Destoryed>().enabled = true;
                }
                break;
            case BoxType.Tag:
                if (collision.gameObject.CompareTag("Tag"))
                {
                    gameObject.GetComponent<Destoryed>().enabled = true;
                }
                break;
            default:
                break;
        }
    }
}
