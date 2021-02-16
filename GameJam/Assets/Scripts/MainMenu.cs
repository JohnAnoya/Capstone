using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class MainMenu : MonoBehaviour
{
    public bool start = false;
   
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoBackToMainMenu()
    {
        Destroy(GameObject.Find("NetworkingManager/InteractionReplication"));
        Destroy(GameObject.Find("InteractionSystemManager/Inventory"));
        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            PhotonNetwork.Disconnect(); 
        }

        StartCoroutine(LoadMainMenu());
    }

    public void Exit ()
    {
        Application.Quit();
    }

    IEnumerator LoadMainMenu()
    {
        yield return new WaitForSeconds(0.025f);
        SceneManager.LoadScene("MainMenu");
    }
}
