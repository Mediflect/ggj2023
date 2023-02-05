using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Overgrowth finalOvergrowth;
    public string creditsSceneName = "Credits";

    private void Awake()
    {
        finalOvergrowth.Destroyed += OnFinalOvergrowthDestroyed;
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
