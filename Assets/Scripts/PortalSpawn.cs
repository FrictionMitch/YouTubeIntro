using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpawn : MonoBehaviour
{
    [SerializeField]
    GameObject enemySpawn;

    [SerializeField]
    Transform spawnPosition;

    [SerializeField]
    float timeBetweenSpawn;

    Animator animator;
    bool isOpen = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = enemySpawn.GetComponent<Animator>();
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame


    void LeapEntrence() {
        //instantiate enemy
        GameObject newEnemy = Instantiate(enemySpawn, spawnPosition.position, Quaternion.identity);
        //launch into scene
        animator.SetTrigger("PortalTrigger");
        //start enemy launch anim
        animator.SetBool("isWalking", true);

    }
    
    IEnumerator SpawnEnemies() {
        while (isOpen) {
            yield return new WaitForSeconds(timeBetweenSpawn);
            LeapEntrence();
        }
    }
}
