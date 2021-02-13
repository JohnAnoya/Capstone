using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstantiate : MonoBehaviour
{
    [SerializeField]
    private GameObject player_;

    private void Awake()
    {
        Vector2 offset = Random.insideUnitCircle * 3.0f;
        Vector3 position = new Vector3(transform.position.x + offset.x, transform.position.y, transform.position.z); 
        NetworkingManager.InstantiateOverNetwork(player_, position, Quaternion.identity);
        GameObject.FindObjectOfType<Camera>().enabled = true;
    }
}
