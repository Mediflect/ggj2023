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
    public GameObject geometry;
    public GameObject destroyedParticles;

    [Header("Anticipation stuff")]
    public bool hasAnticipation = false;
    public float anticipationTime = 0.3f;

    private Vector3 savedPosition;

    private Coroutine destructionCoroutine;
    private Coroutine stopDestructionCoroutine;
    private bool preventStopping = false;

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
        if (stopDestructionCoroutine == null && !preventStopping)
        {
            stopDestructionCoroutine = StartCoroutine(RunStopDestruction());
        }
    }

    private IEnumerator RunDestruction()
    {
        // Debug.Log("destruction");
        GlobalAudio.PlayBreaking();
        if (hasAnticipation)
        {
            StartCoroutine(CoroutineHelpers.RunDecayingPositionNoise(transform, maxPositionNoise, destroyTime, false, reverse: true));
            yield return YieldInstructionCache.WaitForSeconds(destroyTime - anticipationTime);
            preventStopping = true;
            GlobalAudio.PlayBreak();
            yield return YieldInstructionCache.WaitForSeconds(anticipationTime);
        }
        else
        {
            yield return CoroutineHelpers.RunDecayingPositionNoise(transform, maxPositionNoise, destroyTime, false, reverse: true);
        }
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
        if (destroyedParticles != null)
        {
            destroyedParticles.SetActive(true);
        }
        if (geometry != null)
        {
            geometry.SetActive(false);
        }
        destructionCoroutine = null;
        if (playsBreakSound && !hasAnticipation)
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
