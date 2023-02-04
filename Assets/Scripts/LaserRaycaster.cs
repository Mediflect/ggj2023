using System;
using UnityEngine;

public class LaserRaycaster : MonoBehaviour
{
    public LayerMask hitLayers = Physics.DefaultRaycastLayers;
    public Transform laserDestination;

    private void Update()
    {
        const float MaxRaycastDist = 100f;
        RaycastHit hit;
        bool hitGeo = Physics.Raycast(transform.position, transform.forward, out hit, MaxRaycastDist, hitLayers, QueryTriggerInteraction.Ignore);
        if (hitGeo)
        {
            const float OvershootAmount = 0.2f;
            laserDestination.position = hit.point + (transform.forward * OvershootAmount);
        }
        else
        {
            laserDestination.position = transform.position + (transform.forward * MaxRaycastDist);
        }
    }
}
