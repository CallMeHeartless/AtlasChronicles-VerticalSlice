using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeInputTypeGameObject : MonoBehaviour
{
    [SerializeField] private GameObject m_rControlsGameobject;
    [SerializeField] private GameObject m_rKeyboardGameobject;

    void Update()
    {
        if(InputManager.s_bInputIsController)
        {
            m_rControlsGameobject.SetActive(true);
            m_rKeyboardGameobject.SetActive(false);
        }
        else
        {
            m_rControlsGameobject.SetActive(false);
            m_rKeyboardGameobject.SetActive(true);
        }
    }
}
