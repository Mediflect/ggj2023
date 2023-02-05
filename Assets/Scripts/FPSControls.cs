using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPSControls : MonoBehaviour
{
    public event Action InteractPressed;
    public event Action InteractReleased;
    public event Action PowerPressed;

    public enum MotionType
    {
        Stationary,
        Walking,
        Running
    }

    public MotionType CurrentMotion => currentMotion;
    public Transform CameraTransform => cameraTransform;

    [SerializeField]
    private Transform cameraTransform = null;

    [SerializeField]
    private CharacterController charController = null;

    [Header("Look")]
    [SerializeField]
    private float lookSpeed = 90f;

    [SerializeField]
    private float maxVerticalLookAngle = 85f;

    [SerializeField]
    private bool invertVerticalLook = false;

    [Header("Movement")]
    [SerializeField]
    private float speed = 2f;           // a little faster than average human walking speed

    [SerializeField]
    private float runSpeed = 4f;        // when shift is held

    [SerializeField]
    private float acceleration = 20f;   // gets up to speed in 0.1s

    [SerializeField]
    private bool applyGravity = true;

    [SerializeField]
    private LayerMask floorLayers = Physics.DefaultRaycastLayers;

    private MotionType currentMotion = MotionType.Stationary;
    private Vector3 currentVelocity = Vector3.zero;
    private float currentPitch = 0f;
    private float currentYaw = 0f;

    private void OnEnable()
    {
        currentPitch = cameraTransform.localEulerAngles.x;
        currentYaw = cameraTransform.localEulerAngles.y;
        charController.enabled = true;
    }

    private void OnDisable()
    {
        charController.enabled = false;
        currentVelocity = Vector3.zero;
    }

    private void Update()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        UpdateRotation();
        UpdatePosition();
        // check for interacts
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            InteractPressed?.Invoke();
        }
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            InteractReleased?.Invoke();
        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            PowerPressed?.Invoke();
        }
    }

    private void UpdateRotation()
    {
         // This value is framerate independent! Represent pixels moved since last frame (or last read?)
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        // This multiplier was arbitrarily applied to mouse movements in the old input system
        // It makes the look speed variable easier to tune
        const float MAGIC_LOOK_SENSITIVITY_ADJUSTMENT = 0.05f;
        currentYaw += mouseDelta.x * lookSpeed * MAGIC_LOOK_SENSITIVITY_ADJUSTMENT;
        float pitchDelta = mouseDelta.y * lookSpeed * MAGIC_LOOK_SENSITIVITY_ADJUSTMENT;
        if (!invertVerticalLook)
        {
            pitchDelta *= -1f;
        }
        currentPitch += pitchDelta;
        currentPitch = Mathf.Clamp(currentPitch, -maxVerticalLookAngle, maxVerticalLookAngle);
        cameraTransform.localRotation = Quaternion.Euler(currentPitch, currentYaw, 0f);
    }

    private void UpdatePosition()
    {
        // Horizontal movement
        Vector3 moveDir = Vector3.zero;
        if (Keyboard.current.wKey.isPressed)
        {
            moveDir += Vector3.forward;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            moveDir += Vector3.back;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            moveDir += Vector3.left;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            moveDir += Vector3.right;
        }

        moveDir = cameraTransform.TransformDirection(moveDir);
        if (applyGravity)
        {
            moveDir = new Vector3(moveDir.x, 0f, moveDir.z);
        }
        moveDir = moveDir.normalized;
        bool isRunning = Keyboard.current.leftShiftKey.isPressed;
        Vector3 goalVelocity = moveDir * (isRunning ? runSpeed : speed);

        currentVelocity = Vector3.MoveTowards(currentVelocity, goalVelocity, acceleration * Time.deltaTime);
        if (applyGravity)
        {
            charController.SimpleMove(currentVelocity);
        }
        else
        {
            charController.Move(currentVelocity * Time.deltaTime);
        }

        if (moveDir == Vector3.zero)
        {
            currentMotion = MotionType.Stationary;
        }
        else
        {
            currentMotion = isRunning ? MotionType.Running : MotionType.Walking;
        }
    }

    private Vector3 AdjustVelocityToFloor(Vector3 velocity)
    {
        const float FloorCheckDist = 0.2f;
        RaycastHit hit;
        if (Physics.Raycast(charController.transform.position, Vector3.down, out hit, FloorCheckDist, floorLayers, QueryTriggerInteraction.Ignore))
        {
            Vector3 floorNormal = hit.normal;
            // TODO: do the adjustment
            return floorNormal;
        }
        else
        {
            return velocity;
        }
    }
}
