using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapVisionComponent : MonoBehaviour
{

    [SerializeField]
    private Material m_BaseMaterial;
    [SerializeField]
    private Material m_MapVisionMaterial;
    [SerializeField]
    private Renderer m_rMaterialReference;

    private void Start() {
        if (!m_rMaterialReference) {
            m_rMaterialReference = GetComponent<Renderer>();
        }

        // Register this component with its parent zone. NOTE: All objects with this component should be children 
        Zone rParent = transform.root.GetComponent<Zone>();
        if (rParent) {
            rParent.AddToMapVisionList(this);
        }
    }

    public void MapVisionOn() {
        m_rMaterialReference.material = m_MapVisionMaterial;
    }

    public void MapVisionOff() {
        m_rMaterialReference.material = m_BaseMaterial;
    }

    public void ToggleMapVision(bool _bOn) {
        if (_bOn) {
            m_rMaterialReference.material = m_MapVisionMaterial;
        } else {
            m_rMaterialReference.material = m_BaseMaterial;
        }
    }
}
