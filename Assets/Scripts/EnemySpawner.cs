using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] enemies;
    [SerializeField]
    Transform[] spawnPoints;
    [SerializeField]
    float initalDelay = 1f;
    [SerializeField]
    float spawnDelay = 2f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(initalDelay);
        for(int i = 0; i < enemies.Length; i++)
        {
            Instantiate(enemies[i], spawnPoints[i].position, Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
