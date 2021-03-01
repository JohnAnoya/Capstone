using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToSecretRoom : MonoBehaviour
{
    [SerializeField]
    private GameObject Entrance;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag.Equals("Player"))
        {
            Debug.Log("Found Player!");
            other.gameObject.GetComponent<CharacterController>().enabled = false; 
            other.gameObject.transform.position = Entrance.transform.position;
            StartCoroutine(ReEnableCharacterController(other.gameObject.name));
        }    
    }


  IEnumerator ReEnableCharacterController(string playerName_)
  {
        yield return new WaitForSeconds(0.025f);
        GameObject.Find(playerName_).GetComponent<CharacterController>().enabled = true;
  }
}
