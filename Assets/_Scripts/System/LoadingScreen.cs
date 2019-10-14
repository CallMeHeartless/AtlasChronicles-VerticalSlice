using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    private const float MIN_WAIT_TIME = 2.0f;

    [SerializeField] private GameObject m_rLoadingPanel;
    public static LoadingScreen Instance;
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

            m_animator = GetComponentInChildren<Animator>();
            m_canvas = GetComponent<Canvas>();
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
            if (m_currentLoadingOperation.isDone && !m_bAnimationHasTriggered)
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

    public void ActivateLoadingScreen(AsyncOperation _operation)
    {
        //if(SceneManager.GetActiveScene().buildIndex == 0)
        //{
        //    m_canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        //}
        //else
        //{
        //    m_canvas.renderMode = RenderMode.ScreenSpaceCamera;
        //}
        m_currentLoadingOperation = _operation;
        m_rLoadingPanel.SetActive(true);
        _operation.allowSceneActivation = false;
        m_fTimeElapsed = 0.0f;
        m_bIsLoading = true;

        //// Play the fade in animation:          
        m_animator.SetTrigger("Show");
    }

    public void HideLoadingScreen()
    {
        // Disable the loading screen:
        m_rLoadingPanel.SetActive(false);
        m_currentLoadingOperation = null;
        m_bIsLoading = false;
        m_fTimeElapsed = 0.0f;
    }
}