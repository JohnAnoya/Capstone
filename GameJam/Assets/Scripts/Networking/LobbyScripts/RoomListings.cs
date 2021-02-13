using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomListings : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform listingsContent_;
    [SerializeField]
    private RoomListingDetails roomListing_;

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {   
            RoomListingDetails Listing = Instantiate(roomListing_, listingsContent_);
            if (Listing != null)
            {
                Listing.SetRoomInformation(info);
            }
        }
    }
}
