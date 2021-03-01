using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class TeleportToTeleporter : MonoBehaviour
{
    [SerializeField]
    private GameObject OtherTeleporter;

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
                other.gameObject.transform.position = OtherTeleporter.transform.position;
                cc.enabled = true;
            }
        }
    }
}
