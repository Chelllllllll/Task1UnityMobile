using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float mouseSensivity = 2f;
    [SerializeField] private Transform cameraTransform;
    
    private CharacterController characterController;
    private InputAction controls;
    private float mouseUp;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        controls = new InputAction();
    }

    private void OnEnable()
    {
        controls.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        Vector2 moveInput = controls.ReadValue<Vector2>();
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        characterController.Move(move * moveSpeed * Time.deltaTime);

        Look();
    }

    private void Look()
    {
        Vector2 mouseSide = Mouse.current.delta.ReadValue();
        
        float mouseSensivityDelta = mouseSide.x * mouseSensivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseSensivityDelta);
        
        mouseUp -= mouseSide.y * mouseSensivity * Time.deltaTime;
        mouseUp = Mathf.Clamp(mouseUp, -90f, 90f);
        cameraTransform.localEulerAngles = new Vector3(mouseUp, 0f, 0f);
        
    }
}
