using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;

    void Start()
    {
        // Store the starting position of the background
        startPos = transform.position;

        // Calculate the width of the background for repeating
        // Assumes you have a BoxCollider2D or BoxCollider attached that matches the sprite size
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    void Update()
    {
        // If the background moves left past a certain point, reset its position
        if (transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }
    }
}
