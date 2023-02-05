using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Medi
{
    public class SineScaleBob : MonoBehaviour
    {
        private static VerboseLogger Verbose = VerboseLogger.Get(nameof(SineScaleBob));

        [SerializeField]
        private SinePulse pulse;

        private Vector3 baseLocalScale;

        private void Awake()
        {
            baseLocalScale = transform.localScale;
        }

        private void OnEnable()
        {
            if (pulse == null)
            {
                Verbose.LogWarning("No pulse is assigned");
                enabled = false;
            }
        }

        private void OnDisable()
        {
            transform.localScale = baseLocalScale;
        }

        private void Update()
        {
            transform.localScale = baseLocalScale * (1 + pulse.Value);
        }
    }
}