using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelLight : MonoBehaviour
{
    private const string EMISSION_PROP = "_EmissionColor";

    public List<Laserable> laserables;
    public Material offMaterial;
    public Material onMaterial;
    public List<Renderer> renderers;
    public GameObject lowLight;
    public GameObject highLight;

    private bool isTurnedOn = false;

    private void Awake()
    {
        foreach (Renderer rend in renderers)
        {
            rend.sharedMaterial = offMaterial;
        }
        foreach (Laserable l in laserables)
        {
            l.LaserStarted += OnLaserTouched;
        }
    }

    private void OnLaserTouched()
    {
        if (isTurnedOn)
        {
            return;
        }

        isTurnedOn = true;
        foreach (Renderer rend in renderers)
        {
            rend.sharedMaterial = onMaterial;
        }
        GlobalAudio.PlaySimpleBreak();
        lowLight.SetActive(false);
        highLight.SetActive(true);
    }
}
