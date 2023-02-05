using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Medi;

public class Root : MonoBehaviour
{
    public float shrivelTime = 2f;

    public void Shrivel()
    {
        StartCoroutine(RunShrivel());
    }

    private IEnumerator RunShrivel()
    {
        Vector3 baseLocalScale = transform.localScale;
        yield return CoroutineHelpers.RunTimer((progress) => transform.localScale = baseLocalScale * (1f - progress), shrivelTime);
        gameObject.SetActive(false);
    }
}
