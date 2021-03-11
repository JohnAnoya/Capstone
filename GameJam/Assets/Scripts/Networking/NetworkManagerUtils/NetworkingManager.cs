using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class NetworkingManager : MonoBehaviour
{
    /* SINGLETON CLASS SETUP */
    private static NetworkingManager instance_;
    public static NetworkingManager Instance { get { return instance_; } }

    void Awake()
    {
        if (instance_ == null)
        {
            instance_ = this;
            DontDestroyOnLoad(gameObject);
           
        }

        else if (instance_ != this)
        {
            Destroy(instance_.gameObject);
            instance_ = this;
            DontDestroyOnLoad(gameObject);
        }
        //PopulatePrefabsonNetwork(); 
    }
    /* SINGLETON CLASS SETUP */

    [SerializeField]
    private Settings settings_;
    public Settings Settings { get { return Instance.settings_; } }

    //[SerializeField]
    //private List<NetworkPrefab> networkPrefrabs = new List<NetworkPrefab>();


    public static GameObject InstantiateOverNetwork(GameObject object_, Vector3 pos_, Quaternion rot_)
    {
        GameObject newPrefab = PhotonNetwork.Instantiate("NetworkPrefabs/" + object_.name, pos_, rot_);

        return newPrefab;
    }

    public static void DeleteObject(GameObject object_)
    {
        PhotonNetwork.Destroy(object_);
    }
}