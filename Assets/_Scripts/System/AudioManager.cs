using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer m_rMixer;
    private Slider m_rSlider;
    private PlayerPrefsManager m_rPrefs;

    public enum AudioType { NONE, BGM, SFX };
    public AudioType m_rType = AudioType.NONE;

    // Start is called before the first frame update
    public void Start()
    {
        m_rPrefs = PlayerPrefsManager.GetInstance();
        m_rSlider = GetComponent<Slider>();
        if (m_rType == AudioType.BGM)
        {
            m_rSlider.value = m_rPrefs.RetrieveAudioBGM();
            SetVol(m_rPrefs.RetrieveAudioBGM());
        }
        else if(m_rType == AudioType.SFX)
        {
            m_rSlider.value = m_rPrefs.RetrieveAudioVFX();
            SetVol(m_rPrefs.RetrieveAudioVFX());
        }
    }

    public void SetVol(float _sliderVal)
    {
        float audioVal = Mathf.Log10(_sliderVal) * 20;
        if (m_rType == AudioType.BGM)
        {
            m_rMixer.SetFloat("BGMVol", audioVal);
            m_rPrefs.StoreAudioBGM(_sliderVal);
        }
        else if (m_rType == AudioType.SFX)
        {
            m_rMixer.SetFloat("SFXVol", audioVal);
            m_rPrefs.StoreAudioVFX(_sliderVal);
        }
    }
}
