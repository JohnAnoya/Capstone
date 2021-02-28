using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScaler : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        if(other.tag.Equals("DraggableCube"))
        {

            other.gameObject.transform.GetChild(0).GetComponent<CubeProperties>().ReSize(); 
        }
    }
}
