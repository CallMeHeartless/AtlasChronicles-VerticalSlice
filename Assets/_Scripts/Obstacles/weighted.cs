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
        Red,
        None
    }
    public List<GameObject> ObjectInArea = new List<GameObject>();
    public List<Color> ObjectColor = new List<Color>();
    public List<int> ObjectWeight = new List<int>();

    public int[] m_PassNumber;
    public List<MonoBehaviour> m_gEffectingObject;
    private bool pastWasFalse = true;

    public int[] m_ColorListRequirment = new int[3];
    public int m_WeightRequirment = 0;
    public int[] m_ColorList = new int[3];
    public int m_Weight = 0;
    public bool m_RequirmentWeight = true;
    public bool m_RequirmentColor = false;

    public bool m_IsOn = false;
    public bool m_bTypeDoor = false;
    public float m_fSpeed;
    GameObject WeightedLoaction;
    GameObject m_Topplate;
    // Start is called before the first frame update

    private void Start()
    {
        m_Topplate = transform.GetChild(0).transform.GetChild(1).gameObject;
        WeightedLoaction = transform.GetChild(0).transform.GetChild(2).gameObject; 
    }
    private void Update()
    {
        if (m_Topplate.transform.position != WeightedLoaction.transform.position)
        {
            m_Topplate.transform.position = Vector3.MoveTowards(m_Topplate.transform.position, WeightedLoaction.transform.position, m_fSpeed);
        }
    }

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
                m_Weight += ObjectWeight[ObjectWeight.Count - 1];
            }
            else if (m_RequirmentColor == true)
           
          
            {
                switch (ObjectColor[ObjectColor.Count - 1])
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
        else if (other.CompareTag("Player"))
        {
           
            if (!ObjectInArea.Contains(other.gameObject))
            {
                //Debug.Log("durug");
                ObjectInArea.Add(other.gameObject);
                ObjectColor.Add(Color.None);
                ObjectWeight.Add(other.GetComponent<PlayerController>().getWeight());
                if (m_RequirmentWeight)
                {
                    m_Weight += ObjectWeight[ObjectWeight.Count - 1];
                }
                UpdateDoor();
            }
            else
            {
                Debug.Log(ObjectInArea.Contains(other.gameObject));
            }

            //ObjectInArea.Add(other.gameObject);
            //ObjectColor.Add(Color.None);
            //ObjectWeight.Add(other.GetComponent<PlayerController>().getWeight());
        }
        else if (other.CompareTag("Enemy"))
            {
            if (!ObjectInArea.Contains(other.gameObject))
            { //m_gObjects.Add(new ObjectPressPad(other.gameObject,other.GetComponent<Switchable>().ObjectColor,other.GetComponent<Switchable>().Weight));
                ObjectInArea.Add(other.gameObject);
                ObjectColor.Add(Color.None);
                ObjectWeight.Add(other.GetComponent<EnemyController>().GetWeight());
                if (m_RequirmentWeight)
                {
                    m_Weight += ObjectWeight[ObjectWeight.Count - 1];
                }
                // UpdateDoor();
            }
            else
            {
                Debug.Log(ObjectInArea.Contains(other.gameObject));
            }
            
                    
        }
        Weighdown();
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
            else if (m_RequirmentColor == true)
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
                
                //m_gObjects.Remove(new ObjectPressPad(other.gameObject, other.GetComponent<Switchable>().ObjectColor, other.GetComponent<Switchable>().Weight));

            }
            UpdateDoor();
            ObjectInArea.RemoveAt(i);
            ObjectColor.RemoveAt(i);
            ObjectWeight.RemoveAt(i);
        }
        else if (other.CompareTag("Player"))
        {
            if (ObjectInArea.Contains(other.gameObject))
            {
                int i = ObjectInArea.IndexOf(other.gameObject);
                if (m_RequirmentWeight)
                {
                    m_Weight -= ObjectWeight[i];
                }
                ObjectInArea.RemoveAt(i);
                ObjectColor.RemoveAt(i);
                ObjectWeight.RemoveAt(i);

                UpdateDoor();
            }
            else
            {
                Debug.Log(ObjectInArea.Contains(other.gameObject));
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            if (ObjectInArea.Contains(other.gameObject))
            {
                int i = ObjectInArea.IndexOf(other.gameObject);
                if (m_RequirmentWeight)
                {
                    m_Weight -= ObjectWeight[i];
                }
                ObjectInArea.RemoveAt(i);
                ObjectColor.RemoveAt(i);
                ObjectWeight.RemoveAt(i);

                UpdateDoor();
            }
        }

        //Debug.Log("gems");
        Weighdown();
        //Debug.Log("gems collected");

        if (ObjectInArea.Count == 0)
        {
           
                m_Weight = 0;
           
        }
        
    }
    void UpdateDoor()
    {
        if (m_RequirmentWeight)
        {
            if (m_Weight >= m_WeightRequirment)
            {
                if (!m_IsOn)
                {

                    if (m_bTypeDoor== true)
                    {
                        gameObject.GetComponent<SwitchController>().switchoff();
                    }
                    else
                    {
                        //Debug.Log("count: "+m_gEffectingObject.Count);

                        for (int i = 0; i < m_gEffectingObject.Count; ++i)
                        {
                            m_gEffectingObject[i].gameObject.SetActive(true);
                            IMessageReceiver target = m_gEffectingObject[i] as IMessageReceiver;
                            target.OnReceiveMessage(MessageType.eOn, m_PassNumber[i]);//true
                        }
                        //Debug.Log("out: ");

                        pastWasFalse = false;
                       
                        
                    }
                    m_IsOn = true;
                }
            }
            else
            {
                if (pastWasFalse == false)
                {
                    if (m_IsOn)
                    {
                        if (m_bTypeDoor == true)
                        {
                            gameObject.GetComponent<SwitchController>().switchoff();
                        }
                        else
                        {

                            for (int i = 0; i < m_gEffectingObject.Count; ++i)
                            {
                                IMessageReceiver target = m_gEffectingObject[i] as IMessageReceiver;
                                target.OnReceiveMessage(MessageType.eOff, m_PassNumber[i]);//false
                            }
                            pastWasFalse = true;
                        }
                        m_IsOn = false;
                    }
                }

            }

        }
        else if(m_RequirmentColor == true)
        {
            if ((m_ColorList[0] == m_ColorListRequirment[0]) && (m_ColorList[1] == m_ColorListRequirment[1]) && (m_ColorList[2] == m_ColorListRequirment[2]))
            {
                for (int i = 0; i < m_gEffectingObject.Count; ++i)
                {
                    IMessageReceiver target = m_gEffectingObject[i] as IMessageReceiver;
                    target.OnReceiveMessage(MessageType.eOn, m_PassNumber[i]);//true
                }
                pastWasFalse = false;
            }
            else
            {
                if (pastWasFalse == false)
                {
                    for (int i = 0; i < m_gEffectingObject.Count; ++i)
                    {
                        IMessageReceiver target = m_gEffectingObject[i] as IMessageReceiver;
                        target.OnReceiveMessage(MessageType.eOff, m_PassNumber[i]);//false
                    }
                    pastWasFalse = true;
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
    public void switching()
    {
        if (pastWasFalse == true)
        {
            for (int i = 0; i < m_gEffectingObject.Count; ++i)
            {
                IMessageReceiver target = m_gEffectingObject[i] as IMessageReceiver;
                target.OnReceiveMessage(MessageType.eOn, m_PassNumber[i]);//true
            }
            pastWasFalse = false;
        }
        else
        {
            
            for (int i = 0; i < m_gEffectingObject.Count; ++i)
            {
                IMessageReceiver target = m_gEffectingObject[i] as IMessageReceiver;
                target.OnReceiveMessage(MessageType.eOff, m_PassNumber[i]);//false
            }
            pastWasFalse = true;


        }
        DamageController m_rDamageController = gameObject.GetComponent<DamageController>();
        m_rDamageController.ResetDamage();
        Debug.Log("hit");
}
    void Weighdown()
    {
        //based on weight deteraned how far the top piller goes to said height
        if (m_Weight == 0)
        {
            WeightedLoaction = transform.GetChild(0).transform.GetChild(2).gameObject;
        }
        else if (m_Weight< m_WeightRequirment)
        {
            WeightedLoaction = transform.GetChild(0).transform.GetChild(3).gameObject;
        }
        else if (m_Weight == m_WeightRequirment)
        {
            WeightedLoaction = transform.GetChild(0).transform.GetChild(4).gameObject;
        }
        else if(m_Weight> m_WeightRequirment)
        {
            WeightedLoaction = transform.GetChild(0).transform.GetChild(5).gameObject;
        }
    }
}
