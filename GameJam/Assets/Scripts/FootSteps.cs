using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{

    CharacterController cc;
    

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
       
    }

    // Update is called once per frame
    void Update()
    {
        //if (cc.velocity.magnitude > 45f && GetComponent<AudioSource>().isPlaying == false)
        //{
        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") > 0 || Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Vertical") < 0)
        {
            if (!GetComponent<AudioSource>().isPlaying)

            {
                Debug.Log("IsPlaying sound");
                GetComponent<AudioSource>().Play();
            }
        }

       // }
    }
}
