using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Medi;

public class Ambience : MonoBehaviour
{
    public AudioSource ambienceAudio;
    public float fadeTime = 1f;

    public void RawPlay()
    {
        ambienceAudio.Play();
    }

    public void FadeIn()
    {
        ambienceAudio.Play();
        CoroutineHelpers.RunAudioFade(ambienceAudio, 0f, 1f, fadeTime, false);
    }

    public void FadeOut()
    {
        CoroutineHelpers.RunAudioFade(ambienceAudio, 1f, 0f, fadeTime, false);
        ambienceAudio.Stop();
    }
}
