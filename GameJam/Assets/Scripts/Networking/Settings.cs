using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField]
    private string DefaultNickName = "Ali";

    public string NewNickName()
    {
        int RandomInt = Random.Range(1, 999);
        return DefaultNickName + RandomInt.ToString();      
    }
}
