using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ReaddMissingObjects : MonoBehaviour
{
    [SerializeField]
    private GameObject interactionReplication;

    [SerializeField]
    private GameObject inventory;

    private void Start()
    {
        StartCoroutine(AddBackInteractionAndInventory());
    }

    IEnumerator AddBackInteractionAndInventory()
    {
        yield return new WaitForSeconds(0.025f);
        if (!GameObject.Find("InteractionReplication"))
        {
            var newInteractionReplication = Instantiate(interactionReplication, transform.position, Quaternion.identity);
            newInteractionReplication.GetComponent<PhotonView>().ViewID = 51;
            newInteractionReplication.transform.parent = GameObject.Find("NetworkingManager").transform;
            newInteractionReplication.name = "InteractionReplication";

            var newInventory = Instantiate(inventory, transform.position, Quaternion.identity);
            newInventory.GetComponent<PhotonView>().ViewID = 50;
            newInventory.transform.parent = GameObject.Find("InteractionSystemManager").transform;
            newInventory.name = "Inventory";
        }
    }
}
