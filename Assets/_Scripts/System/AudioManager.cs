using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer m_rMixer;
    private Slider m_rSlider;

    public enum AudioType { eNONE, eBGM, eSFX };
    public AudioType m_rType = AudioType.eNONE;

    // Start is called before the first frame update
    public void Start()
    {
        //Get the slider of the component this script is currently on
        m_rSlider = GetComponent<Slider>();

        //Check which type the user has set the audio type to be
        if (m_rType == AudioType.eBGM)
        {
            //Set slider value and volume to whatever value is stored in player prefs
            m_rSlider.value = PlayerPrefsManager.RetrieveAudioBGM();

            //Set the current BGM Volume on the AudioMixer
            SetVol(PlayerPrefsManager.RetrieveAudioBGM());
        }
        else if(m_rType == AudioType.eSFX)
        {
            //Set slider value and volume to whatever value is stored in player prefs
            m_rSlider.value = PlayerPrefsManager.RetrieveAudioVFX();

            //Set the current SFX Volume on the AudioMixer
            SetVol(PlayerPrefsManager.RetrieveAudioVFX());
        }
    }

    public void SetVol(float _sliderVal)
    {
        // Convert slider value to be compatible with the AudioMixer values
        float audioVal = Mathf.Log10(_sliderVal) * 20;
        if (m_rType == AudioType.eBGM)
        {
            //Set the BGM volume value to whatever the player has selected
            m_rMixer.SetFloat("BGMVol", audioVal);
            //Store the un-converted slider value in player prefs
            PlayerPrefsManager.StoreAudioBGM(_sliderVal);
        }
        else if (m_rType == AudioType.eSFX)
        {
            //Set the SFX volume value to whatever the player has selected
            m_rMixer.SetFloat("SFXVol", audioVal);
            //Store the un-converted slider value in player prefs
            PlayerPrefsManager.StoreAudioVFX(_sliderVal);
        }
    }
}
