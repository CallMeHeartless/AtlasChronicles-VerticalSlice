using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnBox : MonoBehaviour
{
   // public int ItemCount;
   // public GameObject Chest;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Respawn()
    {

        //GameObject NewChest = Instantiate(Chest, transform.position, transform.rotation);
        //  NewChest.GetComponent<Destoryed>().m_intSecondaryItem = ItemCount;
        gameObject.tag = "Untagged";
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<Boxes>().enabled = true;
        gameObject.GetComponent<BoxCollider>().enabled = true;
        gameObject.GetComponent<RespawnBox>().enabled = false;

    }
}
