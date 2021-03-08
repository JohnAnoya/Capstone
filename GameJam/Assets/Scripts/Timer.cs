using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    static public float Seconds = 0;
    static public float Minutes = 10;
    public TMP_Text timeDisplay;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 6)
        {

            Minutes = 10;
        }
    }

    // Update is called once per frame
    void Update()
    {
       
       
        
           
        

        timeDisplay.text = Minutes.ToString() + "." + Seconds.ToString();
        if (Seconds <= 0)
        {
            Seconds = 59;
            if (Minutes >= 1)
            {
                Minutes--;
            }
            else
            {
                Minutes = 0;
                Seconds = 0;
                timeDisplay.text =  "GAMER OVER";
                SceneManager.LoadScene("LosingScene");
            }
        }
        else
        {
            Seconds -= Time.deltaTime;
        }

    }
}
