using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Action<GameObject> OnDeath;

    [SerializeField]
    float deathDelay = 0.8f;

    public float timeBetweenAttacks;
    public float enemySpeed;
    public int damage = 10;
    public int health;
    public int maxDistance = 500;

    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public bool bIsAlive = true;
    [HideInInspector]
    public Transform player;

    private DissolveEffect dissolveEffect;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (!animator) {
            animator = GetComponentInChildren<Animator>();
        }
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (!player) {
            Debug.Log("No Player Found");
            return;
        }

        dissolveEffect = GetComponentInParent<DissolveEffect>();
    }

    private void Update()
    {
        if (bIsAlive)
        {
            //MoveTowardsPlayer(player);
        }
    }

    public void TakeDamage(int damageAmount) {
        if (health <= 0) {
            bIsAlive = false;
            DestroySelf();
            return;
        }
        health = health - damageAmount;
    }

    IEnumerator DeathRoutine()
    {
        if(OnDeath != null)
        {
            OnDeath(this.gameObject);
        }
            
        animator.SetBool("isIdle", true);
        //dissolveEffect.bIsDissolving = true;
        dissolveEffect.StartDissolve();
        yield return new WaitForSeconds(deathDelay);
        Destroy(gameObject);
    }

    void DestroySelf() 
    {
        bIsAlive = false;
        StartCoroutine(DeathRoutine());
    }

    void MoveTowardsPlayer(Transform target)
    {
        //transform.position = Vector2.MoveTowards(transform.position, target.position, enemySpeed * Time.deltaTime);
        transform.position = Vector2.MoveTowards(transform.position, target.position, maxDistance) * Time.deltaTime * enemySpeed;
    }
}
