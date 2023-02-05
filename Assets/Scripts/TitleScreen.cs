using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Medi;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public string gameSceneName;
    public Ambience titleAmbience;
    public RawImage blackFade;
    public float fadeInTime = 4f;
    public float fadeOutTime = 4f;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI inputPromptText;

    private bool acceptingInput = false;

    private void Update()
    {
        if (acceptingInput && Keyboard.current.anyKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame)
        {
            StartCoroutine(RunExitSequence());
        }
    }

    private IEnumerator Start()
    {
        inputPromptText.color = inputPromptText.color.WithA(0f);
        titleAmbience.RawPlay();
        blackFade.color = blackFade.color.WithA(1f);
        yield return CoroutineHelpers.RunImageFade(blackFade, 1f, 0f, fadeInTime, false);
        yield return CoroutineHelpers.RunTextFade(inputPromptText, 0f, 1f, fadeInTime, false);
        acceptingInput = true;
    }

    private IEnumerator RunExitSequence()
    {
        GlobalAudio.PlayStart();
        titleAmbience.FadeOut();
        yield return CoroutineHelpers.RunImageFade(blackFade, 0f, 1f, fadeOutTime, false);
        SceneManager.LoadScene(gameSceneName);
        yield break;
    }
}
