using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 8f;
    public float jumpHeight = 2f;
    public float gravity = -20f;
    public float mouseSensitivity = 2f;
    public float airControl = 0.5f;

    [Header("References")]
    public Transform cameraTransform;

    [Header("Input Actions")]
    public InputActionReference moveAction;
    public InputActionReference lookAction;
    public InputActionReference jumpAction;

    private CharacterController controller;
    private Vector3 velocity;
    private float xRotation = 0f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        moveAction.action.Enable();
        lookAction.action.Enable();
        jumpAction.action.Enable();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        lookAction.action.Disable();
        jumpAction.action.Disable();
    }

    private void Update()
    {
        HandleMouseLook();
        HandleMovement();
    }

    private void HandleMouseLook()
    {
        Vector2 lookInput = lookAction.action.ReadValue<Vector2>();

        float mouseX = lookInput.x * mouseSensitivity;
        float mouseY = lookInput.y * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void HandleMovement()
    {
        Vector2 moveInput = moveAction.action.ReadValue<Vector2>();
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

        if (controller.isGrounded)
        {
            if (velocity.y < 0f)
                velocity.y = -2f;

            if (jumpAction.action.WasPressedThisFrame())
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            controller.Move(move * moveSpeed * Time.deltaTime);
        }
        else
        {
            controller.Move(move * moveSpeed * airControl * Time.deltaTime);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}