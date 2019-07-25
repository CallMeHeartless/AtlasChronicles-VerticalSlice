using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageScroller : MonoBehaviour
{
    [SerializeField] bool m_bHorSine, m_bVertSine = false;
    [SerializeField] float m_fHorizontalSpeed = 0.1f;
    [SerializeField] float m_fverticalSpeed = 0.2f;
    private RawImage m_rImageToScroll;  //i.e. the texture
    float originalX, originalY = 0.0f;

    Rect texCoords;
    // Start is called before the first frame update
    void Start()
    {
        m_rImageToScroll = GetComponent<RawImage>();
        originalX = texCoords.x;
    }

    // Update is called once per frame
    void Update()
    {
        texCoords = m_rImageToScroll.uvRect;
        
        //Scroll Horizontally
        if(m_bHorSine)
        {
            texCoords.x = originalX + Mathf.Sin(Time.time * m_fHorizontalSpeed);
        }
        else
        {
            if (m_fHorizontalSpeed != 0.0f)
            {
                texCoords.x -= Time.deltaTime * m_fHorizontalSpeed;
            }
            if (texCoords.x <= -1f || texCoords.x >= 1f)
            {
                texCoords.x = 0f;
            }
        }

        if(m_bVertSine)
        {
            texCoords.y = originalY + Mathf.Sin(Time.time * m_fverticalSpeed);
        }
        else
        {
            if (m_fverticalSpeed != 0.0f)
            {
                texCoords.y -= Time.deltaTime * m_fverticalSpeed;
            }

            if (texCoords.y <= -1f || texCoords.y >= 1f)
            {
                texCoords.y = 0f;
            }
        }
        
        m_rImageToScroll.uvRect = texCoords;
    }
}
