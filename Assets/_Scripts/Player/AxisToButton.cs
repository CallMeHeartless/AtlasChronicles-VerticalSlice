using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisToButton 
{
    public enum InputState {
        FirstPressed,
        Pressed,
        FirstReleased,
        Released
    }
    public string m_strAxis;
    private float m_fPrevious = 0.0f;
    private float m_fCurrent = 0.0f;

    // Update is called once per frame
    public void Update()
    {
        m_fPrevious = m_fCurrent;
        m_fCurrent = Input.GetAxisRaw(m_strAxis);
    }

    public InputState GetCurrentState() {
        if(m_fCurrent == 0.0f) {
            if(m_fPrevious == 0.0f) {
                return InputState.Released;
            } else {
                return InputState.FirstReleased;
            }
        } else {
            if(m_fPrevious == 0.0f) {
                return InputState.FirstPressed;
            } else {
                return InputState.Pressed;
            }
        }
    }
}
