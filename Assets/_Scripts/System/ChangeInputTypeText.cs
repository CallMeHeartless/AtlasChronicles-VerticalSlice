using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Changes text depending on which input type is being used (Mouse/Controller)
[RequireComponent(typeof(TextMeshProUGUI))]
public class ChangeInputTypeText : MonoBehaviour
{
    TextMeshProUGUI m_strCurrentText;
    [SerializeField] string m_strControllerText = "controller-unassigned";
    [SerializeField] string m_strKeyboardText = "keymouse-unassigned";

    private void Start()
    {
        m_strCurrentText = GetComponent<TextMeshProUGUI>();
        ChangeText();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeText();
    }

    void ChangeText()
    {
        if (InputManager.s_bInputIsController)
        {
            m_strCurrentText.text = m_strControllerText;
        }
        else
        {
            m_strCurrentText.text = m_strKeyboardText;
        }
    }
}
