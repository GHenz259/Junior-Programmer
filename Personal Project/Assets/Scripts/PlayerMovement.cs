using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Movement Settings")]
    public float speed = 5f;
    public float sprintSpeed = 10f;
    public float jumpForce = 7f;
    private bool isGrounded = true;

    [Header("Camera Settings")]
    public Transform cameraPivot;
    public Transform mainCamera;
    public float mouseSensitivity = 150f;
    public float rotationSmoothTime = 0.1f;
    public Vector2 pitchLimits = new Vector2(-40f, 60f);
    public float cameraDistance = 5f;

    private float yaw;
    private float pitch;
    private Vector3 currentRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleCameraRotation();
        HandleMovement();
    }

    void HandleCameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, pitchLimits.x, pitchLimits.y);

        // Rotate the camera pivot
        cameraPivot.rotation = Quaternion.Euler(pitch, yaw, 0f);

        // Position the camera behind the pivot
        mainCamera.position = cameraPivot.position - cameraPivot.forward * cameraDistance;
        mainCamera.LookAt(cameraPivot);
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : speed;

        // Movement relative to camera direction
        Vector3 moveDir = cameraPivot.forward * moveZ + cameraPivot.right * moveX;
        moveDir.y = 0f;
        moveDir.Normalize();

        if (moveDir.magnitude > 0.1f)
        {
            // Move and rotate player
            Vector3 newPosition = rb.position + moveDir * currentSpeed * Time.deltaTime;
            rb.MovePosition(newPosition);

            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothTime);
        }
        else
        {
            // 🔒 Stop drift when no input
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
