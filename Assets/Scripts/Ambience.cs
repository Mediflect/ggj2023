using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Medi;

public class Ambience : MonoBehaviour
{
    public AudioSource ambienceAudio;
    public float fadeTime = 1f;

    private float targetVolume;

    private void Awake()
    {
        targetVolume = ambienceAudio.volume;
    }

    public void RawPlay()
    {
        ambienceAudio.Play();
    }

    public void FadeIn()
    {
        ambienceAudio.Play();
        StartCoroutine(CoroutineHelpers.RunAudioFade(ambienceAudio, 0f, targetVolume, fadeTime, false));
    }

    public void FadeOut()
    {
        StartCoroutine(CoroutineHelpers.RunAudioFade(ambienceAudio, targetVolume, 0f, fadeTime, false));
        ambienceAudio.Stop();
    }
}
