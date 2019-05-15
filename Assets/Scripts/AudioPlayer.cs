using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private AudioSource m_Audiosource;
    public bool m_bRandomizePitch = true;
    [Range(0.0f, 1.0f)]
    [Tooltip("0 - 1, The value for the sound effect in 2D to 3D space")]
    [SerializeField] private float m_bSpatialValue = 0.0f;
    [SerializeField] private float m_fPitchStart = 1.0f;
    [SerializeField] private float m_fPitchRandomRange = 0.2f;
    [SerializeField] private float m_fAudioDelay = 0.0f;
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
        m_Audiosource.spatialBlend = m_bSpatialValue;
        m_Audiosource.pitch = m_bRandomizePitch ? Random.Range(m_fPitchStart, m_fPitchStart + m_fPitchRandomRange) : 1.0f;
        m_Audiosource.clip = clip;
        m_Audiosource.PlayDelayed(m_fAudioDelay);
    }
}
