using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using cakeslice;

//[RequireComponent(typeof(Outline))]
public class MapVisionComponent : MonoBehaviour
{
    //private Outline m_rOutline;

    private void Start() {
        // Obtain reference to outline component
        //m_rOutline = GetComponent<Outline>();
        //m_rOutline.enabled = false; // Start as disabled

        // Register this component with its parent zone. NOTE: All objects with this component should be children 
        Zone rParent = transform.root.GetComponent<Zone>();
        if (rParent) {
            rParent.AddToMapVisionList(gameObject);
        }
    }

    // Toggles the outline component
    public void ToggleMapVision(bool _bOn) {
        //m_rOutline.enabled = _bOn;
    }

    // OnDestroy is called here to ensure that there are no null references left in the parent Zone's list of map vision components
    public void OnDestroy() {
        Zone rParent = transform.root.GetComponent<Zone>();
        if (rParent) { // Null reference check
            rParent.RemoveFromMapVisionList(gameObject);
        }
    }
}
