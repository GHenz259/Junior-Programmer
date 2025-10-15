using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float sprintMultiplier = 2f;
    public float acceleration = 10f;
    public float deceleration = 10f;

    [Header("Mouse Settings")]
    public float mouseSensitivity = 2f;

    float xRotation = 0f;
    float yRotation = 0f;
    Vector3 currentVelocity = Vector3.zero;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Vertical look
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Horizontal look
        yRotation += mouseX;

        // Apply both rotations
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal"); // A/D
        float z = Input.GetAxis("Vertical");   // W/S
        float y = 0f;

        if (Input.GetKey(KeyCode.Space)) y += 1f;
        if (Input.GetKey(KeyCode.LeftControl)) y -= 1f;

        Vector3 targetDirection = (transform.right * x) + (transform.forward * z) + (transform.up * y);
        targetDirection.Normalize();

        float currentSpeed = moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
            currentSpeed *= sprintMultiplier;

        Vector3 targetVelocity = targetDirection * currentSpeed;
        currentVelocity = Vector3.Lerp(currentVelocity, targetVelocity,
            (targetDirection.magnitude > 0 ? acceleration : deceleration) * Time.deltaTime);

        transform.position += currentVelocity * Time.deltaTime;
    }
}
