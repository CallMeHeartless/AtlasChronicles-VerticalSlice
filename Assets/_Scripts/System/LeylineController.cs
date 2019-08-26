using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MessageSystem;
using SplineMesh;

public class LeylineController : MonoBehaviour, IMessageReceiver
{
    private bool m_bIsActive = false;
    private List<LeyNodeController> m_rConnectedNodes = null;
    private SplineMeshTiling m_rSplineMesh;
    private MeshRenderer m_rMeshRenderer;
    [SerializeField]
    private Material m_InactiveMaterial;
    [SerializeField]
    private Material m_ActiveMaterial;

    // Start is called before the first frame update
    void Start()
    {
        // Get reference to zone parent
        Zone parent = transform.root.GetComponent<Zone>();
        if (parent) {
            // Register component
            parent.AddToLeylineList(this);

            // Deactivate self
            gameObject.SetActive(false);
        } else {
            Debug.LogError("ERROR: Leyline " + name + " is not a child of a zone.");
        }

        // Get spline mesh component
        m_rSplineMesh = GetComponent<SplineMeshTiling>();
        m_rMeshRenderer = GetComponentInChildren<MeshRenderer>();
        if (!m_rMeshRenderer) {
            Debug.Log("No mesh renderer");
        }
    }

    private void Update() {
        // DEBUG
        if (Input.GetKeyDown(KeyCode.T)) {
            ActivateLeyline(true);
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
    public void ActivateLeyline(bool _bOn) {
        // Prevent repetition
        if(m_bIsActive == _bOn) {
            return;
        }

        // Update status
        m_bIsActive = _bOn;

        // Change material
        if(_bOn && m_ActiveMaterial) { // Set active material
            //m_rSplineMesh.material = m_ActiveMaterial;
            m_rMeshRenderer.material = m_ActiveMaterial;
        }
        else if(!_bOn && m_InactiveMaterial) { // Set inactive material
            //m_rSplineMesh.material = m_InactiveMaterial;
            m_rMeshRenderer.material = m_InactiveMaterial;
        }

        // Notify any registered nodes of the status change
        foreach(LeyNodeController node in m_rConnectedNodes) {
            node.CheckLeylineStatus();
        }
    }
    
    /// <summary>
    /// Handle message events
    /// </summary>
    /// <param name="_messageType"></param>
    /// <param name="_message"></param>
    public void OnReceiveMessage(MessageSystem.MessageType _messageType, object _message) {
        switch (_messageType) {
            // Handle an activation message
            case MessageType.eActivate: {
                ActivateLeyline(true);
                break;
            }

            // Handle disable messages
            case MessageType.eReset: {
                ActivateLeyline(false);
                break;
            }

            default:break;
        }
    }
}
