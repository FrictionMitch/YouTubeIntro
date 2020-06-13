using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy {
    public Transform target;
    public bool bCanAttack = false;
    public bool willChase = true;
    public float attackSpeed;

    [SerializeField]
    float stopDistance; // Distance from Player


    [SerializeField]
    float attackDelay;


    public enum EnemyTypeEnum {MELEE, BLIND, DUAL, SINGLE, RANGED, SPAWNER, BOSS};
    public EnemyTypeEnum EnemyType;

    float attackTime;
    Vector2 soundPosition;

    // Update is called once per frame
    void Update()
    {
        if (player != null) {   
            if(Vector2.Distance(transform.position, player.transform.position) > stopDistance && willChase) {
                if(EnemyType == EnemyTypeEnum.BLIND) {
                    soundPosition = player.GetComponentInChildren<Weapon>().strumPosition;
                    transform.position = Vector2.MoveTowards(transform.position, soundPosition, enemySpeed * Time.deltaTime);
                } else {
                    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemySpeed * Time.deltaTime);
                    //transform.position = Vector2.MoveTowards(transform.position, target.position, enemySpeed * Time.deltaTime);
                }
                
                if (animator) {
                    animator.SetBool("isWalking", true);
                    //animator.SetBool("Walk", true);
                }
            } else {
                animator.SetBool("isWalking", false);
                animator.SetBool("isIdle", true);
                //animator.SetBool("Walk", false);
                if(Time.time >= attackTime) {
                    //attack
                    attackTime = Time.time + timeBetweenAttacks;

                    switch (EnemyType) {
                        case EnemyTypeEnum.MELEE:
                            if (bCanAttack)
                            {
                                StartCoroutine(MeleeAttack());
                            }
                            break;
                        case EnemyTypeEnum.SINGLE:
                            StartCoroutine(SingleWeaponAttack());
                            break;
                        case EnemyTypeEnum.DUAL:
                            break;
                        case EnemyTypeEnum.BLIND:
                            break;
                        case EnemyTypeEnum.BOSS:
                            break;
                        case EnemyTypeEnum.RANGED:
                            break;
                        case EnemyTypeEnum.SPAWNER:
                            break;
                    }
                }
            }
        }

    }

    IEnumerator MeleeAttack()
    {
        player.GetComponent<Player>().TakeDamage(damage);

        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.transform.position;

        float percent = 0;
        while (percent <= 1) {
            percent += Time.deltaTime * attackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);

            yield return null;
        }
    }

    IEnumerator SingleWeaponAttack()
    {
        animator.SetTrigger("Slash");
        player.GetComponent<Player>().TakeDamage(damage);
        yield return new WaitForSeconds(attackDelay);
    }

    void MoveTowardsPlayer(Transform target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, enemySpeed * Time.deltaTime);
    }

    void Walk(Vector2 targetPosition)
    {

    }

    void StopMoving(GameObject enemyObject)
    {
        //if(this.gameObject == enemyObject)
        //{
            enemySpeed = 0f;
        //}
    }

    private void OnEnable()
    {
        Enemy.OnDeath += StopMoving;
    }

    private void OnDisable()
    {
        Enemy.OnDeath -= StopMoving;
    }
}
