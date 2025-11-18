using UnityEngine;

using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEditorInternal;

public List<GameObject> targets;

public class GameManager : MonoBehaviour
{
    private float spawnRate = 1.0f;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnTarget());
    }

    IEnumerator SpawnTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
