using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerGroup : MonoBehaviour
{
    [SerializeField]
    GameObject enemy;
    [SerializeField]
    int enemyCount = 20;
    [SerializeField]
    Transform[] spawnLocations;
    [SerializeField]
    float initialDelay = 6f;
    [SerializeField]
    float minSpawnDelay = 0.01f;
    [SerializeField]
    float maxSpawnDelay = 0.2f;
    [SerializeField]
    GameObject reticle;

    private Animator animator;

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
        yield return new WaitForSeconds(initialDelay);
        if(reticle != null)
        {
            Destroy(reticle);
        }
        foreach(var location in spawnLocations)
        {
            //yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            yield return new WaitForSeconds(minSpawnDelay);
            GameObject newEnemy = Instantiate(enemy, location.position, Quaternion.identity);
            newEnemy.GetComponent<DissolveEffect>().bStartDissolved = true;
            newEnemy.GetComponent<Animator>().SetBool("isIdle", true);
            newEnemy.GetComponent<MeleeEnemy>().attackSpeed = 0;
            newEnemy.GetComponent<MeleeEnemy>().willChase = false;

        }
    }
}
