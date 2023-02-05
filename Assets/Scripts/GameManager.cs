using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Overgrowth hubOvergrowth;
    public Overgrowth finalOvergrowth;
    public string creditsSceneName = "Credits";
    public Ambience levelAmbience;
    public Ambience finaleAmbience;

    private void Awake()
    {
        hubOvergrowth.Destroyed += OnHubOvergrowthDestroyed;
        finalOvergrowth.Destroyed += OnFinalOvergrowthDestroyed;
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
        SceneManager.LoadScene(creditsSceneName);
        yield break;
    }
}
