using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBackToMainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BackToMainMenuScene());
    }
    
    IEnumerator BackToMainMenuScene()
    {
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene("MainMenu");
    }
}
