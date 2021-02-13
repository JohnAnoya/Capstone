using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LeaveRoom : MonoBehaviour
{
   public void LeaveCurrentRoom()
    {
        PhotonNetwork.LeaveRoom(true);
        GameObject.Find("RoomPanels").GetComponent<CreateRoomMenu>().HideCurrentRoomPanel();
        GameObject.Find("RoomPanels").GetComponent<CreateRoomMenu>().ShowCreateRoomPanel();
    }
}
