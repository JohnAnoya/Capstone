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

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        PlayerListingDetails Listing = Instantiate(playerListing_, listingsContent_);
        if (Listing != null)
        {
            Listing.SetPlayerInformation(newPlayer);
            Listings_.Add(Listing);
        }
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
