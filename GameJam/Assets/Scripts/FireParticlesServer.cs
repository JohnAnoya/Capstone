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
        if (photonView && photonView.IsMine && PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            photonView.RPC("RPC_StopFireParticles", RpcTarget.AllBuffered, photonView.Owner);
        }
    }

    public static void ServerStartPlayingFireParticles()
    {
        if (photonView && photonView.IsMine && PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            photonView.RPC("RPC_PlayFireParticles", RpcTarget.AllBuffered, photonView.Owner);
        }
    }

    [PunRPC]
    void RPC_StopFireParticles(PhotonMessageInfo info)
    {
        transform.GetComponent<ParticleSystem>().Stop();
    }

    [PunRPC]
    void RPC_PlayFireParticles(PhotonMessageInfo info)
    {
        transform.GetComponent<ParticleSystem>().Play();
    }
}
