using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager
{
    //public static PlayerPrefsManager s_rInstance = null;

    //private void Awake()
    //{
    //    if (s_rInstance == null)
    //    {
    //        s_rInstance = this;
    //    }
    //    else if (s_rInstance != this)
    //    {
    //        Destroy(gameObject);
    //    }
    //    DontDestroyOnLoad(gameObject);
    //}

    //static public PlayerPrefsManager GetInstance()
    //{
    //    if(s_rInstance == null) {
    //        s_rInstance = new PlayerPrefsManager();
    //    }

    //    return s_rInstance;
    //}
    
    //PlayerPref Setters
    static public void StoreCamX(bool _camX)
    {
        PlayerPrefs.SetInt("CamX", (_camX ? 1 : 0));
    }
    static public void StoreCamY(bool _camY)
    {
        PlayerPrefs.SetInt("CamY", (_camY ? 1 : 0));
    }
    static public void StoreAudioBGM(float _bgmVal)
    {
        PlayerPrefs.SetFloat("AudioBGMVal", _bgmVal);
    }
    static public void StoreAudioVFX(float _vfxVal)
    {
        PlayerPrefs.SetFloat("AudioVFXVal", _vfxVal);
    }

    //PlayerPref Getters
    static public bool RetrieveCamX()
    {
        return (PlayerPrefs.GetInt("CamX", 0) == 1 ? true : false);
    }
    static public bool RetrieveCamY()
    {
        return (PlayerPrefs.GetInt("CamY", 1) == 1 ? true : false);
    }
    static public float RetrieveAudioBGM()
    {
        return PlayerPrefs.GetFloat("AudioBGMVal", 0.38f);
    }
    static public float RetrieveAudioVFX()
    {
        return PlayerPrefs.GetFloat("AudioVFXVal", 0.38f);
    }
}
