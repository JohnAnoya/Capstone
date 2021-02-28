using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FpsCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI fpsDisplay;

    [SerializeField] TextMeshProUGUI averageFpsDisplay;
    int framesPassed = 0;
    float fpsTotal = 0f;

    //singleton setup
    private static FpsCounter instance_;

    public static FpsCounter Instance { get { return instance_; } }

	private void Awake()
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

	// Start is called before the first frame update
	void Start()
    {
        Application.targetFrameRate = 144;
    }

    // Update is called once per frame
    void Update()
    {
        float fps = 1 / Time.unscaledDeltaTime;
        fpsDisplay.text = "FPS: " + fps;

        fpsTotal += fps;
        framesPassed++;
        averageFpsDisplay.text = "Average FPS: " + (fpsTotal / framesPassed);
    }
}
