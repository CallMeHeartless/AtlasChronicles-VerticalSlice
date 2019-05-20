using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBGM : MonoBehaviour
{
    public float soundTrackVolume = 1;
    public float initialVolume = 1;
    public float volumeRampSpeed = 1;
    public bool playOnStart = true;
    public AudioSource[] audioSources;

    AudioSource activeAudio;
    List<AudioSource> m_fadeAudio = new List<AudioSource>();

    float volumeVelocity, fadeVelocity;
    float volume;
    Stack<string> trackStack = new Stack<string>();

    public void PushTrack(string name)
    {
        trackStack.Push(name);
        //print("Pushing: " + name);
        Enqueue(name);
    }

    public void PopTrack()
    {
        if (trackStack.Count > 1)
            trackStack.Pop();
        //print("Popping: " + trackStack.Peek());

        Enqueue(trackStack.Peek());
    }
    public void Enqueue(string name)
    {
        foreach (var i in audioSources)
        {
            if (i.name == name)
            {
                //Fade audio that is currently playing
                //print("Setting fade: " + activeAudio.name);

                m_fadeAudio.Add(activeAudio);
                //Set audio that has just been queued as the active audio
                activeAudio = i;
                //Play audio if not already playing
                if (!activeAudio.isPlaying) activeAudio.Play();
                break;
            }
        }
    }

    public void Play()
    {
        if (activeAudio != null)
            activeAudio.Play();
    }

    public void Stop()
    {
        foreach (var i in audioSources) i.Stop();
    }

    void OnEnable()
    {
        //Clear stack
        Reset();
        m_fadeAudio.Clear();
        trackStack.Clear();
        if (audioSources.Length > 0)
        {
            //Set the first audio as the current active audio
            activeAudio = audioSources[0];
            //Set all audio volumes to 0
            foreach (var i in audioSources) i.volume = 0;
            //Add all audio names to the stack
            trackStack.Push(audioSources[0].name);
            //Play current active audio
            if (playOnStart) Play();
        }
        volume = initialVolume;
    }

    void Reset()
    {
        //Reset by replacing the current audio array with the gameobjects children
        audioSources = GetComponentsInChildren<AudioSource>();
    }

    public void SetVolume(float volume)
    {
        this.volume = volume;
    }

    void Update()
    {
        if (activeAudio != null)
        {
            if(activeAudio.volume != 1)
            {
                activeAudio.volume = Mathf.SmoothDamp(activeAudio.volume, 1, ref volumeVelocity, volumeRampSpeed, 20);
            }
        }

        for (int track = 0; track < m_fadeAudio.Count; ++track)
        {
            if(m_fadeAudio[track] != null)
            {
                if(activeAudio == m_fadeAudio[track])
                {
                    m_fadeAudio.RemoveAt(track);
                    break;
                }
                m_fadeAudio[track].volume = Mathf.SmoothDamp(m_fadeAudio[track].volume, 0, ref fadeVelocity, volumeRampSpeed, 10);

                if (Mathf.Approximately(m_fadeAudio[track].volume, 0))
                {
                    m_fadeAudio[track].volume = 0;
                    m_fadeAudio[track].Stop();
                    m_fadeAudio.RemoveAt(track);
                }
            }
        }
    }
}
