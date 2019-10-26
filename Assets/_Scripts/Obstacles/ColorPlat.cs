﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPlat : MonoBehaviour
{
    
    public GameObject m_gEffectingObject;
    private Animator m_rAnimator;
    public enum m_Colors
    {
        blue, green, red
    }
    public enum m_Blockage
    {
        door,lift,Switch
    }
    public int PassNumber;
    public m_Colors m_colCurrentColor;
    public m_Colors m_colSetColor;
    public m_Blockage m_MatType;
    private MeshRenderer m_rMeshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        m_rAnimator = GetComponentInChildren<Animator>();
        m_rMeshRenderer = GetComponentInChildren<MeshRenderer>();
        if (!m_rMeshRenderer)
        {
            Debug.LogError("ERROR: No mesh renderer found on " + name);
        }
        m_rMeshRenderer.material.SetInt("_Select", (int)m_colCurrentColor);

        //setting the color of the wheel to what is stored in m_colCurrentColor
        if ((m_Colors.green == m_colCurrentColor)|| (m_Colors.red == m_colCurrentColor))
        {
            if (m_rAnimator)
            {
                m_rAnimator.SetTrigger("Switch");
            }
        }
       
        // GetComponentInChildren<MeshRenderer>().material = m_matMaterialColor[(sbyte)m_colCurrentColor];
        CorrectColor();

    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < 3)
        //    {
        //        changeColor();
        //    }
           
        //}
    }
    // Update is called once per frame
    private void CorrectColor()
    {
        if (m_colSetColor == m_colCurrentColor)
        {
            switch (m_MatType)
            {
                case m_Blockage.door:
                    m_gEffectingObject.GetComponent<Door>().SwitchChanged(PassNumber, true);
                    //passName  TRUE
                    break;
                case m_Blockage.lift:
                    //passName  TRUE
                    break;
                case m_Blockage.Switch:
                    //passName  TRUE
                    break;
                default:
                    break;
            }

        }
        else
        {
            switch (m_MatType)
            {
                case m_Blockage.door:
                    m_gEffectingObject.GetComponent<Door>().SwitchChanged(PassNumber, false);
                    //passName FALSE
                    break;
                case m_Blockage.lift:
                    //passName FALSE
                    break;
                case m_Blockage.Switch:
                    //passName FALSE
                    break;
                default:
                    break;
            }

        }
        
    }

    public void changeColor()
    {
        //Debug.Log("Colorchange");
        if (m_Colors.red == m_colCurrentColor)
        {
            m_colCurrentColor = 0;
           
        }
        else
        {
            
            m_colCurrentColor += 1;
        }

        // Play animation
        if (m_rAnimator)
        {
            m_rAnimator.SetTrigger("Switch");
        }

        m_rMeshRenderer.material.SetInt("_Select", (int)m_colCurrentColor);
       // GetComponentInChildren<MeshRenderer>().material = m_matMaterialColor[(sbyte)m_colCurrentColor];
        CorrectColor();
        GetComponent<DamageController>().ResetDamage();
    }
}
