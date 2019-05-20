using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public enum AudioType { BGM, SFX };

    public static AudioManager instance;
    [SerializeField] private AudioMixer m_mixer;
    [SerializeField] private AudioType m_type;
    [SerializeField] [Range(0.0001f, 1.0f)] private float m_defaultVal = 1.0f;

    Slider m_slider;

    // Start is called before the first frame update
    public void Start()
    {
        m_slider = GetComponent<Slider>();
        m_slider.value = m_defaultVal;
        SetVol(m_slider.value);
    }

    public void SetVol(float _sliderVal)
    {
        if (m_type == AudioType.BGM)
        {
            m_mixer.SetFloat("BGMVol", Mathf.Log10(_sliderVal) * 20);
        }
        else
        {
            m_mixer.SetFloat("SFXVol", Mathf.Log10(_sliderVal) * 20);
        }
    }
}
