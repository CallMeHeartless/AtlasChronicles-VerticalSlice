using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newPostion : MonoBehaviour
{
    float y;
    Vector3 newPos;
    public GameObject Zone;
    // Start is called before the first frame update
    void Start()
    {
        y = transform.parent.position.y;
        
    }

    // Update is called once per frame
    IEnumerator Fade()
    {
        yield return new WaitForSeconds(Random.Range(0.5f,3));
        SetNewPosition();

    }

    public void SetNewPosition()
    {
        newPos = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10))+ Zone.transform.position;
        Debug.Log(newPos);
        if (Vector3.Distance(gameObject.transform.position, newPos) < 1)
            {
                SetNewPosition();
            }
        transform.position = newPos;
      
    }
}
