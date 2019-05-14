using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private AudioSource m_Audiosource;
    public bool m_bRandomizePitch = true;
    public float m_fPitchRandomRange = 0.2f;
    public float m_fAudioDelay = 0.0f;
    public AudioClip[] m_Audioclips;

    private void Awake()
    {
        m_Audiosource = GetComponent<AudioSource>();
    }


    // Start is called before the first frame update
    public void PlayAudio()
    {
        if (m_Audioclips == null || m_Audioclips.Length == 0)
            return;

        AudioClip clip = m_Audioclips[Random.Range(0, m_Audioclips.Length)];

        if (clip == null)
            return;

        m_Audiosource.pitch = m_bRandomizePitch ? Random.Range(1.0f - m_fPitchRandomRange, 1.0f + m_fPitchRandomRange) : 1.0f;
        m_Audiosource.clip = clip;
        m_Audiosource.PlayDelayed(m_fAudioDelay);
    }
}
