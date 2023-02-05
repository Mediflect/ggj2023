using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalAudio : MonoBehaviour
{
    private static GlobalAudio Instance;

    public AudioSource startSfx;
    public AudioSource breakSfx;
    public AudioSource pickupSfx;
    public AudioSource breakingSfx;

    public static void PlayStart()
    {
        Instance.startSfx.Play();
    }

    public static void PlayBreak()
    {
        Instance.breakSfx.Play();
    }

    public static void PlayPickup()
    {
        Instance.pickupSfx.Play();
    }

    public static void PlayBreaking()
    {
        Instance.breakingSfx.Play();
    }

    public static void StopBreaking()
    {
        Instance.breakingSfx.Stop();
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
