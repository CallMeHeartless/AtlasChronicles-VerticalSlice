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
        TextUI= GameObject.FindGameObjectWithTag("UI").transform.GetChild(5).gameObject;
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
                if (GetComponent<Switchs>() != null)
                {
                    gameObject.GetComponent<Switchs>().SwitchPressed();

                    Debug.Log("happens");
                }
                TImerOn = false;
            }
                else
                {
                    m_fPauseDuration -= Time.deltaTime;
                TextUI.GetComponent<TextMeshProUGUI>().text = System.Math.Round(m_fPauseDuration,2).ToString();
            }

        }
    }
    public void Damage()
    {
        Debug.Log("hit");
        
        m_fPauseDuration = m_fCurrentPause;
        TextUI.SetActive(true);
      
        TImerOn = true;
        TextUI.GetComponent<TextMeshProUGUI>().text = m_fPauseDuration.ToString();
        GetComponent<DamageController>().ResetDamage();
        

    }
    public void Death()
    {
        TextUI.SetActive(false);
        TImerOn = false;
        //door open
        Destroy(gameObject);
    }

}
