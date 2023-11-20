using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerPersist : MonoBehaviour
{
    public static ServerPersist Instance;

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
