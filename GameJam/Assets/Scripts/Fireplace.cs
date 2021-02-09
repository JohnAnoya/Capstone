using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireplace : MonoBehaviour
{

    [SerializeField] AudioSource fireSound;

    

    // Start is called before the first frame update
    void Start()
    {
        fireSound.Play();
    }

    private void Update()
    {
        if (InteractionSystemManager.Instance.firePlace == false)
        {
            fireSound.Stop();
            Debug.Log("Stopped fireplace sound from fireplace script");
        }
        else if (InteractionSystemManager.Instance.firePlace) 
        {
            fireSound.Play();
        }
	}

}
