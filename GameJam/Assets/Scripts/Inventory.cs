using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    private void Start()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene; //Call the ChangedActiveScene Method whenever the scene changes
    }

    private void ChangedActiveScene(Scene current, Scene next)
    {
        if (next.name.Contains("EscapeRoom")) //When the Scene changes make sure its an EscapeRoom Scene, and assign interactables during runtime
        {
            PotionCount = 0;
            KeyCardUpgradeCount = 1; 
        }
    }

    public static void RemoveFromInventory(string item_)
    {
        CurrentInventory.Remove(item_);
    }

    public static void IncrementValue(string ValueName_, int customIncrement_)
    {
        if (photonView && PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            photonView.RPC("RPC_IncrementValue", RpcTarget.All, ValueName_, customIncrement_);   
        }
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
                    CurrentInventory.Add("Potion" + PotionCount);
                    return; 
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
    void RPC_IncrementValue(string ValueName_, int customIncrement_)
    {
        if(ValueName_.Equals("KeyCardUpgrade"))
        {
            KeyCardUpgradeCount += customIncrement_;
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
