using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ColourCubePuzzle : MonoBehaviour
{
    [SerializeField]
    private GameObject RedPad;

    [SerializeField]
    private GameObject GreenPad;

    [SerializeField]
    private GameObject BluePad;

    void Update()
    {
        if (RedPad.GetComponent<CubeScanner>().isOnPad
            && GreenPad.GetComponent<CubeScanner>().isOnPad
            && BluePad.GetComponent<CubeScanner>().isOnPad){

            if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
            {
                InteractionReplicate.OpenSciFiDoubleDoors(gameObject.name);
            }

            else
            {
                gameObject.GetComponent<Animator>().SetBool("DoorOpen", true);
            }
        }
    }
}
