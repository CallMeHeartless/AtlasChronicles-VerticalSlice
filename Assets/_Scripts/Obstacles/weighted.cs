using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MessageSystem;

public class weighted : MonoBehaviour, IMessageReceiver
{
    public enum Color
    {
        Green,
        Blue,
        Red
    }
    public List<GameObject> ObjectInArea = new List<GameObject>();
    public List<Color> ObjectColor = new List<Color>();
    public List<int> ObjectWeight = new List<int>();

    public int m_PassNumber;
    public List<MonoBehaviour> m_gEffectingObject;
    

    public int[] m_ColorListRequirment = new int[3];
    public int m_WeightRequirment = 0;
    public int[] m_ColorList = new int[3];
    public int m_Weight = 0;
    public bool m_RequirmentWeight = true;
    // List<GameObject> ObjectInArea = new List<GameObject>();
    //List<Color> ObjectColor = new List<Color>();
    //public GameObject[] ObjectInArea;
    //public Color[] ObjectColor;
    // Start is called before the first frame update
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TeleportBox"))
        {
            //m_gObjects.Add(new ObjectPressPad(other.gameObject,other.GetComponent<Switchable>().ObjectColor,other.GetComponent<Switchable>().Weight));
            ObjectInArea.Add(other.gameObject);
            ObjectColor.Add(other.GetComponent<Switchable>().ObjectColor);
            ObjectWeight.Add(other.GetComponent<Switchable>().Weight);
            //Objects.Sort(Sorter);
            if (m_RequirmentWeight)
            {
                m_Weight += ObjectWeight[ObjectWeight.Count-1 ];
            }
            else
            {
                switch (ObjectColor[ObjectColor.Count-1])
                {
                    case Color.Green:
                        m_ColorList[0]++;
                        break;
                        
                    case Color.Red:
                        m_ColorList[1]++;
                        break;
                    case Color.Blue:
                        m_ColorList[2]++;
                        break;
                    default:
                        break;
                }
            }
            UpdateDoor();
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("TeleportBox"))
        {
            int i = ObjectInArea.IndexOf(other.gameObject);
            if (m_RequirmentWeight)
            {
                m_Weight -= ObjectWeight[i];
            }
            else
            {
                switch (ObjectColor[i])
                {
                    case Color.Green:
                        m_ColorList[0]--;
                        break;

                    case Color.Red:
                        m_ColorList[1]--;
                        break;
                    case Color.Blue:
                        m_ColorList[2]--;
                        break;
                    default:
                        break;
                }

                // m_gObjects.Remove(new ObjectPressPad(other.gameObject, other.GetComponent<Switchable>().ObjectColor, other.GetComponent<Switchable>().Weight));

            }
            UpdateDoor();
        }
    }
 void UpdateDoor()
    {
        if (m_RequirmentWeight)
        {
            if (m_Weight == m_WeightRequirment)
            {
                for (int i = 0; i < m_gEffectingObject.Count; ++i)
                {
                    IMessageReceiver target = m_gEffectingObject[i] as IMessageReceiver;
                    target.OnReceiveMessage(MessageType.eOn, m_PassNumber);//true
                }
            }
            else
            {
                for (int i = 0; i < m_gEffectingObject.Count; ++i)
                {
                    IMessageReceiver target = m_gEffectingObject[i] as IMessageReceiver;
                    target.OnReceiveMessage(MessageType.eOff, m_PassNumber);//false
                }
            }

        }
        else
        {
            if ((m_ColorList[0] == m_ColorListRequirment[0])&& (m_ColorList[1] == m_ColorListRequirment[1])&& (m_ColorList[2] == m_ColorListRequirment[2]))
            {
                for (int i = 0; i < m_gEffectingObject.Count; ++i)
                {
                    IMessageReceiver target = m_gEffectingObject[i] as IMessageReceiver;
                    target.OnReceiveMessage(MessageType.eOn, m_PassNumber);//true
                }
            }
            else
            {
                for (int i = 0; i < m_gEffectingObject.Count; ++i)
                {
                    IMessageReceiver target = m_gEffectingObject[i] as IMessageReceiver;
                    target.OnReceiveMessage(MessageType.eOff, m_PassNumber);//false
                }
            }
        }

    }

    public void OnReceiveMessage(MessageType _eType, object _message)
    {
        switch (_eType)
        {
           
            default: break;
        }
    }
}
