using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FireParticlesServer : MonoBehaviour
{
    static PhotonView photonView;

    private void Awake()
    {
        photonView = transform.GetComponent<PhotonView>(); 
    }

    public static void ServerStopPlayingFireParticles()
    {
        if (photonView && PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            photonView.RPC("RPC_StopFireParticles", RpcTarget.All, photonView.Owner);
        }
    }

    public static void ServerStartPlayingFireParticles()
    {
        if (photonView && PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            photonView.RPC("RPC_PlayFireParticles", RpcTarget.All, photonView.Owner);
        }
    }

    [PunRPC]
    void RPC_StopFireParticles(PhotonMessageInfo info)
    {
        transform.GetComponent<ParticleSystem>().Stop();
        InteractionSystemManager.firePlace = false; 
    }

    [PunRPC]
    void RPC_PlayFireParticles(PhotonMessageInfo info)
    {
        transform.GetComponent<ParticleSystem>().Play();
        InteractionSystemManager.firePlace = true;
    }
}
