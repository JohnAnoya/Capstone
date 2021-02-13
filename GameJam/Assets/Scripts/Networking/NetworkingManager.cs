using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
    }
    /* SINGLETON CLASS SETUP */

    [SerializeField]
    private Settings settings_; 
    public Settings Settings { get { return Instance.settings_; } }
}
