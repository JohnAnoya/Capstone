using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class CreateRoomMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TextMeshProUGUI roomName_; 

    public void CreateNewRoom()
    {
        if (!PhotonNetwork.IsConnected)
        {
            return; 
        }

        else if(roomName_.text.Length > 0)
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 4;
            PhotonNetwork.CreateRoom(roomName_.text, roomOptions, TypedLobby.Default);
        }
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room was successfully created!");
        HideCreateRoomPanel();
        ShowCurrentRoomPanel();             
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Failed to create a room! ERROR MESSAGE: " + message);    
    }

    public void ShowCreateRoomPanel()
    {
        GameObject.Find("CreateRoomPanel").SetActive(true);
    }

    public void HideCreateRoomPanel()
    {
        GameObject.Find("CreateRoomPanel").SetActive(false);
    }

    public void ShowCurrentRoomPanel()
    {
        GameObject.Find("RoomPanels").transform.Find("CurrentRoomPanel").gameObject.SetActive(true);
    }

    public void HideCurrentRoomPanel()
    {
        GameObject.Find("RoomPanels").transform.Find("CurrentRoomPanel").gameObject.SetActive(false);
    }
}
