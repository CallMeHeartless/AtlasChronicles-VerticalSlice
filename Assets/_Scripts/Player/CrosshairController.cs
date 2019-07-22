using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    [SerializeField]
    private RaycastHit m_hit;
    [SerializeField]
    private float m_fRaycastRange = 50.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    //void Update()
    //{
    //    if(Physics.Raycast(transform.position, transform.forward, out m_hit)) {
    //        Vector3 position = transform.position;
    //        position.z = m_hit.point.z;
    //        transform.position = position;
    //        Debug.Log(transform.position);
    //    }
    //}
}
