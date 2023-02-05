using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceManager : MonoBehaviour
{
    private static AmbienceManager Instance;

    public Ambience titleAmbience;
    public Ambience levelAmbience;

    private Ambience currentAmbience;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void SwitchTo(Ambience ambience)
    {
        
    }
}
