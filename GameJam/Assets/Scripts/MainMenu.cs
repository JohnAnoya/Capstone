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

    [SerializeField]
    private Sprite[] BackgroundImages = new Sprite[5];
    private int ImageIndex = 0;
    private bool showNewBackgroundImage = true; 

    [SerializeField]
    private GameObject MainMenuPanel;
    [SerializeField]
    private GameObject SettingsPanel;
    [SerializeField]
    private GameObject CreditsPanel;
    [SerializeField]
    private GameObject UsernamePanel;
    [SerializeField]
    private GameObject CreateRoomPanel;
    [SerializeField]
    private GameObject CurrentRoomPanel;
    [SerializeField]
    private GameObject MapPickPanel;


    private void Update()
    {
        if (showNewBackgroundImage && SceneManager.GetActiveScene().buildIndex == 0)
        {
            showNewBackgroundImage = false;
            StartCoroutine(ChangeBackgroundImage());
        }
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

    IEnumerator ChangeBackgroundImage()
    {

        MainMenuPanel.GetComponent<Image>().sprite = BackgroundImages[ImageIndex];
        SettingsPanel.GetComponent<Image>().sprite = BackgroundImages[ImageIndex];
        CreditsPanel.GetComponent<Image>().sprite = BackgroundImages[ImageIndex];
        UsernamePanel.GetComponent<Image>().sprite = BackgroundImages[ImageIndex];
        CreateRoomPanel.GetComponent<Image>().sprite = BackgroundImages[ImageIndex];
        CurrentRoomPanel.GetComponent<Image>().sprite = BackgroundImages[ImageIndex];
        MapPickPanel.GetComponent<Image>().sprite = BackgroundImages[ImageIndex];

        //FADE IN 
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            MainMenuPanel.GetComponent<Image>().color = new Color(1, 1, 1, i);
            SettingsPanel.GetComponent<Image>().color = new Color(1, 1, 1, i);
            CreditsPanel.GetComponent<Image>().color = new Color(1, 1, 1, i);
            UsernamePanel.GetComponent<Image>().color = new Color(1, 1, 1, i);
            CreateRoomPanel.GetComponent<Image>().color = new Color(1, 1, 1, i);
            CurrentRoomPanel.GetComponent<Image>().color = new Color(1, 1, 1, i);
            MapPickPanel.GetComponent<Image>().color = new Color(1, 1, 1, i);
            yield return null;
        }

        if (ImageIndex < 4)
        {
            ImageIndex++;
        }

        else
        {
            ImageIndex = 0;
        }

        yield return new WaitForSeconds(10.0f);
        //FADE OUT
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            MainMenuPanel.GetComponent<Image>().color = new Color(1, 1, 1, i);
            SettingsPanel.GetComponent<Image>().color = new Color(1, 1, 1, i);
            CreditsPanel.GetComponent<Image>().color = new Color(1, 1, 1, i);
            UsernamePanel.GetComponent<Image>().color = new Color(1, 1, 1, i);
            CreateRoomPanel.GetComponent<Image>().color = new Color(1, 1, 1, i);
            CurrentRoomPanel.GetComponent<Image>().color = new Color(1, 1, 1, i);
            MapPickPanel.GetComponent<Image>().color = new Color(1, 1, 1, i);
            yield return null;
        }

        showNewBackgroundImage = true; 
    }
}
