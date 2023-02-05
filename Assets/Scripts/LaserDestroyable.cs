using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Medi;

public class LaserDestroyable : MonoBehaviour
{
    public System.Action DestroyedByLaser;
    public bool hasBeenDestroyed = false;
    public Laserable laserable;
    public float destroyTime = 2.5f;
    public float maxPositionNoise = 0.1f;
    public float destructionStopBufferTime = 0.1f;
    public bool playsBreakSound = false;
    public bool playsSimpleBreakSound = false;

    private Vector3 savedPosition;

    private Coroutine destructionCoroutine;
    private Coroutine stopDestructionCoroutine;

    private void OnEnable()
    {
        laserable.LaserStarted += OnLaserStarted;
        laserable.LaserStopped += OnLaserStopped;
        savedPosition = transform.position;
    }

    private void OnDisable()
    {
        laserable.LaserStarted -= OnLaserStarted;
        laserable.LaserStopped -= OnLaserStopped;
    }

    private void OnLaserStarted()
    {
        if (destructionCoroutine == null)
        {
            savedPosition = transform.position;
            destructionCoroutine = StartCoroutine(RunDestruction());
        }

        if (stopDestructionCoroutine != null)
        {
            StopCoroutine(stopDestructionCoroutine);
            stopDestructionCoroutine = null;
        }
    }

    private void OnLaserStopped()
    {
        if (stopDestructionCoroutine == null)
        {
            stopDestructionCoroutine = StartCoroutine(RunStopDestruction());
        }
    }

    private IEnumerator RunDestruction()
    {
        // Debug.Log("destruction");
        GlobalAudio.PlayBreaking();
        yield return CoroutineHelpers.RunDecayingPositionNoise(transform, maxPositionNoise, destroyTime, false, reverse: true);
        GlobalAudio.StopBreaking();
        FinishDestruction();
    }

    private IEnumerator RunStopDestruction()
    {
        yield return YieldInstructionCache.WaitForSeconds(destructionStopBufferTime);
        if (destructionCoroutine != null)
        {
            // Debug.Log("stoppping destruction");
            StopCoroutine(destructionCoroutine);
            GlobalAudio.StopBreaking();
            destructionCoroutine = null;
            transform.position = savedPosition;
        }
        stopDestructionCoroutine = null;
    }

    [ContextMenu("Force Destroy")]
    private void FinishDestruction()
    {
        hasBeenDestroyed = true;
        gameObject.SetActive(false);
        destructionCoroutine = null;
        if (playsBreakSound)
        {
            GlobalAudio.PlayBreak();
        }
        else if (playsSimpleBreakSound)
        {
            GlobalAudio.PlaySimpleBreak();
        }
        DestroyedByLaser?.Invoke();
    }

}
