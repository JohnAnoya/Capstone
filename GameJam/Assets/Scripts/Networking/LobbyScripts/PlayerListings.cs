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


    public override void OnEnable()
    {
        base.OnEnable(); 
        GetAllCurrentRoomPlayers();
    }

    public override void OnDisable()
    {
        base.OnDisable();
        for (int i = 0; i < Listings_.Count; i++)
        {
            Destroy(Listings_[i].gameObject);
        }

        Listings_.Clear(); 
    }

    private void GetAllCurrentRoomPlayers()
    {
        if (!PhotonNetwork.IsConnected)
        {
            return; 
        }

        if(PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null)
        {
            return;
        }

        foreach (KeyValuePair<int, Player> playerInfo_ in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(playerInfo_.Value);
        }
    }

    private void AddPlayerListing(Player player_)
    {
        int index = Listings_.FindIndex(x => x.player == player_);
        if (index != -1)
        {
            Listings_[index].SetPlayerInformation(player_);
        }

        else
        {
            PlayerListingDetails Listing = Instantiate(playerListing_, listingsContent_);
            if (Listing != null)
            {
                Listing.SetPlayerInformation(player_);
                Listings_.Add(Listing);
            }
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

    public void StartMultiplayerGame ()
    {
        PhotonNetwork.LoadLevel("EscapeRoom1");
    }
}
