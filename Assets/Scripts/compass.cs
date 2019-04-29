using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class compass : MonoBehaviour
{
    public RawImage image;
    public Transform player;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        image.uvRect = new Rect(player.localEulerAngles.y / 720f, 0, 1, 1);
        //north is the center point
    }
}
