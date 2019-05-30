using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformHandler : MonoBehaviour
{
    public enum LoopType
    {
        Once,
        PingPong,
        Repeat
    }

    public LoopType loopType;
    public float duration = 1;
    public AnimationCurve accelCurve;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
