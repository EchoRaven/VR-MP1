using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float lookSpeed = 2f;

    [Header("References")]
    [SerializeField] private Transform cameraTransform;

    private float rotationX = 0f;
    private float rotationY = 0f;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private bool cursorLocked = true;

    private void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main?.transform;
        }
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        ReadInput();
        HandleMovement();
        HandleMouseLook();
        HandleCursor();
    }

    private void ReadInput()
    {
        var keyboard = Keyboard.current;
        var mouse = Mouse.current;

        if (keyboard == null || mouse == null) return;

        // Movement input
        moveInput = Vector2.zero;
        if (keyboard.wKey.isPressed) moveInput.y = 1f;
        if (keyboard.sKey.isPressed) moveInput.y = -1f;
        if (keyboard.aKey.isPressed) moveInput.x = -1f;
        if (keyboard.dKey.isPressed) moveInput.x = 1f;

        // Look input
        lookInput = mouse.delta.ReadValue() * 0.1f;
    }

    private void HandleMovement()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 movement = (forward * moveInput.y + right * moveInput.x) * moveSpeed * Time.deltaTime;
        transform.position += movement;

        // Up and Down
        if (keyboard.eKey.isPressed) transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        if (keyboard.qKey.isPressed) transform.position += Vector3.down * moveSpeed * Time.deltaTime;
    }

    private void HandleMouseLook()
    {
        if (!cursorLocked) return;

        rotationY += lookInput.x * lookSpeed;
        rotationX -= lookInput.y * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        transform.rotation = Quaternion.Euler(0f, rotationY, 0f);
        
        if (cameraTransform != null)
        {
            cameraTransform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        }
    }

    private void HandleCursor()
    {
        var keyboard = Keyboard.current;
        var mouse = Mouse.current;

        if (keyboard == null || mouse == null) return;

        if (keyboard.escapeKey.wasPressedThisFrame)
        {
            cursorLocked = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (mouse.leftButton.wasPressedThisFrame && !cursorLocked)
        {
            cursorLocked = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
