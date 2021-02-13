using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;

public class RoomListingDetails : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI roomListingDetails_; 

    public void SetRoomInformation(RoomInfo info_)
    {
        roomListingDetails_.text = info_.Name;
    }
}
