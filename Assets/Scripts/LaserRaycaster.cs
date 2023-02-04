using System;
using UnityEngine;

public class LaserRaycaster : MonoBehaviour
{
    public LayerMask hitLayers = Physics.DefaultRaycastLayers;
    public Transform laserDestination;
    public GameObject laserHitParticles;

    private void OnDisable()
    {
        laserDestination.position = transform.position;
        Update();
    }

    private void Update()
    {
        const float MaxRaycastDist = 15f;
        RaycastHit hit;
        bool hitGeo = Physics.Raycast(transform.position, transform.forward, out hit, MaxRaycastDist, hitLayers, QueryTriggerInteraction.Ignore);
        if (hitGeo)
        {
            const float OvershootAmount = 0.02f;
            laserDestination.position = hit.point + (transform.forward * OvershootAmount);
            laserDestination.LookAt(laserDestination.position + hit.normal);
            laserHitParticles.SetActive(true);
        }
        else
        {
            laserDestination.position = transform.position + (transform.forward * MaxRaycastDist);
            laserHitParticles.SetActive(false);
        }
    }
}
