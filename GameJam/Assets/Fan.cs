using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        Debug.Log("Test");
        if (other.gameObject.tag.Equals("DraggableCube") && !other.gameObject.transform.GetChild(0).GetComponent<CubeProperties>().isDraggingCube)
        {
            Debug.Log("Cube on Fan");

            Vector3 newVelocity = other.gameObject.GetComponent<Rigidbody>().velocity;
            newVelocity.y += 1.1f;
            other.gameObject.GetComponent<Rigidbody>().velocity = newVelocity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(1000.0f * Time.deltaTime, 0.0f, 0.0f);
    }
}
