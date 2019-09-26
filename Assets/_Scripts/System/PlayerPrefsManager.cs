using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager
{
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

    static public void StoreAudioDialogue(float _dialogueVal)
    {
        PlayerPrefs.SetFloat("AudioDialogueVal", _dialogueVal);
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

    static public float RetrieveAudioDialogue()
    {
        return PlayerPrefs.GetFloat("AudioDialogueVal", 0.38f);
    }
}
