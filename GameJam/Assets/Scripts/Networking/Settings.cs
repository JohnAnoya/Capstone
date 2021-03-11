using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Settings : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI NickNameText;
    [SerializeField]
    private TextMeshProUGUI ErrorText;

    private string NickName; 

    public bool CheckIfHasUserName()
    {    
        if(!PlayerPrefs.HasKey("Username"))
        {
            return false; 
        }

        else
        {
            return true; 
        }
    }

    public void SetNewNickName ()
    {
        NickNameText = GameObject.FindGameObjectWithTag("UsernameText").GetComponent<TextMeshProUGUI>();
        ErrorText = GameObject.FindGameObjectWithTag("ErrorText").GetComponent<TextMeshProUGUI>(); 
        if (NickNameText.text.Length < 3)
        {
            StartCoroutine(ShowErrorMessage("Username must be longer than 2 characters!"));
        }

        else if(NickNameText.text.Length > 15)
        {
            StartCoroutine(ShowErrorMessage("Username cannot be longer than 14 characters!"));
        }

        else
        {
            PlayerPrefs.SetString("Username", NickNameText.text);
        }
    }

    public string NewNickName()
    {
        NickName = PlayerPrefs.GetString("Username");
        return NickName;
    }

    IEnumerator ShowErrorMessage(string ErrorMessage_)
    {
        ErrorText.text = ErrorMessage_;
        yield return new WaitForSeconds(2.0f);
        ErrorText.text = "";
    }
}
