using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeylineController : MonoBehaviour
{
    private bool m_bIsActive = false;
    private List<LeyNodeController> m_rConnectedNodes = null;

    // Start is called before the first frame update
    void Start()
    {
        // Get reference to zone parent
        Zone parent = transform.root.GetComponent<Zone>();
        if (parent) {
            // Register component
        }
    }

    /// <summary>
    /// Used to register a LeyNodeController with this Leyline. Ensures that the related node will be updated with this leyline. Called at runtime. 
    /// </summary>
    /// <param name="_Node">The node to be registered</param>
    public void RegisterNode(LeyNodeController _Node) {
        // Ensure the list exists
        if (m_rConnectedNodes == null) {
            m_rConnectedNodes = new List<LeyNodeController>();
        }

        // Add the node to this list
        m_rConnectedNodes.Add(_Node);
    }

    /// <summary>
    /// Checks if the leyline is active
    /// </summary>
    /// <returns></returns>
    public bool IsActive() {
        return m_bIsActive;
    }

    /// <summary>
    /// Sets the active state of the leyline
    /// </summary>
    /// <param name="_bOn"></param>
    public void SetActive(bool _bOn) {
        // Prevent repetition
        if(m_bIsActive == _bOn) {
            return;
        }

        // Update status
        m_bIsActive = _bOn;

        // Notify any registered nodes of the status change
        foreach(LeyNodeController node in m_rConnectedNodes) {
            node.CheckLeylineStatus();
        }
    }
    
}
