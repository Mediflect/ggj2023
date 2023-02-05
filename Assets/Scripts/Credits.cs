using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Medi;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public Ambience titleAmbience;
    public RawImage blackFade;
    public float fadeInTime = 4f;
    public float fadeOutTime = 4f;

    private IEnumerator Start()
    {
        titleAmbience.RawPlay();
        blackFade.color = blackFade.color.WithA(1f);
        yield return CoroutineHelpers.RunImageFade(blackFade, 1f, 0f, fadeInTime, false);
    }
}
