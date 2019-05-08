using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InkPounch : MonoBehaviour
{
    float timer;
    Slider slide;
    // Start is called before the first frame update
    void Start()
    {
        slide = GetComponent<Slider>();
        timer = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer<=0)
        {
            if (slide.value <= 0)
            {
                slide.value = 0;
            }
            else
            {
                slide.value -= 0.001f;
                timer = 1;
            }
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
    bool UseInk(float Amount)
    {
        if (Amount <= slide.value)
        {
            slide.value -= Amount;
            return (true);
        }
        else
        {
            return (false);
        }
    }
    void AddedingInk(float Amount)
    {
        slide.value += Amount;
        if (slide.value >=1)
        {
            slide.value = 1;
        }
    }
}
