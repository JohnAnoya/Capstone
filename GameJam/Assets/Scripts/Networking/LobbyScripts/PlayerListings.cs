using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerListings : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform listingsContent_;
    [SerializeField]
    private PlayerListingDetails playerListing_;

    private List<PlayerListingDetails> Listings_ = new List<PlayerListingDetails>();

    private void Awake()
    {
        GetAllCurrentRoomPlayers();
    }

    public override void OnLeftRoom()
    {
        listingsContent_.DestroyChildren();
    }

    private void GetAllCurrentRoomPlayers()
    {
        foreach (KeyValuePair<int, Player> playerInfo_ in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(playerInfo_.Value); 
        } 
    }

    private void AddPlayerListing(Player player_)
    {
        PlayerListingDetails Listing = Instantiate(playerListing_, listingsContent_);
        if (Listing != null)
        {
            Listing.SetPlayerInformation(player_);
            Listings_.Add(Listing);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerListing(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = Listings_.FindIndex(x => x.player == otherPlayer);
        if (index != -1)
        {
            Destroy(Listings_[index].gameObject);
            Listings_.RemoveAt(index);
        }
    }
}
