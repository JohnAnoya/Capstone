using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MyPlayer : MonoBehaviourPun
{   
    public Camera camera; 
    CharacterController characterController; 
    Vector3 playerVel;
    float WalkSpeed = 8.0f;
    float gravity = -8.0f;
    float thickness = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        camera = gameObject.GetComponentInChildren<Camera>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom && base.photonView.IsMine)
        {
            float XAxis = Input.GetAxis("Horizontal");
            float ZAxis = Input.GetAxis("Vertical");

            Vector3 move = transform.right * XAxis + transform.forward * ZAxis;
            characterController.Move(move * Time.deltaTime * WalkSpeed);

            characterController.Move(new Vector3(playerVel.x, gravity, playerVel.z) * Time.deltaTime);
        }

        else if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom && !base.photonView.IsMine)
        {
            Destroy(characterController);
            Destroy(camera);
        }

        else
        {
            float XAxis = Input.GetAxis("Horizontal");
            float ZAxis = Input.GetAxis("Vertical");

            Vector3 move = transform.right * XAxis + transform.forward * ZAxis;
            characterController.Move(move * Time.deltaTime * WalkSpeed);

            characterController.Move(new Vector3(playerVel.x, gravity, playerVel.z) * Time.deltaTime);     
        }
    }
}

