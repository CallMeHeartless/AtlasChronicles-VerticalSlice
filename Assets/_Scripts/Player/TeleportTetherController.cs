using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTetherController : MonoBehaviour
{
    private LineRenderer m_LineRenderer;

    // Start is called before the first frame update
    void Start(){
        // Obtain references
        m_LineRenderer = GetComponent<LineRenderer>();    
    }

    // Update is called once per frame
    void Update(){
        
    }

    /// <summary>
    /// Set the other end of of the teleport tether
    /// </summary>
    /// <param name="_position"></param>
    public void SetTetherEnd(Vector3 _position) {
        m_LineRenderer.SetPosition(1, _position);
    }
}
