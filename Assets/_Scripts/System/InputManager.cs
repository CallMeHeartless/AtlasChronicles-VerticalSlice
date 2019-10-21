using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static bool s_bInputController = true;
    public static InputManager m_instance = null;

    private void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
        }
        else if (m_instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    static public InputManager GetInstance()
    {
        return m_instance;
    }

    // Update is called once per frame
    void Update()
    {
        InputChecker();
    }

    /// <summary>
    /// Checks whether the last input pressed was from the keyboard or controller and
    ///         sets the static variable of s_bInputController to true or false
    /// </summary>
    static void InputChecker()
    {
        if (Input.anyKeyDown)
        {
            s_bInputController = false;
        }

        //If LT, RT, horizontal, vertical, RHorizontal and RVertical 
        //buttons are pressed on controller
        if (Input.GetAxis("XBoxLT") > 0 || Input.GetAxis("XBoxRT") > 0
              || Input.GetAxis("XBoxHor") != 0 || Input.GetAxis("XBoxVert") != 0
              || Input.GetAxis("XBoxRHor") != 0 || Input.GetAxis("XBoxRVert") != 0
              || Input.GetAxis("DPadX") != 0 || Input.GetAxis("DPadY") != 0)
        {
            s_bInputController = true;
        }

        //If any joystick keys are pressed on the xbox controller, set controller in use
        for (int i = 0; i < 20; i++)
        {
            if (Input.GetKeyDown("joystick 1 button " + i))
            {
                s_bInputController = true;
            }
        }
        //print((s_bInputController ? "Controller" : "Key"));
    }
}
