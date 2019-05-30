using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    protected AudioSource[] m_Sounds;
    
    // Start is called before the first frame update
    public virtual void Start(){
        m_Sounds = GetComponents<AudioSource>();    
    }
    
    // Plays a single sound
    public void PlaySingleSound(string sound) {
        AudioSource audio = GetSource(sound);
        if (audio) {
            if (!audio.isPlaying) {// Stop looping sounds from starting up again
                audio.Play();
                //Debug.Log("Playing: " + sound);
            }
            else {
                //Debug.Log("Already playing sound");
            }
        }
    }

    // Stops a sound (assumed to be looping)
    public void StopSingleSound(string sound) {
        AudioSource audio = GetSource(sound);
        if (audio) {
            if (audio.isPlaying) {
                audio.Stop();
            }
        }
        else {
           // Debug.Log("Sound not found - null reference.");
        }
    }

    // Internal helper function
    protected AudioSource GetSource(string sound) {
        foreach (AudioSource audio in m_Sounds) {
            if (audio.clip.name == sound) {
                return audio;
            }
        }

        return null;
    }

}
