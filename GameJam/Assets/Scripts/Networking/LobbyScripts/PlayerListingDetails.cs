using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;

public class PlayerListingDetails : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI playerListingDetails_;

    public Player player { get; private set; } 

    public void SetPlayerInformation(Player player_)
    {
        player = player_;
        playerListingDetails_.text = player_.NickName;
    }
}
