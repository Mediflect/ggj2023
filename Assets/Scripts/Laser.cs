using UnityEngine;
using Medi;

public class Laser : MonoBehaviour
{
    public Transform origin;
    public Transform destination;

    private void Update()
    {
        float dist = Vector3.Distance(origin.position, destination.position);
        transform.localScale = transform.localScale.WithY(dist);

        Vector3 midpoint = (origin.position + destination.position) / 2f;
        transform.position = midpoint;

        Vector3 direction = (destination.position - origin.position).normalized;
        transform.up = direction;
    }
}
