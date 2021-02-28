using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CubeScanner : MonoBehaviour
{
    [SerializeField]
    private string RequiredColouredCube; 

    [SerializeField]
    private Animator SciFiDoubleDoor;
    private bool DoubleDoorisOpen = false;


    [SerializeField]
    private bool isMultiPuzzle = false;
    public bool isOnPad; 

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.transform.GetChild(0).tag.Equals(RequiredColouredCube) &&
            !collision.gameObject.transform.GetChild(0).GetComponent<CubeProperties>().isDraggingCube
            && !DoubleDoorisOpen)
        {
            if(SciFiDoubleDoor != null && !isMultiPuzzle)
            {
                if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
                {
                    InteractionReplicate.OpenSciFiDoubleDoors(SciFiDoubleDoor.gameObject.name);
                    DoubleDoorisOpen = true;
                    isOnPad = true; 
                }

                else
                {
                    SciFiDoubleDoor.SetBool("DoorOpen", true);
                    DoubleDoorisOpen = true;
                    isOnPad = true; 
                }
            }    

            else
            {
                isOnPad = true; 
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.transform.GetChild(0).tag.Equals(RequiredColouredCube) && DoubleDoorisOpen)
        {
            if (SciFiDoubleDoor != null && !isMultiPuzzle)
            {
                if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
                {
                    InteractionReplicate.CloseSciFiDoubleDoors(SciFiDoubleDoor.gameObject.name);
                    DoubleDoorisOpen = false;
                    isOnPad = false; 
                }

                else
                {
                    SciFiDoubleDoor.SetBool("DoorOpen", false);
                    DoubleDoorisOpen = false;
                    isOnPad = false; 
                }
            }

            else
            {
                isOnPad = false;
            }
        }
    }
}
