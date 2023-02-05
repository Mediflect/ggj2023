using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Medi;

public class GlobalAudio : MonoBehaviour
{
    private static GlobalAudio Instance;

    public AudioSource startSfx;
    public AudioSource breakSfx;
    public AudioSource simpleBreakSfx;
    public AudioSource pickupSfx;
    public AudioSource breakingSfx;
    public AudioSource laserHumSfx;

    public static void PlayStart()
    {
        Instance.startSfx.Play();
    }

    public static void PlayBreak()
    {
        Instance.breakSfx.Play();
    }

    public static void PlaySimpleBreak()
    {
        Instance.simpleBreakSfx.Play();
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

    private const float laserHumFadeTime = 0.25f;
    private const float laserHumTargetVolume = 0.085f;

    public static void StartLaserHum()
    {
        Instance.StartCoroutine(CoroutineHelpers.RunAudioFade(Instance.laserHumSfx, 0f, laserHumTargetVolume, laserHumFadeTime, false));
    }

    public static void StopLaserHum()
    {
        Instance.StartCoroutine(CoroutineHelpers.RunAudioFade(Instance.laserHumSfx, laserHumTargetVolume, 0f, laserHumFadeTime, false));
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
