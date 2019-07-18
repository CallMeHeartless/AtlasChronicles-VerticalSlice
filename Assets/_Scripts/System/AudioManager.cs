using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public enum AudioType { BGM, SFX };

    [SerializeField] private AudioMixer m_rMixer;
    public AudioType m_rType;

    private Slider m_rSlider;

    // Start is called before the first frame update
    public void Start()
    {
        m_rSlider = GetComponent<Slider>();
        if (m_rType == AudioType.BGM)
        {
            m_rSlider.value = 0.4f;
            SetVol(0.4f);
        }
        else
        {
            m_rSlider.value = 1.0f;
            SetVol(1.0f);
        }
    }

    public void SetVol(float _sliderVal)
    {
        print(m_rType);
        float audioVal = Mathf.Log10(_sliderVal) * 20;
        if (m_rType == AudioType.BGM)
        {
            m_rMixer.SetFloat("BGMVol", audioVal);
            PlayerPrefsManager.GetInstance().StoreAudioBGM(_sliderVal);
        }
        else
        {
            m_rMixer.SetFloat("SFXVol", audioVal);
            PlayerPrefsManager.GetInstance().StoreAudioVFX(_sliderVal);
        }
    }
}
