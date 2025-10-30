using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    private Vector3 spawnPos = new Vector3(25, 1, 0);
    private float startDelay = 2f;
    private float repeatRate = 2f;
    private PlayerController playerControllerScript;

    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        playerControllerScript =
            GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void SpawnObstacle()
    {
        if (playerControllerScript.gameOver == false)
            Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
    }
}
