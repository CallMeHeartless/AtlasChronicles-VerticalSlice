using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool[] m_bDoorLocks;
    public GameObject m_gDoorOpen;
    public GameObject m_gDoorClosed;
    public float m_fSpeed = 0.01f;
    bool Unlocked = false;
    private bool m_bMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        LockedDoor();
        m_bMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bMoving)
        {
            
            if (Unlocked == true)
            {
                if (Vector3.Distance(transform.position, m_gDoorOpen.transform.position) < .1f)
                {
                    m_bMoving = false;
                }
                transform.position = Vector3.MoveTowards(gameObject.transform.position, m_gDoorOpen.transform.position, m_fSpeed);
               

                //doorOpen
            }
            else
            {
                if (Vector3.Distance(transform.position, m_gDoorClosed.transform.position) < .1f)
                {
                    m_bMoving = false;
                }
                transform.position = Vector3.MoveTowards(gameObject.transform.position, m_gDoorClosed.transform.position, m_fSpeed * 3);
               
                // doorClosed
            }
        }
    }

    public void SwitchChanged(int Location, bool correct) {
        switch (Location)
        {
            case 0:
                m_bDoorLocks[Location] = correct;
                break;
            case 1:
                m_bDoorLocks[Location] = correct;
                break;
            case 2:
                m_bDoorLocks[Location] = correct;
                break;
            case 3:
                m_bDoorLocks[Location] = correct;
                break;
            default:
                break;
        }


        bool newLock = LockedDoor();
        Debug.Log("newLock: "+newLock);
        Debug.Log("Unlocked: "+Unlocked);
        if (newLock != Unlocked)
        {
            Unlocked = newLock;
            m_bMoving = true;
        }
    }
    bool LockedDoor(){

        //Unlocked = true;
       
        for (int i = 0; i< m_bDoorLocks.Length; i++)
        {
            if (m_bDoorLocks[i] == false)
            {
                return false;
            }
        }
        return true;
    }
}
