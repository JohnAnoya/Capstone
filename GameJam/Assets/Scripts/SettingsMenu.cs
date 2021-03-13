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

	private void Start()
	{
		FpsDisplayCanvas();
	}

	public void SetMusicVolume(float volume_) 
	{
		//Converting slider to use logarithmic scale instead of a linear one 
		musicMixer.SetFloat("MusicVolume", Mathf.Log10(volume_) * 20);
	}

	public void SetSfxVolume(float volume_)
	{
		//Converting slider to use logarithmic scale instead of a linear one 
		sfxMixer.SetFloat("SfxVolume", Mathf.Log10(volume_) * 20);
	}

	public void FpsDisplayCanvas() 
	{
		if (!fpsCountActive)
		{
			fpsCanvas.enabled = true;
			fpsCountActive = true;
		}
		else if (fpsCountActive)
		{
			fpsCanvas.enabled = false;
			fpsCountActive = false;
		}
	}

}
