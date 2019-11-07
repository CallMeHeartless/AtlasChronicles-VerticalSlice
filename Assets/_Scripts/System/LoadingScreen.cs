using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    private const float MIN_WAIT_TIME = 10.0f;

    public static LoadingScreen s_LoadingScrInstance;

    [SerializeField] private GameObject m_rLoadingScrSpace;
    [SerializeField] private GameObject m_rLoadingOverlay;
    private GameObject m_rCurrentLoadingPanel;

    private AsyncOperation m_currentLoadingOperation;
    private Animator m_animator;
    private Canvas m_canvas;
    private float m_fTimeElapsed = 0.0f;

    private bool m_bIsLoading = false;
    private bool m_bAnimationHasTriggered = false;

    private void Awake()
    {
        if (s_LoadingScrInstance == null)
        {
            s_LoadingScrInstance = this;
            AssignCurrentSceneCanvas();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (m_bIsLoading)
        {
            if (m_currentLoadingOperation.allowSceneActivation && !m_bAnimationHasTriggered)
            {
                m_animator.SetTrigger("Hide");
                m_bAnimationHasTriggered = true;
            }
            else
            {
                m_fTimeElapsed += Time.deltaTime;
                if (m_fTimeElapsed >= MIN_WAIT_TIME)
                {
                    //Allow loading screen to finish if minimum waiting time has been reached
                    m_currentLoadingOperation.allowSceneActivation = true;
                }
            }
        }
    }

    void AssignCurrentSceneCanvas()
    {
        m_rCurrentLoadingPanel = s_LoadingScrInstance.m_rCurrentLoadingPanel;
        m_rLoadingScrSpace = s_LoadingScrInstance.m_rLoadingScrSpace;
        m_rLoadingOverlay = s_LoadingScrInstance.m_rLoadingOverlay;
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            m_rCurrentLoadingPanel = m_rLoadingScrSpace;
        }
        else
        {
            m_rCurrentLoadingPanel = m_rLoadingOverlay;
        }
        m_animator = m_rCurrentLoadingPanel.GetComponent<Animator>();
        m_canvas = m_rCurrentLoadingPanel.GetComponent<Canvas>();
        if(m_canvas != null)
        {
            if (m_canvas.worldCamera == null)
            {
                Camera cam = GameObject.Find("Camera").GetComponent<Camera>();
                if(cam != null)
                {
                    m_canvas.worldCamera = cam;
                }
            }
        }
    }

    public void ActivateLoadingScreen(AsyncOperation _operation)
    {
        s_LoadingScrInstance.m_currentLoadingOperation = _operation;
        m_currentLoadingOperation = s_LoadingScrInstance.m_currentLoadingOperation;

        AssignCurrentSceneCanvas();

        m_rCurrentLoadingPanel.SetActive(true);
        m_currentLoadingOperation.allowSceneActivation = false;
        m_fTimeElapsed = 0.0f;
        m_bAnimationHasTriggered = false;
        s_LoadingScrInstance.m_bIsLoading = true;
        m_bIsLoading = s_LoadingScrInstance.m_bIsLoading;

        //// Play the fade in animation:          
        m_animator.SetTrigger("Show");
    }

    public void HideLoadingScreen()
    {
        m_rCurrentLoadingPanel.SetActive(false);
        m_rLoadingScrSpace.SetActive(false);
        m_rLoadingOverlay.SetActive(false);
        // Disable the loading screen:
        m_currentLoadingOperation = null;
        m_bIsLoading = false;
        m_fTimeElapsed = 0.0f;
    }
}