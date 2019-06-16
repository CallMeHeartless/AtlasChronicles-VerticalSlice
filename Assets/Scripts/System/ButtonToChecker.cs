using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonToChecker : MonoBehaviour
{
    Button m_rButton;

    // Start is called before the first frame update
    void Start()
    {
        m_rButton = GetComponent<Button>();
    }

    // Update is called once per frame
    public void ToggleCheck(Toggle _toggle)
    {
        if (_toggle.isOn)
        {
            _toggle.isOn = false;
        }
        else
        {
            _toggle.isOn = true;
        }
    }
}
