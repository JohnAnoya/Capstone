using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseCursor : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None; 
        var cursorPos = Input.mousePosition;
        cursorPos.z = 10.0f;
        cursorPos = Camera.main.ScreenToWorldPoint(cursorPos);

        transform.position = new Vector2(cursorPos.x, cursorPos.y); 
    }
}
