﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using Cinemachine;

public class PlayerPrefsManager : MonoBehaviour
{
    public static PlayerPrefsManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    static public PlayerPrefsManager GetInstance()
    {
        return instance;
    }
    
    //PlayerPref Setters
    public void StoreCamX(bool _camX)
    {
        PlayerPrefs.SetInt("CamX", (_camX ? 1 : 0));
    }
    public void StoreCamY(bool _camY)
    {
        PlayerPrefs.SetInt("CamY", (_camY ? 1 : 0));
    }
    public void StoreAudioBGM(float _bgmVal)
    {
        PlayerPrefs.SetFloat("AudioBGMVal", _bgmVal);
    }
    public void StoreAudioVFX(float _vfxVal)
    {
        PlayerPrefs.SetFloat("AudioVFXVal", _vfxVal);
    }

    //PlayerPref Getters
    public bool RetrieveCamX()
    {
        return (PlayerPrefs.GetInt("CamX", 0) == 1 ? true : false);
    }
    public bool RetrieveCamY()
    {
        return (PlayerPrefs.GetInt("CamY", 1) == 1 ? true : false);
    }
    public float RetrieveAudioBGM()
    {
        return PlayerPrefs.GetFloat("AudioBGMVal", 0.38f);
    }
    public float RetrieveAudioVFX()
    {
        return PlayerPrefs.GetFloat("AudioVFXVal", 0.38f);
    }
}
