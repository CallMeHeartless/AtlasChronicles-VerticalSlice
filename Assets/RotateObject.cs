using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float m_fSpeed;
    public bool stops;
    public float[] stoppingPoints;
    public float Range;
    public int CurrentPoint = 0;
    public Vector3 point;
    bool Pause = false;
    private float currentTimer;
    public float MaxTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (stops == true)
        {
            Debug.Log(Mathf.Rad2Deg *transform.rotation.x*4.5);
            if ((stoppingPoints[CurrentPoint] <=(Mathf.Rad2Deg * transform.rotation.x*4.5)+ Range) && (stoppingPoints[CurrentPoint] >= (Mathf.Rad2Deg *4.5f* transform.rotation.x) - Range))
            {
                transform.rotation = Quaternion.Euler(new Vector3(stoppingPoints[CurrentPoint], point.y, point.z));
                Pause = true;
                currentTimer = MaxTimer;
                if (CurrentPoint == stoppingPoints.Length-1)
                {
                    CurrentPoint = 0;
                }
                else
                {
                    CurrentPoint++;
                }
            }

            if (Pause == true)
            {
                if (currentTimer <= 0)
                {
                    Pause = false;
                    transform.Rotate(Vector3.up, m_fSpeed);
                }
                else
                {
                    currentTimer -= Time.deltaTime;
                }
            }
            else
            {
                transform.Rotate(Vector3.up, m_fSpeed);
            }

        }
        else
        {
            transform.Rotate(Vector3.up, m_fSpeed);
        }

    }
}
