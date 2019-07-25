using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ButtonMask : MonoBehaviour
{
    [SerializeField] float m_fAlphaThreshold = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        //Set alpha to wrap around the image while excluding transparent areas.
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = m_fAlphaThreshold;
    }
}
