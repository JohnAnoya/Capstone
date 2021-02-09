using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource soundEffectSource;
    public AudioSource musicSource;

    public bool levelMusic = true;

    public static AudioManager instance = null;

    public Player playerInteraction;

    // will be used to alter the pitch of audio effects for variation of sound.
    public float lowPitch = 0.95f;
    public float highPitch = 1.05f;

    private void Awake()
	{
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
	}

    //Play single audio clip
    public void PlayAudioClip(AudioClip audio)
    {
        levelMusic = true;
        soundEffectSource.clip = audio;
        musicSource.PlayOneShot(audio);
    }

    // will alter the clip of multiple audio tracks using the low and high pitch variables 
    public void alterPitchEffect(params AudioClip[] audioClips)
    {
        int randomNum = Random.Range(0, audioClips.Length);
        float randomPitch = Random.Range(lowPitch, highPitch);

        soundEffectSource.pitch = randomPitch;
        soundEffectSource.clip = audioClips[randomNum];
        soundEffectSource.Play();
    }

}
