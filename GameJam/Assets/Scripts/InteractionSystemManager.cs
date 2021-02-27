using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class InteractionSystemManager : MonoBehaviour
{
    /* SINGLETON CLASS SETUP */
    private static InteractionSystemManager instance_;
    public static InteractionSystemManager Instance { get { return instance_; } }

    void Awake()
    {
        if (instance_ != null && instance_ != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance_ = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    /* SINGLETON CLASS SETUP */

    GameObject player; //Reference to Player

    static List<string> Items = new List<string>(); //List of Items
    static List<string> Interactables = new List<string>(); //List of Interactables

    public GameObject InteractionPopUp; //Interaction Notification Popup Prefab
    GameObject tempPopup;
    bool showingPopup = false;

    static public bool firePlace = true; //Fireplace stuff
    public ParticleSystem fireplaceEmitter;
    public AudioSource firePlaceSounds;
    private PhotonView fireParticleView; 

    [SerializeField] //Note stuff
    Image NoteImage; 
    bool showingNote = false;

    [SerializeField] private Animator[] DoubleDoor = new Animator[2];
    [SerializeField] private Animator SingleDoorPrison = null;
    [SerializeField] private Animator ExitDoor = null;
    bool DoubleDoorisOpen = false;

   // AudioSource switchSound;
    GameObject AnswerScreen;

    // Audio Clips \\
    [SerializeField] private AudioClip switchSnd;

    void Start()
    {
        //Assigning Items and Interactables
        AddToItemList("Key", "Potion", "KeyCard");

        AddToInteractableList("FireplaceSwitch", "PrisonSwitch", "Note", "Note2", "Note3", "Note4", "Note5",
           "DoubleDoorTrigger1", "DoubleDoorTrigger2", "DoubleDoorTrigger3", "Brew", "BookButton",
           "1KeyPad", "2KeyPad", "3KeyPad", "4KeyPad", "5KeyPad", "6KeyPad", "7KeyPad", "8KeyPad", 
           "9KeyPad", "Enter", "Reset");

        SceneManager.activeSceneChanged += ChangedActiveScene; //Call the ChangedActiveScene Method whenever the scene changes    
    }

    void Update()
    {
       if (player && Camera.main != null)
       {
          CheckForInteractions();
       }
    }

    void CheckForInteractions()
    {

       var PlayerScript = player.GetComponent<MyPlayer>(); 
     
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, 1.0f))
        {
            if (hit.transform.tag == "Key")
            {
                if (!showingPopup && !firePlace)
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x, hit.transform.position.y + 0.25f, hit.transform.position.z - 0.4f), Quaternion.identity);
                    tempPopup.GetComponentInChildren<TMP_Text>().SetText("Pick up Key");
                }

                if (!Inventory.CheckInventory("Key") && Input.GetMouseButtonDown(0) && !firePlace)
                {
                    if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
                    {
                        Items.Remove("Key");
                        Inventory.AddToInventory("Key", true, hit.transform.gameObject.name);
                        Destroy(tempPopup);
                        showingPopup = false;
                        Debug.Log("Key picked up");
                    }

                    else
                    {
                        Items.Remove("Key");
                        Inventory.AddToInventory("Key", true, hit.transform.gameObject.name);
                        Destroy(tempPopup);
                        showingPopup = false;
                        Debug.Log("Key picked up");
                    }
                }
            }

            else if (hit.transform.tag == "FireplaceSwitch")
            {
                if (!showingPopup)
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x + 0.5f, hit.transform.position.y + 0.9f, hit.transform.position.z), Quaternion.identity);

                    if (firePlace)
                    {
                        tempPopup.GetComponentInChildren<TMP_Text>().SetText("Turn off Fireplace");
                    }

                    else
                    {
                        tempPopup.GetComponentInChildren<TMP_Text>().SetText("Turn on Fireplace");
                    }
                }

                if (Interactables.Contains("FireplaceSwitch") && Input.GetMouseButtonDown(0))
                {
                    if (firePlace) //if fireplace is on
                    {
                        tempPopup.GetComponentInChildren<TMP_Text>().SetText("Turn on Fireplace"); //Update text

                        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
                        {
                            FireParticlesServer.ServerStopPlayingFireParticles();
                            firePlaceSounds.Stop();
                            Destroy(tempPopup);
                            showingPopup = false;
                        }

                        else
                        {
                            firePlace = false;
                            fireplaceEmitter.Stop();
                            firePlaceSounds.Stop();
                            Destroy(tempPopup);
                            showingPopup = false;
                        }

                        //switchSound.time = 0.45f;
                        // switchSound.Play();
                        AudioManager.instance.alterPitchEffect(switchSnd);
                    }

                    else
                    {
                        tempPopup.GetComponentInChildren<TMP_Text>().SetText("Turn off Fireplace"); //Update text

                        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
                        {
                            FireParticlesServer.ServerStartPlayingFireParticles();
                            firePlaceSounds.Play();
                            Destroy(tempPopup);
                            showingPopup = false;
                        }

                        else
                        {
                            firePlace = true;
                            fireplaceEmitter.Play();
                            firePlaceSounds.Play();
                            Destroy(tempPopup);
                            showingPopup = false;
                        }

                        //switchSound.time = 0.45f;
                        //switchSound.Play();
                        AudioManager.instance.alterPitchEffect(switchSnd);

                    }

                }
            }

            else if (hit.transform.tag == "PrisonSwitch")
            {
                if (!showingPopup)
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z), Quaternion.identity);
                    tempPopup.GetComponentInChildren<TMP_Text>().SetText("Use");
                }

                if (Interactables.Contains("PrisonSwitch") && Input.GetMouseButtonDown(0))
                {
                    if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
                    {
                        InteractionReplicate.OpenSingleDoor(SingleDoorPrison.gameObject.name);
                    }

                    else
                    {
                        SingleDoorPrison.SetBool("DoorOpen", true);
                    }

                    Destroy(tempPopup);
                    showingPopup = false;

                    //switchSound.time = 0.45f;
                    //switchSound.Play();  
                    AudioManager.instance.alterPitchEffect(switchSnd);

                }
            }

            else if (hit.transform.tag == "BookButton")
            {
                if (!showingPopup)
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x - 0.4f, hit.transform.position.y, hit.transform.position.z + 0.5f), Quaternion.identity);
                    tempPopup.GetComponentInChildren<TMP_Text>().SetText("Pull book");
                    // Debug.Log("Ah yies");
                }

                if (Interactables.Contains("BookButton") && Input.GetMouseButtonDown(0))
                {
                    if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
                    {
                        InteractionReplicate.OpenSingleDoor(ExitDoor.gameObject.name);
                    }

                    else
                    {
                        ExitDoor.SetBool("DoorOpen", true);
                    }

                    //switchSound.time = 0.45f;
                    //switchSound.Play();
                    AudioManager.instance.alterPitchEffect(switchSnd);

                }

            }

            else if (hit.transform.tag == "Note")
            {
                if (!showingPopup)
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x, hit.transform.position.y + 1.0f, hit.transform.position.z - 0.4f), Quaternion.identity);
                    tempPopup.GetComponentInChildren<TMP_Text>().SetText("Read Note");
                }

                if (Interactables.Contains("Note") && Input.GetMouseButtonDown(0) && !showingNote)
                {
                    NoteImage.enabled = true;
                    showingNote = true;
                }
                else if (showingNote && Input.GetMouseButtonDown(0))
                {
                    NoteImage.enabled = false;
                    showingNote = false;
                }
            }

            else if (hit.transform.tag == "Hint1")
            {
                if (!showingPopup)
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z), Quaternion.identity);
                    tempPopup.GetComponentInChildren<TMP_Text>().SetText("Find the key to open the gate");
                }

                if (Interactables.Contains("Hint1") && Input.GetMouseButtonDown(0) && !showingNote)
                {
                    showingNote = true;
                }
                else if (showingNote && Input.GetMouseButtonDown(0))
                {

                    showingNote = false;
                }
            }

            else if (hit.transform.tag == "DoubleDoorTrigger1")
            {
                if (!showingPopup && !DoubleDoorisOpen && Inventory.CheckInventory("Key"))
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x - 0.5f, hit.transform.position.y + 0.5f, hit.transform.position.z + 1.0f), Quaternion.identity);
                    tempPopup.GetComponentInChildren<TMP_Text>().SetText("Open Door");
                }

                else if (!showingPopup && !DoubleDoorisOpen && !Inventory.CheckInventory("Key"))
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x - 0.5f, hit.transform.position.y + 0.5f, hit.transform.position.z + 1.0f), Quaternion.identity);
                    tempPopup.GetComponentInChildren<TMP_Text>().SetText("Door is locked");
                }


                else if (Interactables.Contains("DoubleDoorTrigger1") && Input.GetMouseButtonDown(0) && !DoubleDoorisOpen && Inventory.CheckInventory("Key"))
                {
                    DoubleDoorisOpen = true;

                    if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
                    {
                        InteractionReplicate.OpenDoubleDoors(DoubleDoor[0].gameObject.name, DoubleDoor[1].gameObject.name);
                    }

                    else
                    {
                        DoubleDoor[0].SetBool("DoorOpen", true);
                        DoubleDoor[1].SetBool("DoorOpen", true);
                    }

                    Destroy(tempPopup);
                    showingPopup = false;
                }
            }

            else if (hit.transform.tag == "Potion")
            {
                if (!showingPopup)
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z), Quaternion.identity);
                    tempPopup.GetComponentInChildren<TMP_Text>().SetText("Pickup Potion");
                }

                else if (Items.Contains("Potion") && Input.GetMouseButtonDown(0) && Inventory.PotionCount < 4)
                {
                    if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
                    {
                        Inventory.AddToInventory("Potion" + Inventory.PotionCount.ToString(), true, hit.transform.parent.gameObject.name);
                        Debug.Log("Potion Count " + Inventory.PotionCount);
                    }

                    else
                    {
                        Destroy(hit.transform.parent.gameObject);
                        Inventory.AddToInventory("Potion" + Inventory.PotionCount.ToString(), true, hit.transform.parent.gameObject.name);
                        Debug.Log("Potion Count " + Inventory.PotionCount);
                    }
                }
            }

            else if (hit.transform.tag == "Brew")
            {
                Debug.Log("Potion1 is... " + Inventory.CheckInventory("Potion1"));
                Debug.Log("Potion2 is... " + Inventory.CheckInventory("Potion2"));
                Debug.Log("Potion3 is... " + Inventory.CheckInventory("Potion3"));

                if (!showingPopup)
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z), Quaternion.identity);
                    tempPopup.GetComponentInChildren<TMP_Text>().SetText("Brew Potions " + Inventory.PotionCount + " /3");
                }

                else if (Inventory.CheckInventory("Potion1") &&
                    Inventory.CheckInventory("Potion2") &&
                    Inventory.CheckInventory("Potion3") &&
                    Input.GetMouseButtonDown(0) && !DoubleDoorisOpen)
                {
                    DoubleDoorisOpen = true;

                    if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
                    {
                        InteractionReplicate.OpenDoubleDoors(DoubleDoor[0].gameObject.name, DoubleDoor[1].gameObject.name);
                    }

                    else
                    {
                        DoubleDoor[0].SetBool("DoorOpen", true);
                        DoubleDoor[1].SetBool("DoorOpen", true);
                    }
                }
            }

            else if (hit.transform.tag == "1KeyPad")
            {
                if (!showingPopup)
                {
                    Debug.Log("On Key 1");
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z - 0.2f), Quaternion.identity);
                    tempPopup.GetComponentInChildren<TMP_Text>().SetText("Press 1");
                    tempPopup.GetComponentInChildren<TMP_Text>().fontSize = 20;
                }

                else if (Interactables.Contains("1KeyPad") && Input.GetMouseButtonDown(0) && AnswerScreen.GetComponentInChildren<TMP_Text>().text.Length < 9)
                {
                    if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
                    {
                        InteractionReplicate.UpdateAnswerScreenOnServer("1");
                    }

                    else
                    {
                        AnswerScreen.GetComponentInChildren<TMP_Text>().SetText(AnswerScreen.GetComponentInChildren<TMP_Text>().text + "1");
                    }
                }
            }

            else if (hit.transform.tag == "2KeyPad")
            {
                if (!showingPopup)
                {
                    Debug.Log("On Key 2");
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z - 0.2f), Quaternion.identity);
                    tempPopup.GetComponentInChildren<TMP_Text>().SetText("Press 2");
                    tempPopup.GetComponentInChildren<TMP_Text>().fontSize = 20;
                }

                else if (Interactables.Contains("2KeyPad") && Input.GetMouseButtonDown(0) && AnswerScreen.GetComponentInChildren<TMP_Text>().text.Length < 9)
                {

                    if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
                    {
                        InteractionReplicate.UpdateAnswerScreenOnServer("2");
                    }

                    else
                    {
                        AnswerScreen.GetComponentInChildren<TMP_Text>().SetText(AnswerScreen.GetComponentInChildren<TMP_Text>().text + "2");
                    }
                }
            }


            else if (hit.transform.tag == "3KeyPad")
            {
                if (!showingPopup)
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z - 0.2f), Quaternion.identity);
                    tempPopup.GetComponentInChildren<TMP_Text>().SetText("Press 3");
                    tempPopup.GetComponentInChildren<TMP_Text>().fontSize = 20;
                }

                else if (Interactables.Contains("3KeyPad") && Input.GetMouseButtonDown(0) && AnswerScreen.GetComponentInChildren<TMP_Text>().text.Length < 9)
                {
                    if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
                    {
                        InteractionReplicate.UpdateAnswerScreenOnServer("3");
                    }

                    else
                    {
                        AnswerScreen.GetComponentInChildren<TMP_Text>().SetText(AnswerScreen.GetComponentInChildren<TMP_Text>().text + "3");
                    }
                }
            }

            else if (hit.transform.tag == "4KeyPad")
            {
                if (!showingPopup)
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z - 0.2f), Quaternion.identity);
                    tempPopup.GetComponentInChildren<TMP_Text>().SetText("Press 4");
                    tempPopup.GetComponentInChildren<TMP_Text>().fontSize = 20;
                }

                else if (Interactables.Contains("4KeyPad") && Input.GetMouseButtonDown(0) && AnswerScreen.GetComponentInChildren<TMP_Text>().text.Length < 9)
                {
                    if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
                    {
                        InteractionReplicate.UpdateAnswerScreenOnServer("4");
                    }

                    else
                    {
                        AnswerScreen.GetComponentInChildren<TMP_Text>().SetText(AnswerScreen.GetComponentInChildren<TMP_Text>().text + "4");
                    }
                }
            }

            else if (hit.transform.tag == "5KeyPad")
            {
                if (!showingPopup)
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z - 0.2f), Quaternion.identity);
                    tempPopup.GetComponentInChildren<TMP_Text>().SetText("Press 5");
                    tempPopup.GetComponentInChildren<TMP_Text>().fontSize = 20;
                }

                else if (Interactables.Contains("5KeyPad") && Input.GetMouseButtonDown(0) && AnswerScreen.GetComponentInChildren<TMP_Text>().text.Length < 9)
                {
                    if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
                    {
                        InteractionReplicate.UpdateAnswerScreenOnServer("5");
                    }

                    else
                    {
                        AnswerScreen.GetComponentInChildren<TMP_Text>().SetText(AnswerScreen.GetComponentInChildren<TMP_Text>().text + "5");
                    }
                }
            }

            else if (hit.transform.tag == "6KeyPad")
            {
                if (!showingPopup)
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z - 0.2f), Quaternion.identity);
                    tempPopup.GetComponentInChildren<TMP_Text>().SetText("Press 6");
                    tempPopup.GetComponentInChildren<TMP_Text>().fontSize = 20;
                }

                else if (Interactables.Contains("6KeyPad") && Input.GetMouseButtonDown(0) && AnswerScreen.GetComponentInChildren<TMP_Text>().text.Length < 9)
                {
                    if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
                    {
                        InteractionReplicate.UpdateAnswerScreenOnServer("6");
                    }

                    else
                    {
                        AnswerScreen.GetComponentInChildren<TMP_Text>().SetText(AnswerScreen.GetComponentInChildren<TMP_Text>().text + "6");
                    }
                }
            }

            else if (hit.transform.tag == "7KeyPad")
            {
                if (!showingPopup)
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z - 0.2f), Quaternion.identity);
                    tempPopup.GetComponentInChildren<TMP_Text>().SetText("Press 7");
                    tempPopup.GetComponentInChildren<TMP_Text>().fontSize = 20;
                }

                else if (Interactables.Contains("7KeyPad") && Input.GetMouseButtonDown(0) && AnswerScreen.GetComponentInChildren<TMP_Text>().text.Length < 9)
                {
                    if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
                    {
                        InteractionReplicate.UpdateAnswerScreenOnServer("7");
                    }

                    else
                    {
                        AnswerScreen.GetComponentInChildren<TMP_Text>().SetText(AnswerScreen.GetComponentInChildren<TMP_Text>().text + "7");
                    }
                }
            }

            else if (hit.transform.tag == "8KeyPad")
            {
                if (!showingPopup)
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z - 0.2f), Quaternion.identity);
                    tempPopup.GetComponentInChildren<TMP_Text>().SetText("Press 8");
                    tempPopup.GetComponentInChildren<TMP_Text>().fontSize = 20;
                }

                else if (Interactables.Contains("8KeyPad") && Input.GetMouseButtonDown(0) && AnswerScreen.GetComponentInChildren<TMP_Text>().text.Length < 9)
                {
                    if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
                    {
                        InteractionReplicate.UpdateAnswerScreenOnServer("8");
                    }

                    else
                    {
                        AnswerScreen.GetComponentInChildren<TMP_Text>().SetText(AnswerScreen.GetComponentInChildren<TMP_Text>().text + "8");
                    }
                }
            }

            else if (hit.transform.tag == "9KeyPad")
            {
                if (!showingPopup)
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z - 0.2f), Quaternion.identity);
                    tempPopup.GetComponentInChildren<TMP_Text>().SetText("Press 9");
                    tempPopup.GetComponentInChildren<TMP_Text>().fontSize = 20;
                }

                else if (Interactables.Contains("9KeyPad") && Input.GetMouseButtonDown(0) && AnswerScreen.GetComponentInChildren<TMP_Text>().text.Length < 9)
                {
                    if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
                    {
                        InteractionReplicate.UpdateAnswerScreenOnServer("9");
                    }

                    else
                    {
                        AnswerScreen.GetComponentInChildren<TMP_Text>().SetText(AnswerScreen.GetComponentInChildren<TMP_Text>().text + "9");
                    }
                }
            }

            else if (hit.transform.tag == "Enter")
            {
                if (!showingPopup)
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z - 0.2f), Quaternion.identity);
                    tempPopup.GetComponentInChildren<TMP_Text>().SetText("Press Enter");
                    tempPopup.GetComponentInChildren<TMP_Text>().fontSize = 20;
                }

                else if (Interactables.Contains("Enter") && Input.GetMouseButtonDown(0))
                {
                    if (GameObject.Find("DoubleDoor"))
                    {
                        var passcodescript = GameObject.Find("DoubleDoor").GetComponent<PasscodeScript>();

                        if (AnswerScreen.GetComponentInChildren<TMP_Text>().text == passcodescript.GetPassCode())
                        {
                            if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
                            {
                                InteractionReplicate.OpenDoubleDoors(DoubleDoor[0].gameObject.name, DoubleDoor[1].gameObject.name);
                            }

                            else
                            {
                                DoubleDoor[0].SetBool("DoorOpen", true);
                                DoubleDoor[1].SetBool("DoorOpen", true);
                            }
                        }

                        else
                        {

                        }
                    }
                }
            }

            else if (hit.transform.tag == "Reset")
            {
                if (!showingPopup)
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z - 0.2f), Quaternion.identity);
                    tempPopup.GetComponentInChildren<TMP_Text>().SetText("Press Reset");
                    tempPopup.GetComponentInChildren<TMP_Text>().fontSize = 20;
                }

                else if (Interactables.Contains("Reset") && Input.GetMouseButtonDown(0))
                {
                    AnswerScreen.GetComponentInChildren<TMP_Text>().SetText("");
                }
            }

            else if (hit.transform.tag == "KeyCard")
            {
                if (!showingPopup)
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z - 0.2f), Quaternion.identity);
                    tempPopup.GetComponentInChildren<TMP_Text>().SetText("Pick up KeyCard");
                    tempPopup.GetComponentInChildren<TMP_Text>().fontSize = 20;
                }

                if (!Inventory.CheckInventory("KeyCard") && Input.GetMouseButtonDown(0))
                {
                    if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
                    {
                        Items.Remove("KeyCard");
                        Inventory.AddToInventory("KeyCard", true, hit.transform.gameObject.name);
                        Destroy(tempPopup);
                        showingPopup = false;
                        Debug.Log("KeyCard picked up");
                    }

                    else
                    {
                        Items.Remove("KeyCard");
                        Inventory.AddToInventory("KeyCard", true, hit.transform.gameObject.name);
                        Destroy(tempPopup);
                        showingPopup = false;
                        Debug.Log("KeyCard picked up");
                    }
                }
            }
        }


        else if (showingPopup)
        {
            showingPopup = false;
            Destroy(tempPopup);
        }

        else if (showingNote)
        {
            if (NoteImage)
            {
                NoteImage.enabled = false;
            }

            showingNote = false;
        }
    }

    void AddToItemList(params string[] list_)
    {
        foreach(string item in list_)
        {
            Items.Add(item);
        }
    }

    void AddToInteractableList(params string[] list_)
    {
        foreach (string interactable in list_)
        {      
            Interactables.Add(interactable);
        }
    }

    private void ChangedActiveScene(Scene current, Scene next)
    {
        if (next.name.Contains("EscapeRoom")) //When the Scene changes make sure its an EscapeRoom Scene, and assign interactables during runtime
        {
            StartCoroutine(SetInteractables());
        }
    }

    IEnumerator SetInteractables()
    {
        yield return new WaitForSeconds(1.0f);

        DoubleDoorisOpen = false; //Reset DoubleDoorisOpen boolean variable

        if (GameObject.FindGameObjectWithTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (GameObject.FindGameObjectWithTag("FireplaceSwitch"))
        {
            fireplaceEmitter = GameObject.FindGameObjectWithTag("FireParticles").GetComponent<ParticleSystem>();
            fireParticleView = fireplaceEmitter.GetComponent<PhotonView>(); 
            firePlaceSounds = GameObject.FindGameObjectWithTag("FireParticles").GetComponent<AudioSource>();
        }

        if (GameObject.FindGameObjectWithTag("LeftDoubleDoor") && GameObject.FindGameObjectWithTag("RightDoubleDoor"))
        {
            DoubleDoor[0] = GameObject.FindGameObjectWithTag("LeftDoubleDoor").GetComponent<Animator>();
            DoubleDoor[1] = GameObject.FindGameObjectWithTag("RightDoubleDoor").GetComponent<Animator>();
        }

        if (GameObject.FindGameObjectWithTag("PrisonDoor"))
        {
            SingleDoorPrison = GameObject.FindGameObjectWithTag("PrisonDoor").GetComponent<Animator>();
        }

        if (GameObject.FindGameObjectWithTag("SingleDoor"))
        {
            ExitDoor = GameObject.FindGameObjectWithTag("SingleDoor").GetComponent<Animator>();
        }

        if (GameObject.FindGameObjectWithTag("AnswerScreen"))
        {
            AnswerScreen = GameObject.FindGameObjectWithTag("AnswerScreen");
        }

        if (GameObject.FindGameObjectWithTag("NoteImage"))
        {
            NoteImage = GameObject.FindGameObjectWithTag("NoteImage").GetComponent<Image>();
        }
    }
}
