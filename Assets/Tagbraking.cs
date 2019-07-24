using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tagbraking : MonoBehaviour
{
    private int m_TagLoctation;
    private PlayerController m_fPlayerController;
    private void Start()
    {
        m_fPlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            //something is on the tag so deactivate it
            m_fPlayerController.SetTeleportCondiction(m_TagLoctation);
            gameObject.SetActive(false);
        }
    }
    public void SetTag(int Tag){
        m_TagLoctation = Tag;
    }
}
