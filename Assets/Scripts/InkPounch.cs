using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InkPounch : MonoBehaviour
{
    float timer;
    Slider slide;
    public float m_fInkSlamAmount, m_fInkMapAttackAmount;
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
    public bool UseInk(float m_fAmount, string m_sType)
    {
        if (m_fAmount <= slide.value)
        {
            slide.value -= m_fAmount;
            if (m_sType =="Slam")
            {
                //Ink Slam effects
            }
            else if(m_sType == "Attack")
            {
                // Ink MapAttack Effect
            }
            return (true);
        }
        else
        {
            return (false);
        }
    }
    public void AddedingInk(float m_fAmount)
    {
        slide.value += m_fAmount;
        if (slide.value >=1)
        {
            slide.value = 1;
        }
    }
}
