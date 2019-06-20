using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class endLevel : MonoBehaviour
{
    
    //exist potal
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(0);
    }
}
