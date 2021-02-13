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

    private List<RoomListingDetails> Listings_ = new List<RoomListingDetails>();

    public override void OnJoinedRoom()
    {
        GameObject.Find("RoomPanels").GetComponent<CreateRoomMenu>().HideCreateRoomPanel();
        GameObject.Find("RoomPanels").GetComponent<CreateRoomMenu>().ShowCurrentRoomPanel();
        listingsContent_.DestroyChildren();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            if (info.RemovedFromList) //If Room is removed from the list
            {
                int index = Listings_.FindIndex(x => x.roomInfo_.Name == info.Name);
                if(index != -1)
                {
                    Destroy(Listings_[index].gameObject);
                    Listings_.RemoveAt(index);
                }
            }


            else //Otherwise if the room is added to the list, instantiate the listing prefab! 
            {
                RoomListingDetails Listing = Instantiate(roomListing_, listingsContent_);
                if (Listing != null)
                {
                    Listing.SetRoomInformation(info);
                    Listings_.Add(Listing);
                }
            }
        }
    }
}
