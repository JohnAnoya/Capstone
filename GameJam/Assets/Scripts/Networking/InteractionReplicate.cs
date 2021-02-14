using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 

public class InteractionReplicate : MonoBehaviour
{
    static PhotonView photonView;

    private Animator[] DoubleDoor = new Animator[2];

    private void Awake()
    {
        photonView = transform.GetComponent<PhotonView>();
    }


    public static void OpenDoubleDoors(string leftDoorName_, string rightDoorName_)
    {
        if (photonView && PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            photonView.RPC("RPC_OpenDoubleDoors", RpcTarget.All, leftDoorName_, rightDoorName_);
        }
    }


    [PunRPC]
    void RPC_OpenDoubleDoors(string leftDoorName_, string rightDoorName_)
    {
        if(GameObject.Find(leftDoorName_) && GameObject.Find(rightDoorName_))
        {
            DoubleDoor[0] = GameObject.Find(leftDoorName_).GetComponent<Animator>();
            DoubleDoor[1] = GameObject.Find(rightDoorName_).GetComponent<Animator>();

            if (DoubleDoor != null)
            {
                Debug.Log("Opening Doors Serverside");
                DoubleDoor[0].SetBool("DoorOpen", true);
                DoubleDoor[1].SetBool("DoorOpen", true);

                if (GameObject.Find("DoubleDoorTrigger"))
                {
                    NetworkingManager.DeleteObject(GameObject.Find("DoubleDoorTrigger"));
                }
            }
        }
    }
}
