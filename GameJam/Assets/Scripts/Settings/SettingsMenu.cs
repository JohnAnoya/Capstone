using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
	public AudioMixer musicMixer;
	public AudioMixer sfxMixer;
	
	public Canvas fpsCanvas;
	public bool fpsCountActive = false;

	SettingsManager settingsManager;

	private void Start()
	{
		settingsManager = GetComponent<SettingsManager>();

		float savedMusicVol = PlayerPrefs.GetFloat("MusicVolumePref");
		musicMixer.SetFloat("MusicVolume", Mathf.Log(savedMusicVol) * 20); //Source Used: https://forum.unity.com/threads/changing-audio-mixer-group-volume-with-ui-slider.297884/

        float savedSfxVol = PlayerPrefs.GetFloat("SfxVolumePref");
		sfxMixer.SetFloat("SfxVolume", Mathf.Log(savedSfxVol) * 20);
	}

	public void SetMusicVolume(float volume_) 
	{
		PlayerPrefs.SetFloat("MusicVolumePref", volume_);
		if (PlayerPrefs.HasKey("MusicVolumePref")) 
		{
			float savedMusicVol = PlayerPrefs.GetFloat("MusicVolumePref");
			musicMixer.SetFloat("MusicVolume", Mathf.Log(savedMusicVol) * 20);
		} 
	}

	public void SetSfxVolume(float volume_)
	{
		PlayerPrefs.SetFloat("SfxVolumePref", volume_);
		if (PlayerPrefs.HasKey("SfxVolumePref"))
		{
			float savedSfxVol = PlayerPrefs.GetFloat("SfxVolumePref");
			sfxMixer.SetFloat("SfxVolume", Mathf.Log(savedSfxVol) * 20);
		}
	}

	public void FpsDisplayCanvas() 
	{
		if (settingsManager.enableFpsCounter.isOn == true)
		{
			fpsCanvas.enabled = true;
		}
		else if (settingsManager.enableFpsCounter.isOn == false)
		{
			fpsCanvas.enabled = false;
		}
	}

	public void SetFullScreen() 
	{
		if (settingsManager.enableFullscreen.isOn == true)
		{
			Screen.fullScreen = Screen.fullScreen;
		}
		else if (settingsManager.enableFullscreen.isOn == false)
		{
			Screen.fullScreen = !Screen.fullScreen;
		}
	}
}
