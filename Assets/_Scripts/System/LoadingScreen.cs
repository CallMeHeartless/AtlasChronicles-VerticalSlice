using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    private const float MIN_WAIT_TIME = 2.0f;

    public static LoadingScreen Instance;

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
        if (Instance == null)
        {
            Instance = this;
            AssignCurrentSceneCanvas();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
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
        if(m_canvas.worldCamera == null)
        {
            m_canvas.worldCamera = GameObject.Find("Camera").GetComponent<Camera>();
        }
    }

    public void ActivateLoadingScreen(AsyncOperation _operation)
    {
        AssignCurrentSceneCanvas();

        m_currentLoadingOperation = _operation;
        m_rCurrentLoadingPanel.SetActive(true);
        _operation.allowSceneActivation = false;
        m_fTimeElapsed = 0.0f;
        m_bIsLoading = true;

        //// Play the fade in animation:          
        m_animator.SetTrigger("Show");
    }

    public void HideLoadingScreen()
    {
        m_rCurrentLoadingPanel.SetActive(false);

        // Disable the loading screen:
        //m_rLoadingPanel.SetActive(false);
        m_currentLoadingOperation = null;
        m_bIsLoading = false;
        m_fTimeElapsed = 0.0f;
    }
}