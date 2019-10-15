using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimationController : MonoBehaviour
{
    LoadingScreen m_rLoadingScreen;

    // Start is called before the first frame update
    void Start()
    {
        m_rLoadingScreen = transform.parent.GetComponent<LoadingScreen>();
    }

    public void HideLoadingScreen()
    {
        m_rLoadingScreen.HideLoadingScreen();
    }
}
