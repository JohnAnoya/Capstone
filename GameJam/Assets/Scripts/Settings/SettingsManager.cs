using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    //UI references
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    public Toggle enableFpsCounter;
    public Toggle enableFullscreen;

    //Player pref keys
    private const string MUSIC_VOLUME_PREF = "MusicVolumePref";
    private const string SFX_VOLUME_PREF = "SfxVolumePref";
    
    private const string FPS_DISPLAY_PREF = "FpsDisplayPref";
    private const string ENABLE_FULLSCREEN_PREF = "EnableFullscreenPref";

    // Start is called before the first frame update
    void Start()
    {
        // Assigning player prefs on startup 
        if (!PlayerPrefs.HasKey("MusicVolumePref"))
        {
            musicVolumeSlider.value = 1;
        }

        else if (!PlayerPrefs.HasKey("SfxVolumePref"))
        {
            sfxVolumeSlider.value = 1;
        }

        else
        {
            musicVolumeSlider.value = PlayerPrefs.GetFloat(MUSIC_VOLUME_PREF);
            sfxVolumeSlider.value = PlayerPrefs.GetFloat(SFX_VOLUME_PREF);
        }

        enableFpsCounter.isOn = GetBoolPref(FPS_DISPLAY_PREF);
        enableFullscreen.isOn = GetBoolPref(ENABLE_FULLSCREEN_PREF);
    }

    // Volume Prefs
    public void OnChangeSfxVolume(float value_) 
    {
        SetPref(SFX_VOLUME_PREF, value_);
    }
    public void OnChangeMusicVolume(float value_)
    {
        SetPref(MUSIC_VOLUME_PREF, value_);
    }

    // FPS Toggle Prefs
    public void OnToggleFpsCounter(bool isEnabled_) 
    {
        SetPref(FPS_DISPLAY_PREF, isEnabled_);
    }

    public void OnToggleFullscreen(bool isEnabled_) 
    {
        SetPref(ENABLE_FULLSCREEN_PREF, isEnabled_);
    }

    // Player Pref Getters/Setters
    private void SetPref(string key_, float value_) 
    {
        PlayerPrefs.SetFloat(key_, value_);
    }

    private void SetPref(string key_, string value_)
    {
        PlayerPrefs.SetString(key_, value_);
    }

    private void SetPref(string key_, int value_)
    {
        PlayerPrefs.SetInt(key_, value_);
    }

    private void SetPref(string key_, bool value_)
    {
        PlayerPrefs.SetInt(key_, Convert.ToInt32(value_));
    }
    private bool GetBoolPref(string key_, bool defualtValue = true)
    {
        return Convert.ToBoolean(PlayerPrefs.GetInt(key_, Convert.ToInt32(defualtValue)));
    } 
}
