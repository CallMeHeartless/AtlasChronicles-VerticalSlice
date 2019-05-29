using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountDownTimer : MonoBehaviour
{
    private float m_fPauseDuration = 0;
    public float m_fCurrentPause = 2;
    public bool TImerOn=false;
    GameObject TextUI;
    // Start is called before the first frame update
    void Start()
    {
        TextUI= GameObject.FindGameObjectWithTag("UI").transform.GetChild(4).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (TImerOn == true)
        {
            
                if (m_fPauseDuration <= 0)
                {

                GetComponent<DamageController>().ApplyHealing(1);
                TextUI.SetActive(false);
            }
                else
                {
                    m_fPauseDuration -= Time.deltaTime;
                TextUI.GetComponent<TextMeshPro>().text = System.Math.Round(m_fPauseDuration,2).ToString();
            }

        }
    }
    public void Damage()
    {
        Debug.Log("hit");
        switch (GetComponent<DamageController>().iCurrentHealth)
        {
            
            case 1:
                GetComponent<DamageController>().ApplyHealing(1);
                m_fPauseDuration = m_fCurrentPause;
                TextUI.GetComponent<TextMeshPro>().text = m_fPauseDuration.ToString();
                break;
            case 2:
                m_fPauseDuration = m_fCurrentPause;
                TextUI.SetActive(true);
                TImerOn = true;
                TextUI.GetComponent<TextMeshPro>().text = m_fPauseDuration.ToString();
               
                
                break;
            case 3:
                m_fPauseDuration = m_fCurrentPause;
                TImerOn = true;
                break;
            default:
                break;
        }
    }
    public void Death()
    {
        TextUI.SetActive(false);
        TImerOn = false;
        //door open
        Destroy(gameObject);
    }

}
