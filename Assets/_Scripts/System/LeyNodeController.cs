using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeyNodeController : MonoBehaviour
{
    // Internal variables
    [SerializeField][Tooltip("All of the leylines that must be activated for this node to be activated")]
    private List<LeylineController> m_rLeylines;
    [SerializeField]
    private ParticleSystem m_rActiveParticles = null;
    private bool m_bIsActive = false;

    [Header("Events")]
    public UnityEvent OnActivate;
    public UnityEvent OnDisable;
    

    // Start is called before the first frame update
    void Start()
    {
        // Register this node with all connected leylines
        foreach(LeylineController leyline in m_rLeylines) {
            leyline.RegisterNode(this);
        }
    }

    // Check if the Node is active
    public bool IsActive() {
        return m_bIsActive;
    }

    /// <summary>
    /// Iterates through leyline references and checks to see if they are all on - updating its own status if needed
    /// </summary>
    public void CheckLeylineStatus() {
        // Iterate through a check for a reason to break
        foreach(LeylineController leyline in m_rLeylines) {
            if (!leyline.IsActive()) {
                // Disable (if needed)
                ActivateOrDisable(false);
                return;
            }
        }

        // Activate (if needed)
        ActivateOrDisable(true);
    }

    /// <summary>
    /// Handles the node become active or disables
    /// </summary>
    /// <param name="_bOn"></param>
    private void ActivateOrDisable(bool _bOn) {
        // Prevent repetition
        if(m_bIsActive == _bOn) {
            return;
        }

        // Update activation flag
        m_bIsActive = _bOn;

        // Handle activation
        if (_bOn) {
            // Invoke activation events
            OnActivate.Invoke();

            // Enable any vfx
            if (m_rActiveParticles) {
                m_rActiveParticles.Play();
            }
        } 
        // Handle disabling
        else {
            // Invoke disable events
            OnDisable.Invoke();

            // Disable any vfx
            if (m_rActiveParticles) {
                m_rActiveParticles.Stop();
            }
        }
    }
    
}
