using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InteractionSystem : MonoBehaviour
{
    List<string> Items = new List<string>();
    List<string> Interactables = new List<string>();

    public GameObject InteractionPopUp;
    GameObject tempPopup;
    bool showingPopup = false;

    bool firePlace = true;
    public ParticleSystem fireplaceEmitter;

    [SerializeField]
    Image note1;
    [SerializeField]
    Image note2;
    [SerializeField]
    Image note3;
    [SerializeField]
    Image note4;
    [SerializeField]
    Image note5;
    bool showingNote = false;
    //[SerializeField]
    //Image hintBook;

    [SerializeField] private Animator RightDoubleDoor = null;
    [SerializeField] private Animator LeftDoubleDoor = null;
    [SerializeField] private Animator SingleDoorPrison = null;
    [SerializeField] private Animator ExitDoor = null;
    bool DoubleDoorisOpen = false;

    int PotionCount = 0;

    AudioSource switchSound;


    GameObject AnswerScreen; 

    // Start is called before the first frame update
    void Start()
    {
        Items.Add("Key");
        Items.Add("Potion");
        Interactables.Add("FireplaceSwitch");
        Interactables.Add("PrisonSwitch");
        Interactables.Add("Note1");
        Interactables.Add("Note2");
        Interactables.Add("Note3");
        Interactables.Add("Note4");
        Interactables.Add("Note5");
        Interactables.Add("DoubleDoorTrigger1");
        Interactables.Add("DoubleDoorTrigger2");
        Interactables.Add("DoubleDoorTrigger3");
        Interactables.Add("DoubleDoorTrigger4");
        Interactables.Add("Brew");
        Interactables.Add("BookButton");

        //* KEYPAD STUFF *//
        Interactables.Add("1KeyPad");
        Interactables.Add("2KeyPad");
        Interactables.Add("3KeyPad");
        Interactables.Add("4KeyPad");
        Interactables.Add("5KeyPad");
        Interactables.Add("6KeyPad");
        Interactables.Add("7KeyPad");
        Interactables.Add("8KeyPad");
        Interactables.Add("9KeyPad");
        Interactables.Add("Enter");
        Interactables.Add("Reset");

        //Interactables.Add("Book_Open");   


        if (GameObject.Find("FireplaceSwitch"))
        {
            switchSound = GameObject.Find("FireplaceSwitch").GetComponent<AudioSource>();
        }

        else if (GameObject.Find("PrisonSwitch"))
        {
            switchSound = GameObject.Find("PrisonSwitch").GetComponent<AudioSource>();
        }

        else if (GameObject.Find("AnswerScreen"))
        {
            Debug.Log("Test");
            AnswerScreen = GameObject.Find("AnswerScreen");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckForInteractions(); 
    }

    void CheckForInteractions()
    {
        var PlayerScript = gameObject.GetComponent<Player>();

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

                if (!PlayerScript.CheckInventory("Key") && Input.GetMouseButtonDown(0) && !firePlace)
                {
                    Items.Remove("Key");
                    PlayerScript.AddToInventory("Key");
                    Destroy(hit.transform.gameObject);
                    Destroy(tempPopup);
                    showingPopup = false;
                    Debug.Log("Key picked up");
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
                        firePlace = false;
                        fireplaceEmitter.Stop();

                        Destroy(tempPopup);
                        showingPopup = false;

                        switchSound.time = 0.45f;
                        switchSound.Play();
                    }

                    else
                    {
                        tempPopup.GetComponentInChildren<TMP_Text>().SetText("Turn off Fireplace"); //Update text
                        firePlace = true;
                        fireplaceEmitter.Play();

                        Destroy(tempPopup);
                        showingPopup = false;

                        switchSound.time = 0.45f;
                        switchSound.Play();
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
                    switchSound.time = 0.45f;
                    switchSound.Play();


                    SingleDoorPrison.Play("SingleDoorOpen", 0, 0.0f);
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
                    switchSound.time = 0.45f;
                    switchSound.Play();

                    ExitDoor.Play("ExitDoorOpen", 0, 0.0f);
                }

            }

            else if (hit.transform.tag == "Note1")
            {
                if (!showingPopup)
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x, hit.transform.position.y + 1.0f, hit.transform.position.z - 0.4f), Quaternion.identity);
                    tempPopup.GetComponentInChildren<TMP_Text>().SetText("Read Note");
                }

                if (Interactables.Contains("Note1") && Input.GetMouseButtonDown(0) && !showingNote)
                {
                    note1.enabled = true;
                    showingNote = true;
                }
                else if (showingNote && Input.GetMouseButtonDown(0))
                {
                    note1.enabled = false;
                    showingNote = false;
                }
            }


            else if (hit.transform.tag == "Note2")
            {
                if (!showingPopup)
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x, hit.transform.position.y + 0.3f, hit.transform.position.z - 0.4f), Quaternion.identity);
                    tempPopup.GetComponentInChildren<TMP_Text>().SetText("Read Note");
                }

                if (Interactables.Contains("Note2") && Input.GetMouseButtonDown(0) && !showingNote)
                {
                    note2.enabled = true;
                    showingNote = true;
                }
                else if (showingNote && Input.GetMouseButtonDown(0))
                {
                    note2.enabled = false;
                    showingNote = false;
                }
            }

            else if (hit.transform.tag == "Note3")
            {
                if (!showingPopup)
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x, hit.transform.position.y + 1.0f, hit.transform.position.z - 0.4f), Quaternion.identity);
                    tempPopup.GetComponentInChildren<TMP_Text>().SetText("Read Note");
                }

                if (Interactables.Contains("Note3") && Input.GetMouseButtonDown(0) && !showingNote)
                {
                    note3.enabled = true;
                    showingNote = true;
                }
                else if (showingNote && Input.GetMouseButtonDown(0))
                {
                    note3.enabled = false;
                    showingNote = false;
                }
            }


            else if (hit.transform.tag == "Note4")
            {
                if (!showingPopup)
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z), Quaternion.identity);
                    tempPopup.GetComponentInChildren<TMP_Text>().SetText("Read Note");
                }

                if (Interactables.Contains("Note4") && Input.GetMouseButtonDown(0) && !showingNote)
                {
                    note4.enabled = true;
                    showingNote = true;
                }
                else if (showingNote && Input.GetMouseButtonDown(0))
                {
                    note4.enabled = false;
                    showingNote = false;
                }
            }

            else if (hit.transform.tag == "Note5")
            {
                if (!showingPopup)
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z), Quaternion.identity);
                    tempPopup.GetComponentInChildren<TMP_Text>().SetText("Read Note");
                }

                if (Interactables.Contains("Note5") && Input.GetMouseButtonDown(0) && !showingNote)
                {
                    note5.enabled = true;
                    showingNote = true;
                }
                else if (showingNote && Input.GetMouseButtonDown(0))
                {
                    note5.enabled = false;
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
                if (!showingPopup && !DoubleDoorisOpen && PlayerScript.CheckInventory("Key"))
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x - 0.5f, hit.transform.position.y + 0.5f, hit.transform.position.z + 1.0f), Quaternion.identity);
                    tempPopup.GetComponentInChildren<TMP_Text>().SetText("Open Door");
                }

                else if (!showingPopup && !DoubleDoorisOpen && !PlayerScript.CheckInventory("Key"))
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x - 0.5f, hit.transform.position.y + 0.5f, hit.transform.position.z + 1.0f), Quaternion.identity);
                    tempPopup.GetComponentInChildren<TMP_Text>().SetText("Door is locked");
                }


                else if (Interactables.Contains("DoubleDoorTrigger1") && Input.GetMouseButtonDown(0) && !DoubleDoorisOpen && PlayerScript.CheckInventory("Key"))
                {
                    DoubleDoorisOpen = true;
                    RightDoubleDoor.Play("RightDoubleDoorOpen", 0, 0.0f);
                    LeftDoubleDoor.Play("LeftDoubleDoorOpen", 0, 0.0f);
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

                else if (Items.Contains("Potion") && Input.GetMouseButtonDown(0) && PotionCount < 4)
                {
                    Destroy(hit.transform.parent.gameObject);
                    PotionCount += 1;
                    PlayerScript.AddToInventory("Potion" + PotionCount.ToString());
                    Debug.Log("Potion Count " + PotionCount);
                }
            }

            else if (hit.transform.tag == "Brew")
            {
                if (!showingPopup)
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z), Quaternion.identity);
                    tempPopup.GetComponentInChildren<TMP_Text>().SetText("Brew Potions " + PotionCount + " /3");
                }

                else if (PlayerScript.CheckInventory("Potion1") &&
                   PlayerScript.CheckInventory("Potion2") &&
                   PlayerScript.CheckInventory("Potion3") &&
                   Input.GetMouseButtonDown(0) && !DoubleDoorisOpen)
                {
                    DoubleDoorisOpen = true;
                    RightDoubleDoor.Play("RightDoubleDoorOpen", 0, 0.0f);
                    LeftDoubleDoor.Play("LeftDoubleDoorOpen", 0, 0.0f);
                    Debug.Log("Player has all potions and is trying to brew");
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
                    AnswerScreen.GetComponentInChildren<TMP_Text>().SetText(AnswerScreen.GetComponentInChildren<TMP_Text>().text + "1");
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
                    AnswerScreen.GetComponentInChildren<TMP_Text>().SetText(AnswerScreen.GetComponentInChildren<TMP_Text>().text + "2");
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
                    AnswerScreen.GetComponentInChildren<TMP_Text>().SetText(AnswerScreen.GetComponentInChildren<TMP_Text>().text + "3");
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
                    AnswerScreen.GetComponentInChildren<TMP_Text>().SetText(AnswerScreen.GetComponentInChildren<TMP_Text>().text + "4");
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
                    AnswerScreen.GetComponentInChildren<TMP_Text>().SetText(AnswerScreen.GetComponentInChildren<TMP_Text>().text + "5");
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
                    AnswerScreen.GetComponentInChildren<TMP_Text>().SetText(AnswerScreen.GetComponentInChildren<TMP_Text>().text + "6");
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
                    AnswerScreen.GetComponentInChildren<TMP_Text>().SetText(AnswerScreen.GetComponentInChildren<TMP_Text>().text + "7");
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
                    AnswerScreen.GetComponentInChildren<TMP_Text>().SetText(AnswerScreen.GetComponentInChildren<TMP_Text>().text + "8");
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
                    AnswerScreen.GetComponentInChildren<TMP_Text>().SetText(AnswerScreen.GetComponentInChildren<TMP_Text>().text + "9");
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
                            RightDoubleDoor.Play("RightDoubleDoorOpen", 0, 0.0f);
                            LeftDoubleDoor.Play("LeftDoubleDoorOpen", 0, 0.0f);
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
        }

        else if (showingPopup)
        {
            showingPopup = false;
            Destroy(tempPopup);
        }

        else if (showingNote)
        {
            if (note1)
            {
                note1.enabled = false;
            }

            else if (note2)
            {
                note2.enabled = false;
            }

            else if (note3)
            {
                note3.enabled = false;
            }

            else if (note4)
            {
                note4.enabled = false;
            }

            else if (note5)
            {
                note5.enabled = false;
            }

            showingNote = false;
        }


    }
}
