using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public bool start = false;
    private string MapPickSelection = "";

    private bool showingInfo = false; 
    public TextMeshProUGUI SinglePlayerMapSelected;
    public TextMeshProUGUI SinglePlayerMapSelectedInfo; 
   
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void SelectMapText(string mapSelection_)
    {
        if (mapSelection_.Contains("Room1"))
        {
            SinglePlayerMapSelected.text = "MAP SELECTED: LOST & FOUND"; 
    
        }

        else if (mapSelection_.Contains("Room6"))
        {
            SinglePlayerMapSelected.text = "MAP SELECTED: SCI-FI MAP";
        }

        MapPickSelection = mapSelection_;
    }

    public void Play()
    {
        if (MapPickSelection.Length > 0)
        {
            SceneManager.LoadScene(MapPickSelection);
        }

        else
        {
            if (!showingInfo)
            {
                showingInfo = true; 
                SinglePlayerMapSelectedInfo.text = "Please Select a Map First!";
                StartCoroutine(ClearSinglePlayerMapInfo());
            }
        }
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

    IEnumerator ClearSinglePlayerMapInfo()
    {
        yield return new WaitForSeconds(1.0f);
        SinglePlayerMapSelectedInfo.text = "";
        showingInfo = false; 
    }
}
