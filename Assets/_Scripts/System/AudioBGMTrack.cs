using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBGMTrack : MonoBehaviour
{
    public LayerMask layers;
    AudioBGM soundTrack;

    void OnEnable()
    {
        soundTrack = GetComponentInParent<AudioBGM>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (0 != (layers.value & 1 << other.gameObject.layer))
        {
            soundTrack.PushTrack(this.name);
        }
    }

    void OnTriggerExit(Collider other)
    {
        //If player exits area audio
        if (0 != (layers.value & 1 << other.gameObject.layer))
        {
            soundTrack.PopTrack();
        }
    }
}
