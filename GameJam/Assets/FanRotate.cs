using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanRotate : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(1000.0f * Time.deltaTime, 0.0f, 0.0f); 
    }
}
