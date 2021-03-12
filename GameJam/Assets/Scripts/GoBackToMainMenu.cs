using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GoBackToMainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();     
        }

        if (GameObject.Find("NetworkingManager"))
        {
            Destroy(GameObject.Find("NetworkingManager"));
        }

        if (GameObject.Find("InteractionSystemManager"))
        {
            Destroy(GameObject.Find("InteractionSystemManager"));
        }

        StartCoroutine(BackToMainMenuScene());
    }
    
    IEnumerator BackToMainMenuScene()
    {
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene("MainMenu");
    }
}
