using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    AudioSource[] sfxSounds;

    // ---Sound effects for Sci-fi---\\
    AudioSource accessGranted;
    AudioSource accessDenied;

    AudioSource pickUpCard;
    
    //Singleton Setup
    private static SoundHandler instance_;

    public static SoundHandler Instance { get { return instance_; } }

	private void Awake()
	{
        if (instance_ != null && instance_ != this)
        {
            Destroy(this.gameObject);
        }
        else 
        {
            instance_ = this;
            DontDestroyOnLoad(gameObject);
        }
	}
	// Start is called before the first frame update
	void Start()
    {
        sfxSounds = GetComponents<AudioSource>();
        
        accessDenied = sfxSounds[0];
        accessGranted = sfxSounds[1];
        pickUpCard = sfxSounds[2];
    }

    public void PlayAccessGranted() 
    {
        accessGranted.Play();
    }
    public void PlayAccessDenied()
    {
        accessDenied.Play();
    }

    public void PlayPickUpCard() 
    {
        pickUpCard.Play();
    }
}
