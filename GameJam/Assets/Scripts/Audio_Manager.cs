using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Manager : MonoBehaviour
{
    public AudioSource switchSound;


    public void playsoundEffect()
    {
        switchSound.Play();
    }
}
