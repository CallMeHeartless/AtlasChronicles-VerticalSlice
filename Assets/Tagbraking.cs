using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tagbraking : MonoBehaviour
{
    public int tagLoctation;
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Collision");
        if (!other.CompareTag("Player"))
        {
            Debug.Log("tagLoctation " + tagLoctation);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().setm_rTeleportCondiction(tagLoctation);
            gameObject.SetActive(false);
        }
    }
}
