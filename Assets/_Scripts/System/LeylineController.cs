using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MessageSystem;
using SplineMesh;

public class LeylineController : MonoBehaviour, IMessageReceiver
{
    private bool m_bIsActive = false;
    [SerializeField][Tooltip("If True, this leyline will automatically activate when the map fragment is found")]
    private bool m_bAutoActivate = false;
    private List<LeyNodeController> m_rConnectedNodes = null;
    private SplineMeshTiling m_rSplineMesh;
    private MeshRenderer[] m_rMeshRenderer = null;
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

            // Get spline mesh component
            m_rSplineMesh = GetComponent<SplineMeshTiling>();
            m_rMeshRenderer = GetComponentsInChildren<MeshRenderer>();
            if (m_rMeshRenderer == null) {
                Debug.LogError("ERROR: LeylineController could not find mesh renderer in children (m_rMeshRenderer is null).");
            }

            // Deactivate self
            gameObject.SetActive(false);
        } else {
            Debug.LogError("ERROR: Leyline " + name + " is not a child of a zone.");
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
        if(m_bIsActive == _bOn || !gameObject.activeSelf) {
            return;
        }

        // Update status
        m_bIsActive = _bOn;
        gameObject.SetActive(true);

        // Change material // NEEDS better VFX
        if(_bOn && m_ActiveMaterial) { // Set active material
            //m_rMeshRenderer.material = m_ActiveMaterial;
            if(m_rMeshRenderer != null) {
                foreach (MeshRenderer mesh in m_rMeshRenderer) {
                    mesh.material = m_ActiveMaterial;
                }
            } else {
                Debug.LogError("ERROR: LeylineController could not apply materials on activation (m_rMeshRenderer is null).");
            }

        }
        else if(!_bOn && m_InactiveMaterial) { // Set inactive material
            //m_rSplineMesh.material = m_InactiveMaterial;
            //m_rMeshRenderer.material = m_InactiveMaterial;
            foreach (MeshRenderer mesh in m_rMeshRenderer) {
                mesh.material = m_InactiveMaterial;
            }
        }

        // Notify any registered nodes of the status change
        if(m_rConnectedNodes != null) {
            foreach (LeyNodeController node in m_rConnectedNodes) {
                node.CheckLeylineStatus();
            }
        }

    }

    /// <summary>
    /// Checks if the leyline should be activated automatically, called when the map is collected
    /// </summary>
    public void CheckForAutoActivation() {
        if (m_bAutoActivate) {
            ActivateLeyline(true);
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

            case MessageType.eOn: {
                ActivateLeyline(true);
                break;
            }
            case MessageType.eActivate: {
                ActivateLeyline(true);
                break;
            }

            // Handle disable messages
            case MessageType.eReset: {
                ActivateLeyline(false);
                break;
            }

            case MessageType.eOff: {
                ActivateLeyline(false);
                break;
            }

            default:break;
        }
    }
}
