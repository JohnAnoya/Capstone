using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class InteractionReplicate : MonoBehaviour
{
    static PhotonView photonView;

    private Animator SciFiDoubleDoor;
    private Animator[] DoubleDoor = new Animator[2];
    private Animator SingleDoor; 
    private TextMeshProUGUI AnswerScreenText; 

    private void Awake()
    {
        photonView = transform.GetComponent<PhotonView>();
    }

    public static void ReplicateCubeDraggingBool(string cubeName_, bool isDragging_)
    {
        if(photonView && PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            photonView.RPC("RPC_ReplicateCubeDraggingBool", RpcTarget.All, cubeName_, isDragging_);
        }
    }

    public static void ReplicateCubeDragging(string cubeName_, Vector3 newPos_)
    {
        if (photonView && PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            photonView.RPC("RPC_ReplicateCubeDragging", RpcTarget.All, cubeName_, newPos_);
        }
    }

            public static void OpenSciFiDoubleDoors(string doubleDoorName_)
    {
        if (photonView && PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            photonView.RPC("RPC_OpenSciFiDoubleDoors", RpcTarget.All, doubleDoorName_);
        }
    }

    public static void CloseSciFiDoubleDoors(string doubleDoorName_)
    {
        if (photonView && PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            photonView.RPC("RPC_CloseSciFiDoubleDoors", RpcTarget.All, doubleDoorName_);
        }
    }

    public static void OpenDoubleDoors(string leftDoorName_, string rightDoorName_)
    {
        if (photonView && PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            photonView.RPC("RPC_OpenDoubleDoors", RpcTarget.All, leftDoorName_, rightDoorName_);
        }
    }

    public static void OpenSingleDoor(string doorName_)
    {
        if(photonView && PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            photonView.RPC("RPC_OpenSingleDoor", RpcTarget.All, doorName_);
        }
    }

    public static void UpdateAnswerScreenOnServer(string keyPressed_)
    {
        if (photonView && PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            photonView.RPC("RPC_UpdateAnswerScreen", RpcTarget.All, keyPressed_);
        }
    }

    [PunRPC]
    void RPC_ReplicateCubeDraggingBool(string cubeName_, bool isDragging_)
    {
        if (GameObject.Find(cubeName_))
        {
            var cube = GameObject.Find(cubeName_).transform;

            if (cube != null)
            {
                if (isDragging_)
                {
                    cube.GetChild(0).GetComponent<CubeProperties>().isDraggingCube = true;
                    cube.GetComponent<Rigidbody>().useGravity = false;
                }

                else
                {
                    cube.GetChild(0).GetComponent<CubeProperties>().isDraggingCube = false;
                    cube.GetComponent<Rigidbody>().useGravity = true; 
                }
              
            }
        }
    }

    [PunRPC]
    void RPC_ReplicateCubeDragging(string cubeName_, Vector3 newPos_)
    {
        if (GameObject.Find(cubeName_))
        {
            GameObject.Find(cubeName_).transform.position = new Vector3(newPos_.x, newPos_.y, newPos_.z);
        }
    }

    [PunRPC]
    void RPC_OpenSciFiDoubleDoors(string DoubleDoorName_)
    {
        if (GameObject.Find(DoubleDoorName_))
        {
            SciFiDoubleDoor = GameObject.Find(DoubleDoorName_).GetComponent<Animator>(); 

            if(SciFiDoubleDoor != null)
            {
                Debug.Log("Opening Doors Serverside");
                SciFiDoubleDoor.SetBool("DoorOpen", true);
            }
        }
    }


    [PunRPC]
    void RPC_CloseSciFiDoubleDoors(string DoubleDoorName_)
    {
        if (GameObject.Find(DoubleDoorName_))
        {
            SciFiDoubleDoor = GameObject.Find(DoubleDoorName_).GetComponent<Animator>();

            if (SciFiDoubleDoor != null)
            {
                Debug.Log("Closing Doors Serverside");
                SciFiDoubleDoor.SetBool("DoorOpen", false);
            }
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

                if (photonView.IsMine)
                {            
                    if (GameObject.Find("DoubleDoorTrigger"))
                    {
                        NetworkingManager.DeleteObject(GameObject.Find("DoubleDoorTrigger"));
                    }
                }
            }
        }
    }

    [PunRPC]
    void RPC_OpenSingleDoor(string doorName_)
    {
        if (GameObject.Find(doorName_))
        {
            SingleDoor = GameObject.Find(doorName_).GetComponent<Animator>();

            if(SingleDoor != null)
            {
                Debug.Log("Opening Single Door SERVER SIDE");
                if(photonView.IsMine)
                {
                    SingleDoor.SetBool("DoorOpen", true);
                }
            }
        }
    }

    [PunRPC]
    void RPC_UpdateAnswerScreen(string keyPressed_)
    {
        if (AnswerScreenText == null)
        {
            AnswerScreenText = GameObject.Find("AnswerText").GetComponent<TextMeshProUGUI>(); 
        }

        AnswerScreenText.text = AnswerScreenText.text + keyPressed_;
    }
}
