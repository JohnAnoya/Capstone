using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerInstantiate : MonoBehaviour
{
    [SerializeField]
    private GameObject player_;

    [SerializeField]
    private Camera camera_; 

    private void Awake()
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            StartCoroutine(InstantiateObjectDelayed());
        }

        else 
        {
            Debug.Log("Creating local player");
            var plr = Instantiate(player_, transform.position, Quaternion.identity);
            var newCamera = Instantiate(camera_, transform.position, Quaternion.identity);
            newCamera.transform.position = new Vector3(plr.transform.position.x, plr.transform.position.y + 0.85f, plr.transform.position.z + 0.45f);
            newCamera.transform.parent = plr.transform;
        }
    }


    private IEnumerator InstantiateObjectDelayed()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("CREATING SERVER SIDE PLAYER");
        Vector2 offset = Random.insideUnitCircle * 3.0f;
        Vector3 position = new Vector3(transform.position.x + offset.x, transform.position.y, transform.position.z);
        var plr = NetworkingManager.InstantiateOverNetwork(player_, position, Quaternion.identity);
        var newCamera = Instantiate(camera_, transform.position, Quaternion.identity);
        newCamera.transform.position = new Vector3(plr.transform.position.x, plr.transform.position.y + 0.85f, plr.transform.position.z + 0.45f);
        newCamera.transform.parent = plr.transform;     
    }
}
