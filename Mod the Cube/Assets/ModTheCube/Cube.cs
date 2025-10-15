using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public MeshRenderer Renderer;

    [Header("Rotation Settings")]
    public float rotationSpeedX = 0f;
    public float rotationSpeedY = 0f;
    public float rotationSpeedZ = 0f;

    [Header("Size Settings")]
    [Tooltip("0 = random at start, 1-100 = fixed, 101 = fluctuates smoothly per axis with Perlin noise")]
    public float size = 0f;

    [Header("Fluctuation Settings")]
    public float fluctuationSpeed = 1f; // Speed of size fluctuation

    [Header("Color Settings")]
    public bool fluidColor = false; // Toggle for fluid color change
    public float colorSpeed = 0.5f; // Speed of color change

    private Material cubeMaterial;

    void Start()
    {
        transform.position = new Vector3(3, 4, 1);

        // Set initial size
        if (size == 0f)
        {
            size = Random.Range(1f, 100f);
        }
        transform.localScale = Vector3.one * size;

        // Get material
        cubeMaterial = Renderer.material;

        // Set initial color
        if (!fluidColor)
        {
            cubeMaterial.color = new Color(Random.value, Random.value, Random.value, 1f);
        }

        // Only randomize rotation if the public variable is 0 (default)
        if (rotationSpeedX == 0f)
            rotationSpeedX = Random.Range(-100f, 100f);

        if (rotationSpeedY == 0f)
            rotationSpeedY = Random.Range(-100f, 100f);

        if (rotationSpeedZ == 0f)
            rotationSpeedZ = Random.Range(-100f, 100f);
    }

    void Update()
    {
        // Rotate the cube
        transform.Rotate(rotationSpeedX * Time.deltaTime,
                         rotationSpeedY * Time.deltaTime,
                         rotationSpeedZ * Time.deltaTime);

        // Time variable for synced Perlin noise
        float time = Time.time * fluctuationSpeed;

        // Smoothly fluctuate size per axis if 101
        if (size == 101f)
        {
            float sizeX = Mathf.Lerp(1f, 100f, Mathf.PerlinNoise(time, 0f));
            float sizeY = Mathf.Lerp(1f, 100f, Mathf.PerlinNoise(time, 10f));
            float sizeZ = Mathf.Lerp(1f, 100f, Mathf.PerlinNoise(time, 20f));

            transform.localScale = new Vector3(sizeX, sizeY, sizeZ);

            // Sync color with size fluctuation if fluidColor is enabled
            if (fluidColor)
            {
                float r = Mathf.PerlinNoise(time, 30f);
                float g = Mathf.PerlinNoise(time, 40f);
                float b = Mathf.PerlinNoise(time, 50f);

                cubeMaterial.color = new Color(r, g, b, 1f);
            }
        }
        else if (fluidColor)
        {
            // If size is not 101, still allow fluid color independently
            float r = Mathf.PerlinNoise(Time.time * colorSpeed, 0f);
            float g = Mathf.PerlinNoise(Time.time * colorSpeed, 10f);
            float b = Mathf.PerlinNoise(Time.time * colorSpeed, 20f);

            cubeMaterial.color = new Color(r, g, b, 1f);
        }
    }
}
