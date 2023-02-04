using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Things that should exist for the lifetime of the app
/// </summary>
public class App : MonoBehaviour
{
    private static App Instance = null;
    public static bool Exists => Instance != null;

    private static List<Action> requestsForApp = new List<Action>();

    public static void Request(Action onAppExists)
    {
        if (Instance != null)
        {
            onAppExists?.Invoke();
        }
        else
        {
            requestsForApp.Add(onAppExists);
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        foreach(Action existsCallback in requestsForApp)
        {
            existsCallback?.Invoke();
        }
        requestsForApp.Clear();
    }
}
