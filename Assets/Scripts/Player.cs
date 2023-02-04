using UnityEngine;

public class Player : MonoBehaviour
{
    public static event System.Action LaserStopped;

    public FPSControls controls;
    public Camera headCamera;
    public PlayerSounds sounds;
    public LaserRaycaster laserRaycaster;
    public bool hasLaser = false;

    private void Awake()
    {
        controls.InteractPressed += OnInteractPressed;
        controls.InteractReleased += OnInteractReleased;
        laserRaycaster.gameObject.SetActive(false);
    }

    private void OnInteractPressed()
    {
        if (!hasLaser)
        {
            return;
        }
        laserRaycaster.gameObject.SetActive(true);
    }

    private void OnInteractReleased()
    {
        if (!hasLaser)
        {
            return;
        }
        laserRaycaster.gameObject.SetActive(false);
        LaserStopped?.Invoke();
    }
}
