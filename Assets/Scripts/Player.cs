using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Action OnShoot;

    [SerializeField]
    float deathDelay;

    [SerializeField]
    float speed = 5f;

    [SerializeField]
    int health;

    Animator animator;
    Rigidbody2D rigidBody2D;
    Vector2 moveAmount;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        Vector2 moveInput = new Vector2(horizontalMovement, verticalMovement);
        moveAmount = moveInput.normalized * speed;

        if(moveAmount != Vector2.zero) {
            animator.SetBool("isRunning", true);
        } else {
            animator.SetBool("isRunning", false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void FixedUpdate() {
        rigidBody2D.MovePosition(rigidBody2D.position + moveAmount * Time.fixedDeltaTime);
    }

    void LateUpdate() {
        //ClampTarget();
    }

    void ClampTarget() {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        //pos.x = Mathf.Clamp01(pos.x);
        //pos.y = Mathf.Clamp01(pos.y);
        if(pos.x > 1) {
            pos.x = 1;
        }
        if(pos.x < 0) {
            pos.x = 0;
        }
        transform.position = Camera.main.ViewportToWorldPoint(pos);

    }

    public void TakeDamage(int damageAmount) {
        if (health <= 0) {
            DestroySelf();
        }
        health = health - damageAmount;
    }

    void DestroySelf() {
        Destroy(gameObject, deathDelay);
    }

    void Shoot()
    {
        if(OnShoot != null)
        {
            OnShoot();
        }
    }

}
