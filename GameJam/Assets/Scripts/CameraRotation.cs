using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraRotation : MonoBehaviourPun
{
    Vector2 _mouseAbsolute;
    Vector2 _smoothMouse;

    public Vector2 clampInDegrees = new Vector2(360, 180);
    public bool lockCursor;
    public Vector2 sensitivity = new Vector2(2, 2);
    public Vector2 smoothing = new Vector2(3, 3);
    public Vector2 targetDirection;
    public Vector2 targetCharacterDirection;

    public Transform characterBody;

    // Start is called before the first frame update
    void Start()
    {
        //Ensure the cursor is locked if set
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        Cursor.visible = true;

        // Set target direction to the camera's initial orientation.
        targetDirection = transform.localRotation.eulerAngles;

        // Set target direction for the character body to its inital state.
        if (characterBody)
        {
            Debug.Log("Found Body");
            targetCharacterDirection = characterBody.transform.localRotation.eulerAngles;
        }

        characterBody = transform.parent.gameObject.transform;
    }

    void LateUpdate()
    {
        if (characterBody)
        {
            if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom && base.photonView.IsMine)
            {
                SmoothCamera();
            }

            else if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom && !base.photonView.IsMine)
            {
                Destroy(this);
            }

            else
            {
                SmoothCamera();
            }
        }
    }

    void SmoothCamera() 
    {
        //Allow the script to clamp based on a desired target value.
        var targetOrientation = Quaternion.Euler(targetDirection);
        var targetCharacterOrientation = Quaternion.Euler(targetCharacterDirection);

        //Get raw mouse input for a cleaner reading on more sensitive mice
        var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        //Scale input against the sensitivity setting and multiply that against the smoothing value
        mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity.x * smoothing.x, sensitivity.y * smoothing.y));

        //Interpolate mouse movement over time to apply smoothing delta
        _smoothMouse.x = Mathf.Lerp(_smoothMouse.x, mouseDelta.x, 1f / smoothing.x);
        _smoothMouse.y = Mathf.Lerp(_smoothMouse.y, mouseDelta.y, 1f / smoothing.y);

        // Find the absolute mouse movement value from point zero
        _mouseAbsolute += _smoothMouse;

        //Clamp and apply the local x value first, so as not to be affected by world transforms
        if (clampInDegrees.x < 360)
        {
            _mouseAbsolute.x = Mathf.Clamp(_mouseAbsolute.x, -clampInDegrees.x * 0.5f, clampInDegrees.x * 0.5f);
        }

        // Then clamp and apply thr global y value.
        if (clampInDegrees.y < 360)
        {
            _mouseAbsolute.y = Mathf.Clamp(_mouseAbsolute.y, -clampInDegrees.y * 0.5f, clampInDegrees.y * 0.5f);
        }

        transform.localRotation = Quaternion.AngleAxis(-_mouseAbsolute.y, targetOrientation * Vector3.right) * targetOrientation;

        // if theres a character body that acts as a parent to the camera
        if (characterBody)
        {
            var yRotation = Quaternion.AngleAxis(_mouseAbsolute.x, Vector3.up);
            characterBody.transform.localRotation = yRotation * targetCharacterOrientation;
        }
        else
        {
            var yRotation = Quaternion.AngleAxis(_mouseAbsolute.x, transform.InverseTransformDirection(Vector3.up));
            transform.localRotation *= yRotation;
        }
    }
}
