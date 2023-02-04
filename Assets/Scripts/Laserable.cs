using UnityEngine;

public class Laserable : MonoBehaviour
{
    public System.Action LaserStarted;
    public System.Action LaserStopped;

    public bool laserIsTouching = false;
    public float laserStopTime = 0.1f;

    private void Awake()
    {
        Player.LaserStopped += () => {
            laserIsTouching = false;
            LaserStopped?.Invoke();
        };
    }

    private void OnTriggerEnter(Collider other)
    {
        Laser laser = other.gameObject.GetComponent<Laser>();
        if (laser != null && !laserIsTouching)
        {
            laserIsTouching = true;
            LaserStarted?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Laser laser = other.gameObject.GetComponent<Laser>();
        if (laser != null && laserIsTouching)
        {
            laserIsTouching = false;
            LaserStopped?.Invoke();
        }
    }
}
