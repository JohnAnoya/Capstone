using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneName;

    public Animator transition;
    public float transitionTime = 1f;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("ENTERED COLLISION COMMAND");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("COLLIDED");
            SceneManager.LoadScene(sceneName);
        }
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
