using UnityEngine;

public class Player : MonoBehaviour
{
    public FPSControls controls;
    public Camera headCamera;
    public InteractRaycaster interactRaycaster;
    public PlayerSounds sounds;

    private void Awake()
    {
        controls.InteractPressed += OnInteract;
        interactRaycaster.InteractableFound += OnInteractableFound;
        interactRaycaster.InteractableLost += OnInteractableLost;
    }

    private void OnInteract()
    {
        if (interactRaycaster.CurrentInteractable != null)
        {
            interactRaycaster.CurrentInteractable.Use(this);
        }
    }

    private void OnInteractableFound()
    {
    }

    private void OnInteractableLost()
    {
    }
}
