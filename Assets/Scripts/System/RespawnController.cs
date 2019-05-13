using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
    [SerializeField]
    private Transform m_rRespawnPoint;
    private GameObject m_rPlayer;

    // Start is called before the first frame update
    void Start()
    {
        m_rPlayer = GameObject.FindGameObjectWithTag("Player");   
    }

    // Resets the player's position to that of the most recent checkpoint
    public void RespawnPlayer()
    {

        //m_rPlayer.transform.position = m_rRespawnPoint.position; // This will need to account for animation etc. later
        StartCoroutine(MovePlayer());
    }

    public void SetRespawnPoint(Transform _rNewSpawnPoint) {
        if (_rNewSpawnPoint) {
            m_rRespawnPoint = _rNewSpawnPoint;
        }
    }
    private IEnumerator MovePlayer() {
        yield return new WaitForEndOfFrame();
        m_rPlayer.transform.position = m_rRespawnPoint.position;
    }
}
