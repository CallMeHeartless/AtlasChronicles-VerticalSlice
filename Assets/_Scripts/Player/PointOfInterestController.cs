using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineFreeLook))]
public class PointOfInterestController : MonoBehaviour
{
    // References
    private CinemachineFreeLook m_rCamera;
    private GameObject m_rPlayer;
    private GameObject m_rPortal;
    private CinemachineCollider m_rCameraCollider;

    void Start(){
        // Initialise references
        m_rCamera = GetComponent<CinemachineFreeLook>();
        m_rCameraCollider = GetComponent<CinemachineCollider>();
        m_rPlayer = GameObject.Find("Player");
        m_rPortal = GameObject.Find("GameEndPortal");
    }

    // Returns the nearest map fragment, or null otherwise. 
    private GameObject FindNearestMapFragment() {
        // Obtain references to all map fragments in scene
        GameObject[] maps = GameObject.FindGameObjectsWithTag("PrimaryPickup");

        // Assessment variables
        Vector3 playerPosition = m_rPlayer.transform.position; // Player position
        float fMinDistance = 100000.0f; // The current minimum distance
        GameObject nearest = null; // The nearest map fragment

        // Iterate through maps and find which is closest
        foreach(GameObject map in maps) {
            // Get square distance to player
            float fSquareDistanceToPlayer = (map.transform.position - playerPosition).sqrMagnitude;

            // Compare to current min distance
            if(fSquareDistanceToPlayer < fMinDistance) {
                // Current map is the nearest object
                fMinDistance = fSquareDistanceToPlayer;
                nearest = map;
            }
        }

        return nearest;
    }

    // Look at the nearest map fragment, or the end portal
    public void SetPointOfInterest(bool _bActive) {
        // Toggle the camera collider component
        m_rCameraCollider.enabled = !_bActive;

        // Look at the nearest map fragment / end portal
        if (_bActive) {
            // Find the nearest map fragment
            GameObject nearestMap = FindNearestMapFragment();

            // Determine where to look
            if (nearestMap) { // At the nearest map fragment
                SetCameraTarget(nearestMap.transform);
            } else { // At the end portal
                SetCameraTarget(m_rPortal.transform);
            }
        }
        // Otherwise look back at the player
        else {
            SetCameraTarget(m_rPlayer.transform);
        }

    }

    // Set what the camera is looking at
    private void SetCameraTarget(Transform _target) {
        m_rCamera.m_LookAt = _target;
    }

}
