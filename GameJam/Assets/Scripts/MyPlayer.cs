using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;

public class MyPlayer : MonoBehaviourPun
{
    List<string> Inventory = new List<string>();
    
    public Camera camera; 
    CharacterController characterController; 
    Vector3 playerVel;
    float WalkSpeed = 8.0f;
    float gravity = -9.81f;
    float thickness = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsConnected && base.photonView.IsMine)
        {
            float XAxis = Input.GetAxis("Horizontal");
            float ZAxis = Input.GetAxis("Vertical");

            Vector3 move = transform.right * XAxis + transform.forward * ZAxis;
            characterController.Move(move * Time.deltaTime * WalkSpeed);

            playerVel.y += gravity * Time.deltaTime;
            characterController.Move(playerVel * Time.deltaTime);
          
        }

        else if (PhotonNetwork.IsConnected && !base.photonView.IsMine)
        {
            Destroy(characterController);
        }

        else if (!PhotonNetwork.IsConnected)
        {
            float XAxis = Input.GetAxis("Horizontal");
            float ZAxis = Input.GetAxis("Vertical");

            Vector3 move = transform.right * XAxis + transform.forward * ZAxis;
            characterController.Move(move * Time.deltaTime * WalkSpeed);

            playerVel.y += gravity * Time.deltaTime;
            characterController.Move(playerVel * Time.deltaTime);
        }
    }

   public void AddToInventory(string item_)
    {
        Inventory.Add(item_);
    }

    public bool CheckInventory(string item_)
    {
        if (Inventory.Contains(item_))
        {
            return true; 
        }

        else
        {
            return false;
        }
    }
}
