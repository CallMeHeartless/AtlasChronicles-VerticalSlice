using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndSwitchTimer : MonoBehaviour
{
    private DamageMessage deathCall=new DamageMessage();
    public GameObject startPoint;
    // Start is called before the first frame update
    void Start()
    {
        deathCall.damage = 4;
        deathCall.source = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Damage()
    {
        Debug.Log("Chit");
        if (GameObject.FindGameObjectWithTag("UI").transform.GetChild(4).gameObject.activeSelf == false)
        {
            GetComponent<DamageController>().ResetDamage();// ApplyHealing(1);
        }
        else
        {
            DamageMessage message = new DamageMessage();
            message.damage = 4;
            message.source = gameObject;
            gameObject.GetComponent<DamageController>().ForceKill(message);
        }
    }
    public void death()
    {
        //damagedoor
        Destroy(startPoint);
        Destroy(gameObject);
    }
}
