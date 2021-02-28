using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToTeleporter : MonoBehaviour
{
    [SerializeField]
    private GameObject OtherTeleporter;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag.Equals("Player"))
        {
            Debug.Log("Found Player!");
            other.gameObject.GetComponent<CharacterController>().enabled = false;
            other.gameObject.transform.position = OtherTeleporter.transform.position;
            StartCoroutine(ReEnableCharacterController(other.gameObject.tag));
        }
    }


    IEnumerator ReEnableCharacterController(string playerTag_)
    {
        yield return new WaitForSeconds(0.025f);
        GameObject.FindGameObjectWithTag(playerTag_).GetComponent<CharacterController>().enabled = true;
    }
}
