using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;

public class RoomListingDetails : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI roomListingDetails_;

    public RoomInfo roomInfo_ { get; private set; } 

    public void SetRoomInformation(RoomInfo info_)
    {
        roomInfo_ = info_;
        roomListingDetails_.text = info_.Name;
    }
}
