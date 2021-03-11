using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class TestConnect : MonoBehaviourPunCallbacks
{
    
    public GameObject CreateRoomPanel;
    public GameObject CreateUserNamePanel;


    public void ConnectToServer()
    {
        if (transform.GetComponent<NetworkingManager>().Settings.CheckIfHasUserName())
        {
            CreateUserNamePanel.SetActive(false);
            CreateRoomPanel.SetActive(true);          
            Debug.Log("Connecting to Server...");
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.NickName = transform.GetComponent<NetworkingManager>().Settings.NewNickName();
            PhotonNetwork.GameVersion = "0.0.2";
            PhotonNetwork.ConnectUsingSettings();
        }

        else
        {
            CreateRoomPanel.SetActive(false);
            CreateUserNamePanel.SetActive(true);
            Debug.LogWarning("Player has no Username!");
        }
    }

    public void DisconnectFromServer()
    {
        PhotonNetwork.Disconnect(); 
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon.", this);

        Debug.Log("My nickname is " + PhotonNetwork.LocalPlayer.NickName, this);
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Failed to connect to Photon: " + cause.ToString(), this);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " has joined the lobby!");
    }
}
