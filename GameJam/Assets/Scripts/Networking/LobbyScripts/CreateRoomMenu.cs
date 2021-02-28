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

    //private ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();

    private string MapPickSelection = "";
    public TextMeshProUGUI MultiPlayerMapSelected;
    public TextMeshProUGUI MultiPlayerMapSelectedInfo;
    bool showingInfo = false; 

    bool RoomSuccessfullyCreated = false; 

    public void SelectMapText(string mapSelection_)
    {
        if (mapSelection_.Contains("Room1"))
        {
            MultiPlayerMapSelected.text = "MAP SELECTED: LOST & FOUND";

        }

        else if (mapSelection_.Contains("Room6"))
        {
            MultiPlayerMapSelected.text = "MAP SELECTED: SCI-FI MAP";
        }

        MapPickSelection = mapSelection_;      
    }

    public void StartMultiplayerGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {

            PhotonNetwork.LoadLevel(MapPickSelection);
        }

        else
        {
            Debug.LogWarning("You can't start the game because you are not the host!");
        }
    }


    public void CreateNewRoom()
    {
        if (!PhotonNetwork.IsConnected)
        {
            RoomSuccessfullyCreated = false; 
            return; 
        }

        else if(roomName_.text.Length > 0 && MapPickSelection.Length > 0)
        {
            RoomSuccessfullyCreated = true;
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 4;

            PhotonNetwork.CreateRoom(roomName_.text, roomOptions, TypedLobby.Default);
        }

        else if (MapPickSelection.Length < 1)
        {
            Debug.Log("Trying to show Error");
            if (!showingInfo)
            {
                showingInfo = true;
                MultiPlayerMapSelectedInfo.text = "Please Select a Map First!";
                StartCoroutine(ClearSinglePlayerMapInfo());
            }
        }
    }

    public override void OnCreatedRoom()
    {
        if (RoomSuccessfullyCreated)
        {
            Debug.Log("Room was successfully created!");
            HideCreateRoomPanel();
            ShowCurrentRoomPanel();
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Failed to create a room! ERROR MESSAGE: " + message);    
    }

    public void ShowCreateRoomPanel()
    {
        GameObject.Find("RoomPanels").transform.Find("CreateRoomPanel").gameObject.SetActive(true);
    }

    public void HideCreateRoomPanel()
    {
        GameObject.Find("RoomPanels").transform.Find("CreateRoomPanel").gameObject.SetActive(false);
    }

    public void ShowCurrentRoomPanel()
    {
        GameObject.Find("RoomPanels").transform.Find("CurrentRoomPanel").gameObject.SetActive(true);
    }

    public void HideCurrentRoomPanel()
    {
        GameObject.Find("RoomPanels").transform.Find("CurrentRoomPanel").gameObject.SetActive(false);
    }

    IEnumerator ClearSinglePlayerMapInfo()
    {
        yield return new WaitForSeconds(1.0f);
        MultiPlayerMapSelectedInfo.text = "";
        showingInfo = false;
    }
}
