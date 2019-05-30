using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [System.Serializable]
    public class SoundBank
    {
        public string name;         //What type of audio this bank contains
        public AudioClip[] clips;   //Audio of this bank
    }

    [System.Serializable]
    public class MaterialAudioOverride
    {
        public Material[] materials; //What type of materials will be used to activate this soundbank
        public SoundBank[] banks;    //The bank of sounds to replace the default sounds with
    }

    private AudioSource m_Audiosource;

    [Header("Audio adjustment")]
    public bool m_bRandomizePitch = true;
    [Range(0.0f, 1.0f)]
    [Tooltip("0 - 1, The value for the sound effect in 2D to 3D space")]
    public float m_bSpatialValue = 0.0f;
    public float m_fPitchStart = 1.0f;
    public float m_fPitchRandomRange = 0.2f;
    public float m_fAudioDelay = 0.0f;
    //public AudioClip[] m_Audioclips;

    [Header("Audio Sources")]
    [Tooltip("List of default audio for this sound")]
    public SoundBank defaultAudio = new SoundBank();
    [Tooltip("List of different audios that will replace the default sounds if a material was interacted with")]
    public MaterialAudioOverride[] overrideAudio;
    private Dictionary<Material, SoundBank[]> m_Lookup = new Dictionary<Material, SoundBank[]>();

    private void Awake()
    {
        m_Audiosource = GetComponent<AudioSource>();
        for (int i = 0; i < overrideAudio.Length; i++) {
            foreach (var material in overrideAudio[i].materials)
                m_Lookup[material] = overrideAudio[i].banks;
        }
    }

    public void PlayAudio(Material _overrideMaterial)
    {
        //Plays a material specified audio based on which ID the user has selected
        PickRandomSound(_overrideMaterial);
    }

    public void PlayAudio(int _clipNum)
    {
        //Plays a material specified audio based on which ID the user has selected
        PickRandomSound(_clipNum);
    }

    // Start is called before the first frame update
    public void PlayAudio()
    {
        PickRandomSound(null);
    }

    public void PickRandomSound(Material _overrideMaterial)
    {
        var selectedBank = defaultAudio;

        //Check if user wants to override a sound via materials
        if (_overrideMaterial != null) {
            //Get bank that contains the material specified
            if (m_Lookup.TryGetValue(_overrideMaterial, out SoundBank[] banks)) {
                selectedBank = banks[0];
            }
        }

        //If there is no clip to override with, cancel function
        if (selectedBank.clips == null || selectedBank.clips.Length == 0)
            return;

        //Select a random clip from the audio bank
        AudioClip clip = selectedBank.clips[Random.Range(0, selectedBank.clips.Length)];

        //If the clip does not exist, cancel function
        if (clip == null)
            return;

        //Apply user-specified settings to the selected sound clip
        m_Audiosource.spatialBlend = m_bSpatialValue;
        m_Audiosource.pitch = m_bRandomizePitch ? Random.Range(m_fPitchStart, m_fPitchStart + m_fPitchRandomRange) : 1.0f;
        m_Audiosource.clip = clip;
        m_Audiosource.PlayDelayed(m_fAudioDelay); //Plays audio
    }

    public void PickRandomSound(int _clipNum)
    {
        var selectedBank = defaultAudio;

        AudioClip clip = selectedBank.clips[_clipNum];

        //If the clip does not exist, cancel function
        if (clip == null)
            return;

        //Apply user-specified settings to the selected sound clip
        m_Audiosource.spatialBlend = m_bSpatialValue;
        m_Audiosource.pitch = m_bRandomizePitch ? Random.Range(m_fPitchStart, m_fPitchStart + m_fPitchRandomRange) : 1.0f;
        m_Audiosource.clip = clip;
        m_Audiosource.PlayDelayed(m_fAudioDelay); //Plays audio
    }
}
