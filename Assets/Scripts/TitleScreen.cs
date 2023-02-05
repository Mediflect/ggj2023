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

    private void Awake()
    {
        // App.Request(OnAppExists);
    }

    private void OnAppExists()
    {
        // StartCoroutine(RunTitleSequence());
    }

    private void Update()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame)
        {
            StartCoroutine(RunExitSequence());
        }
    }

    private IEnumerator RunExitSequence()
    {
        GlobalAudio.PlayStart();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(gameSceneName);
        yield break;
    }
}
