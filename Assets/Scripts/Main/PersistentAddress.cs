using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentAddress : MonoBehaviour
{
    public static PersistentAddress Instance;

    public string IPAddress = "localhost";

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

}
