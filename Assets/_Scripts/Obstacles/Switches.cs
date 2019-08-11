using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switches : MonoBehaviour
{
   //two type needs to be added
    public GameObject m_gEffectingObject;
    public enum m_eSwitch
    {
        Guild, groundPound, hit, BigHit
    }
    public enum m_Blockage
    {
        door, lift, Switch
    }
    public int PassNumber;
    //public m_eSwitch m_swSwitchType;
    public m_Blockage m_MatType;
    public bool SwitchIsOn = false;
    // Start is called before the first frame update
  
    private void Update()
    {
        //for testing 
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < 3)
        //    {
        //        SwitchPressed();
        //    }

        //}
    }
    // Update is called once per frame
    public void SwitchOn()
    {
        SwitchIsOn = true;
        SwitchPressed();
    }
    public void SwitchPressed()
    {
        //Debug.Log(SwitchIsOn);
        if (SwitchIsOn == false)
        {
            switch (m_MatType)
            {
                case m_Blockage.door:
                    m_gEffectingObject.GetComponent<Door>().SwitchChanged(PassNumber, false);
                    SwitchIsOn = true;
                    //passName  TRUE
                    break;
                case m_Blockage.lift:
                    //passName  TRUE
                    break;
                case m_Blockage.Switch:
                    //passName  TRUE
                    break;
                default:
                    break;
            }

        }
        else
        {
            switch (m_MatType)
            {
                case m_Blockage.door:
                    m_gEffectingObject.GetComponent<Door>().SwitchChanged(PassNumber, true);
                    SwitchIsOn = false;
                    //passName FALSE
                    break;
                case m_Blockage.lift:
                    //passName FALSE
                    break;
                case m_Blockage.Switch:
                    //passName FALSE
                    break;
                default:
                    break;
            }

        }
        //GetComponent<DamageController>().ResetDamage();
    }

   
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        switch (m_swSwitchType)
    //        {
    //            case m_eSwitch.Guild:

    //                break;
    //            case m_eSwitch.groundPound:

    //                break;
    //            case m_eSwitch.hit:

    //                break;
    //            case m_eSwitch.BigHit:

    //                break;
    //            default:
    //                break;
    //        }
    //        SwitchPressed();
    //    }
    //}
}
