using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraRotation : MonoBehaviourPun
{
    public Transform Player; 
    float MouseSensitivity = 130.0f;
    float XRotate = 0.0f;
    float yRotate = 0.0f;

     void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (base.photonView.IsMine)
        {
            float mouseXAxis = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
            float mouseYAxis = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

            XRotate -= mouseYAxis;
            XRotate = Mathf.Clamp(XRotate, -90f, 90f);
            yRotate += mouseXAxis;

            Player.localRotation = Quaternion.Euler(0.0f, yRotate, 0.0f);
            transform.localRotation = Quaternion.Euler(XRotate, 0f, 0f);
        }
    }
}
