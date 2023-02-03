using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Medi
{
    public static class CoroutineHelpers
    {
        /// <summary>
        /// Runs a generic timer over the specified duration. The onUpdate action will be invoked every frame 
        /// with the current progress of the timer as a percentage, which can optionally be smoothed at the start/end.
        /// </summary>
        public static IEnumerator RunTimer(Action<float> onUpdate, float duration, bool smooth = false, bool reverse = false)
        {
            float timer = 0f;
            while (timer <= duration)
            {
                float t = Mathf.InverseLerp(0, duration, timer);
                if (reverse)
                {
                    t = 1f - t;
                }
                if (smooth)
                {
                    t = Mathf.SmoothStep(0, 1, t);
                }
                onUpdate?.Invoke(t);
                yield return null;
                timer += Time.deltaTime;
            }

            // give one final update at 100% progress
            onUpdate?.Invoke(reverse ? 0f : 1f);
        }

        /// <summary>
        /// Moves the transform from startPos to endPos over the duration, with optional smoothing
        /// </summary>
        public static IEnumerator RunMoveTransform(Transform toMove, Vector3 startPos, Vector3 endPos, float duration, bool smooth = true)
        {
            Action<float> onUpdate = (progress) =>
            {
                toMove.transform.position = Vector3.Lerp(startPos, endPos, progress);
            };
            yield return RunTimer(onUpdate, duration, smooth);
        }

        /// <summary>
        /// Shakes the given transform by applying noise to its local transform that decays over the duration.
        /// If reverse is set to true, the noise will increase over the duration instead.
        /// </summary>
        public static IEnumerator RunDecayingPositionNoise(Transform toMove, float maxDist, float duration, bool smooth = true, bool reverse = false)
        {
            Vector3 baseLocalPos = toMove.localPosition;
            Action<float> onUpdate = (progress) =>
            {
                float maxDistThisFrame = Mathf.Lerp(maxDist, 0, progress);
                float x = Random.Range(baseLocalPos.x - maxDistThisFrame, baseLocalPos.x + maxDistThisFrame);
                float y = Random.Range(baseLocalPos.y - maxDistThisFrame, baseLocalPos.y + maxDistThisFrame);
                float z = Random.Range(baseLocalPos.z - maxDistThisFrame, baseLocalPos.z + maxDistThisFrame);
                toMove.localPosition = new Vector3(x, y, z);
            };
            yield return RunTimer(onUpdate, duration, smooth, reverse);
            toMove.localPosition = baseLocalPos;
        }

        /// <summary>
        /// Fades the alpha of the text over the duration, with optional smoothing
        /// </summary>
        public static IEnumerator RunTextFade(TextMeshProUGUI text, float startAlpha, float endAlpha, float duration, bool smooth = true)
        {
            Action<float> onUpdate = (progress) =>
            {
                text.alpha = Mathf.Lerp(startAlpha, endAlpha, progress);
            };
            yield return RunTimer(onUpdate, duration, smooth);
        }

        /// <summary>
        /// Fades the alpha of the image over the duration, with optional smoothing
        /// </summary>
        public static IEnumerator RunImageFade(RawImage image, float startAlpha, float endAlpha, float duration, bool smooth = true)
        {
            Action<float> onUpdate = (progress) =>
            {
                image.color = image.color.WithA(Mathf.Lerp(startAlpha, endAlpha, progress));
            };
            yield return RunTimer(onUpdate, duration, smooth);
        }

        /// <summary>
        /// Fades the volume of the audio source over the duration, with optional smoothing
        /// </summary>
        public static IEnumerator RunAudioFade(AudioSource audio, float startVolume, float endVolume, float duration, bool smooth = true)
        {
            Action<float> onUpdate = (progress) =>
            {
                audio.volume = Mathf.Lerp(startVolume, endVolume, progress);
            };
            yield return RunTimer(onUpdate, duration, smooth);
        }
    }
}