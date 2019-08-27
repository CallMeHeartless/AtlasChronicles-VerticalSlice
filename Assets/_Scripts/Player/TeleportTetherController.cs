using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTetherController : MonoBehaviour
{
    private LineRenderer m_LineRenderer;
    private Vector3 m_TetherPoint = Vector3.zero;

    // Start is called before the first frame update
    void Start(){
        // Obtain references
        m_LineRenderer = GetComponent<LineRenderer>();
        if (!m_LineRenderer) {
            Debug.Log("No Line Renderer attached");
        }
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update(){
        if (m_LineRenderer) {
            // Set the positions in world space
            m_LineRenderer.SetPosition(0, transform.position);
            m_LineRenderer.SetPosition(1, m_TetherPoint);
        }
    }

    /// <summary>
    /// Set the other end of of the teleport tether
    /// </summary>
    /// <param name="_position"></param>
    public void SetTetherEnd(Vector3 _position) {
        if (!m_LineRenderer) {
            Debug.LogError("ERROR: No line renderer.");
            return;
        }
        Debug.Log("Tether set");
        m_LineRenderer.SetPosition(1, _position);
        m_TetherPoint = _position;
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Disables the player tether effect
    /// </summary>
    public void BreakTether() {
        gameObject.SetActive(false);
    }
}
