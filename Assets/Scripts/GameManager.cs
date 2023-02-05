using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Medi;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Overgrowth hubOvergrowth;
    public Overgrowth finalOvergrowth;
    public string creditsSceneName = "Credits";
    public Ambience levelAmbience;
    public Ambience finaleAmbience;
    public RawImage blackFade;
    public float fadeTime = 4f;
    public FPSControls controls;

    public TextMeshProUGUI inputTipText;
    public TextMeshProUGUI powerTipText;
    public PowerPickup powerPickup;

    private const float TextFadeTime = 1.5f;
    private const float TextHoldTime = 5f;

    private void Awake()
    {
        hubOvergrowth.Destroyed += OnHubOvergrowthDestroyed;
        finalOvergrowth.Destroyed += OnFinalOvergrowthDestroyed;
        powerPickup.PowerAqcuired += () => StartCoroutine(RunPowerTip());
    }

    private IEnumerator Start()
    {
        controls.enabled = false;
        blackFade.color = blackFade.color.WithA(1);
        yield return CoroutineHelpers.RunImageFade(blackFade, 1f, 0f, fadeTime, false);
        controls.enabled = true;
        inputTipText.gameObject.SetActive(true);
        yield return CoroutineHelpers.RunTextFade(inputTipText, 0f, 1f, TextFadeTime, false);
        yield return YieldInstructionCache.WaitForSeconds(TextHoldTime);
        yield return CoroutineHelpers.RunTextFade(inputTipText, 1f, 0f, TextFadeTime, false);
    }

    private void OnHubOvergrowthDestroyed()
    {
        finaleAmbience.FadeIn();
    }

    private void OnFinalOvergrowthDestroyed()
    {
        StartCoroutine(RunTransitionToCredits());
    }

    private IEnumerator RunTransitionToCredits()
    {
        controls.enabled = false;
        yield return CoroutineHelpers.RunImageFade(blackFade, 0f, 1f, fadeTime, false);
        SceneManager.LoadScene(creditsSceneName);
    }

    private IEnumerator RunPowerTip()
    {
        powerTipText.gameObject.SetActive(true);
        yield return CoroutineHelpers.RunTextFade(powerTipText, 0f, 1f, TextFadeTime, false);
        yield return YieldInstructionCache.WaitForSeconds(TextHoldTime);
        yield return CoroutineHelpers.RunTextFade(powerTipText, 1f, 0f, TextFadeTime, false);
    }
}
