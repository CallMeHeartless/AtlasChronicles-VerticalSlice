using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformAttachment : MonoBehaviour
{
    int Child;
   public void Attach(GameObject tag)
    {
        Child = transform.childCount;
        tag.transform.parent = transform;
    }
    public void Deattach(GameObject tag)
    {
        transform.GetChild(Child).transform.parent = null;
    }
}
