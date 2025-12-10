using UnityEngine;

public class Dropper : MonoBehaviour
{
    [Header("Movement Settings")]
    public float zRange = 15f;      // Moves between +zRange and -zRange
    public float speed = 1f;        // Movement speed multiplier

    [Header("Drop Settings")]
    public GameObject spherePrefab; // Assign your Sphere prefab in the Inspector
    public float dropY = 12f;       // Height to spawn spheres at

    private Vector3 basePosition;

    void Start()
    {
        basePosition = new Vector3(0f, 15f, 0f);
        transform.position = basePosition;
    }

    void Update()
    {
        // Smooth oscillation from -zRange to +zRange
        float zOffset = Mathf.Sin(Time.time * speed) * zRange;

        transform.position = new Vector3(
            basePosition.x,
            basePosition.y,
            zOffset
        );

        // Drop sphere on Space key
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DropSphere();
        }
    }

    void DropSphere()
    {
        Vector3 spawnPos = new Vector3(
            transform.position.x,
            dropY,
            transform.position.z
        );

        Instantiate(spherePrefab, spawnPos, Quaternion.identity);
    }
}
