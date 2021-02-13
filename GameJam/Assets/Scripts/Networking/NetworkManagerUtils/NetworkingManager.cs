using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class NetworkingManager : MonoBehaviour
{
    /* SINGLETON CLASS SETUP */
    private static NetworkingManager instance_;
    public static NetworkingManager Instance { get { return instance_; } }

    void Awake()
    {
        if (instance_ != null && instance_ != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance_ = this;
            DontDestroyOnLoad(gameObject);
        }

        PopulatePrefabsonNetwork(); 
    }
    /* SINGLETON CLASS SETUP */

    [SerializeField]
    private Settings settings_; 
    public Settings Settings { get { return Instance.settings_; } }

    [SerializeField]
    private List<NetworkPrefab> networkPrefrabs = new List<NetworkPrefab>();

    public static GameObject InstantiateOverNetwork(GameObject object_, Vector3 pos_, Quaternion rot_)
    {
        foreach (NetworkPrefab networkPrefab in Instance.networkPrefrabs)
        {
            if(networkPrefab.Prefab == object_)
            {
                if (networkPrefab.Path != string.Empty)
                {
                    GameObject newPrefab = PhotonNetwork.Instantiate(networkPrefab.Path, pos_, rot_);

                    return newPrefab;
                }

                else
                {
                    Debug.LogError("Path is empty for gameobject!" + networkPrefab.Prefab);
                    return null;
                }
            }
        }

        return null; 
    }

    public static void DeleteObject(GameObject object_)
    {
        PhotonNetwork.Destroy(object_);
    } 

    public static void PopulatePrefabsonNetwork()
    {
#if UNITY_EDITOR
        Instance.networkPrefrabs.Clear();

        GameObject[] allPrefabs = Resources.LoadAll<GameObject>("");
        for(int i = 0; i < allPrefabs.Length; i++)
        {
            if(allPrefabs[i].GetComponent<PhotonView>() != null)
            {
                string prefabPath = AssetDatabase.GetAssetPath(allPrefabs[i]);
                Instance.networkPrefrabs.Add(new NetworkPrefab(allPrefabs[i], prefabPath));
            }
        }
#endif
    }
}
