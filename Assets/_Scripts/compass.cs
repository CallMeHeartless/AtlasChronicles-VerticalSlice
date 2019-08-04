using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class compass : MonoBehaviour
{
    public GameObject m_rImage;
    public Transform m_rPlayer;
    // Start is called before the first frame update
   
    // Update is called once per frame  
    void Update()
    {

        m_rImage.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, m_rPlayer.localEulerAngles.y);

        
    }
}
