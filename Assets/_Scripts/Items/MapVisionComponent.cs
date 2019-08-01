using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

[RequireComponent(typeof(Outline))]
public class MapVisionComponent : MonoBehaviour
{
    private Outline m_rOutline;

    private void Start() {
        // Obtain reference to outline component
        m_rOutline = GetComponent<Outline>();
        m_rOutline.enabled = false; // Start as disabled

        // Register this component with its parent zone. NOTE: All objects with this component should be children 
        Zone rParent = transform.root.GetComponent<Zone>();
        if (rParent) {
            rParent.AddToMapVisionList(this);
        }
    }

    // Toggles the state of the 
    public void ToggleMapVision(bool _bOn) {
        m_rOutline.enabled = _bOn;
    }
}
