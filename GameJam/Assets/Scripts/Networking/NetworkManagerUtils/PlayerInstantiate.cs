using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerInstantiate : MonoBehaviour
{
    [SerializeField]
    private GameObject player_;

    private void Awake()
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            Debug.Log("CREATING SERVER SIDE PLAYER");
            Vector2 offset = Random.insideUnitCircle * 3.0f;
            Vector3 position = new Vector3(transform.position.x + offset.x, transform.position.y, transform.position.z);
            var plr = NetworkingManager.InstantiateOverNetwork(player_, position, Quaternion.identity);      
            GameObject.FindObjectOfType<Camera>().enabled = true;
        }

        else 
        {
            Debug.Log("Creating local player");
            Instantiate(player_, transform.position, Quaternion.identity);
            GameObject.FindObjectOfType<Camera>().enabled = true;
        }
    }
}
