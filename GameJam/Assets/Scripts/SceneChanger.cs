using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class SceneChanger : MonoBehaviour
{
    public string sceneName;

    public Animator transition;
    public float transitionTime = 1f;

    PhotonView photonView;

    private void Awake()
    {
        photonView = transform.GetComponent<PhotonView>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("ENTERED COLLISION COMMAND");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("COLLIDED");

            if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
            {
                Debug.Log("Changing Scene SERVER SIDE");
                photonView.RPC("RPC_ChangeSceneForServer", RpcTarget.All);
            }

            else
            {
                Debug.Log("Changing Scene Locally");
                SceneManager.LoadScene(sceneName);
            }
        }
    }

    [PunRPC]
    void RPC_ChangeSceneForServer()
    {
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
