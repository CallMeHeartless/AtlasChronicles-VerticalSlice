using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    [SerializeField]
    private Camera m_rCamera;
    private RaycastHit m_hit;
    private Vector3 m_MidPoint = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        m_rCamera = GetComponent<Camera>();
        m_MidPoint.x = m_rCamera.scaledPixelWidth / 2.0f;
        m_MidPoint.y = m_rCamera.scaledPixelHeight / 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = m_rCamera.ScreenPointToRay(m_MidPoint);
        Debug.DrawRay(ray.origin, ray.direction * 50.0f, Color.yellow);
    }
}
