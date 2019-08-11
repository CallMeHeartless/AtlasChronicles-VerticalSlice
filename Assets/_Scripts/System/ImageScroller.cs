using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageScroller : MonoBehaviour
{
    [SerializeField] bool m_bHorSine, m_bVertSine = false;  //Check for whether to activate sine on specified direction
    [SerializeField] float m_fHorizontalSpeed = 0.1f;       //Horizontal speed value to move image
    [SerializeField] float m_fVerticalSpeed = 0.2f;         //Vertical speed value to move image
    private RawImage m_rImageToScroll;                      //i.e. the texture/image to scroll
    private Rect texCoords;
    private float m_fOriginalX, m_fOriginalY = 0.0f;                //The initial position of the image

    // Start is called before the first frame update
    void Start()
    {
        m_rImageToScroll = GetComponent<RawImage>();
        m_fOriginalX = texCoords.x;
    }

    // Update is called once per frame
    void Update()
    {
        texCoords = m_rImageToScroll.uvRect;
        
        if(m_bHorSine)
        {
            //Bounce image back and forth, horizontally, using sine
            texCoords.x = m_fOriginalX + Mathf.Sin(Time.time * m_fHorizontalSpeed);
        }
        else
        {
            //Scroll image Horizontally
            if (m_fHorizontalSpeed != 0.0f)
            {
                texCoords.x -= Time.deltaTime * m_fHorizontalSpeed;
            }
            if (texCoords.x <= -1.0f || texCoords.x >= 1.0f)
            {
                texCoords.x = 0.0f;
            }
        }

        //Scroll Vertically
        if (m_bVertSine)
        {
            //Bounce image back and forth, vertically, using sine
            texCoords.y = m_fOriginalY + Mathf.Sin(Time.time * m_fVerticalSpeed);
        }
        else
        {
            //Scroll image Vertically
            if (m_fVerticalSpeed != 0.0f)
            {
                texCoords.y -= Time.deltaTime * m_fVerticalSpeed;
            }

            if (texCoords.y <= -1.0f || texCoords.y >= 1.0f)
            {
                texCoords.y = 0.0f;
            }
        }
        
        m_rImageToScroll.uvRect = texCoords;
    }
}
