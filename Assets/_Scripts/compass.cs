using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class compass : MonoBehaviour
{
    Image m_rImage;
    Transform m_rPlayer;

    // Start is called before the first frame update
    private void Start()
    {
        m_rImage = GetComponent<Image>();
        m_rPlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame  
    void Update()
    {
        m_rImage.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, m_rPlayer.localEulerAngles.y);
    }
}
