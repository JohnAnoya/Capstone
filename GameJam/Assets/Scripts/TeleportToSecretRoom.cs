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
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag.Equals("Player") && other.gameObject.GetComponent<CharacterController>())
        {
            other.gameObject.GetComponent<CharacterController>().enabled = false;
            other.gameObject.transform.position = Entrance.transform.position;
            StartCoroutine(ReEnableCharacterController(other.gameObject.tag));
        }    
    }


  IEnumerator ReEnableCharacterController(string playerTag_)
  {
        yield return new WaitForSeconds(0.025f);

        if (GameObject.FindGameObjectWithTag(playerTag_).gameObject.GetComponent<CharacterController>())
        {
            GameObject.FindGameObjectWithTag(playerTag_).GetComponent<CharacterController>().enabled = true;
        }
  }
}
