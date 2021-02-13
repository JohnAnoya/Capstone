using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NetworkPrefab 
{
    public GameObject Prefab;
    public string Path; 

    public NetworkPrefab(GameObject object_, string path_)
    {
        Prefab = object_;
        Path = SetModifiedPrefabPath(path_);
    }

    private string SetModifiedPrefabPath(string path_)
    {
        int extensionLength = System.IO.Path.GetExtension(path_).Length;
        int additionalLength = 10; 
        int startIndex = path_.ToLower().IndexOf("resources");

        if (startIndex == -1)
        {
            return string.Empty;
        }

        else
        {
            return path_.Substring(startIndex + additionalLength, path_.Length - (additionalLength + startIndex + extensionLength));
        }
    }
}
