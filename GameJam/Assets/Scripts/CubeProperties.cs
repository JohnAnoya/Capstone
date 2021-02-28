using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeProperties : MonoBehaviour
{
    [SerializeField]
    private float MaxSize;
    [SerializeField]
    private float CurrentSize;

    public void ReSize()
    {
        Vector3 newScale = new Vector3(MaxSize, MaxSize, MaxSize);
        transform.localScale = Vector3.Lerp(transform.localScale, newScale, 1.0f * Time.deltaTime);
    }
}
