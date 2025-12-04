using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed = 15.0f;
    [SerializeField] private float turnSpeed = 5.0f;
    [SerializeField] private float horizontalInput;
    [SerializeField] private float forwardInput;


    // Update is called once per frame
    void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
        transform.Rotate(Vector3.up, horizontalInput);

        // Move the vehicle forward
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        // Turn the vehicle
        transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);
    }
}
