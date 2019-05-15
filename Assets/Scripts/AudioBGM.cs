using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBGM : MonoBehaviour
{
    public float soundTrackVolume = 1;
    public float initialVolume = 1;
    public float volumeRampSpeed = 4;
    public bool playOnStart = true;
    public AudioSource[] audioSources;

    AudioSource activeAudio, fadeAudio;
    float volumeVelocity, fadeVelocity;
    float volume;

    public void Play()
    {
        if (activeAudio != null)
            activeAudio.Play();
    }

    public void Stop()
    {
        foreach (var i in audioSources) i.Stop();
    }

    public void SetVolume(float volume)
    {
        this.volume = volume;
    }

    void Update()
    {
        if (activeAudio != null)
            activeAudio.volume = Mathf.SmoothDamp(activeAudio.volume, volume * soundTrackVolume, ref volumeVelocity, volumeRampSpeed, 1);

        if (fadeAudio != null)
        {
            fadeAudio.volume = Mathf.SmoothDamp(fadeAudio.volume, 0, ref fadeVelocity, volumeRampSpeed, 1);
            if (Mathf.Approximately(fadeAudio.volume, 0))
            {
                fadeAudio.Stop();
                fadeAudio = null;
            }
        }
    }
}
