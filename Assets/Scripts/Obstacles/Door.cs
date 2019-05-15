using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool[] m_bDoorLocks;
    public GameObject m_gDoorOpen;
    public GameObject m_gDoorClosed;
    public float m_fSpeed = 0.01f;
    bool Unlocked = true;
    // Start is called before the first frame update
    void Start()
    {
        LockedDoor();
    }

    // Update is called once per frame
    void Update()
    {

        if (Unlocked == true)
        {
            transform.position = Vector3.MoveTowards(gameObject.transform.position, m_gDoorOpen.transform.position, m_fSpeed);


            //doorOpen
        }
        else
        {
            transform.position = Vector3.MoveTowards(gameObject.transform.position, m_gDoorClosed.transform.position, m_fSpeed);

            // doorClosed
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
        LockedDoor();
    }
    void LockedDoor(){

Unlocked = true;
        for (int i = 0; i< m_bDoorLocks.Length; i++)
        {
            if (m_bDoorLocks[i] == false)
            {
                Unlocked = false;
            }
        }
}
}
