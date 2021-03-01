using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class TeleportToSecretRoom : MonoBehaviour
{
    [SerializeField]
    private GameObject Entrance;

    PhotonView photonView;

    private void Awake()
    {
        photonView = transform.GetComponent<PhotonView>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            if (other.gameObject.GetComponent<CharacterController>() != null)
            {
                var cc = other.gameObject.GetComponent<CharacterController>();
                cc.enabled = false;
                other.gameObject.transform.position = Entrance.transform.position;
                cc.enabled = true;
            }
        }    
    }
}
