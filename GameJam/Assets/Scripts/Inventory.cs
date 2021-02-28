using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Inventory : MonoBehaviour
{
    static PhotonView photonView;
    static List<string> CurrentInventory = new List<string>();
    static bool hasItem = false;

    //Dungeon Room
    static public int PotionCount = 0;

    //Sci-Fi Room 
    static public int KeyCardUpgradeCount = 1; 

    private void Awake()
    {
        photonView = transform.GetComponent<PhotonView>();
    }

    public static void AddToInventory(string item_, bool removeItemFromScene_, string gameObjectName_)
    {
        Debug.Log(removeItemFromScene_);
        if (photonView && PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            Debug.Log("Removing Server Side");
            photonView.RPC("RPC_AddToInventory", RpcTarget.All, item_, removeItemFromScene_, gameObjectName_);
        }

        else
        {
            if (removeItemFromScene_)
            {
                Debug.Log("Removing Client Side");
                if (gameObjectName_.Contains("Potion"))
                {
                    PotionCount += 1;
                }

                CurrentInventory.Add(item_);
                Destroy(GameObject.Find(gameObjectName_));
            }
        }
    }

    public static bool CheckInventory(string item_)
    {
       if(CurrentInventory.Contains(item_))
        {
            hasItem = true; 
            return hasItem; 
        }

       else
        {
            hasItem = false; 
            return hasItem; 
        }
    }

    [PunRPC]
    void RPC_AddToInventory(string item_, bool removeItemFromScene_, string gameObjectName_)
    {
        CurrentInventory.Add(item_);

        if(removeItemFromScene_)
        {
            if (gameObjectName_.Contains("Potion"))
            {
                PotionCount += 1;
                Debug.Log("Adding Potion" + PotionCount + " to Inventory!");
                CurrentInventory.Add("Potion" + PotionCount);
            }

            if (photonView.IsMine)
            {
                NetworkingManager.DeleteObject(GameObject.Find(gameObjectName_));
            }
        }
    }
}
