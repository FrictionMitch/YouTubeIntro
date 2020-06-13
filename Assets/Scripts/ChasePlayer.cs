using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    public bool bCanRotate = false;
    public Transform player;
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        TargetDirection(player);
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    void TargetDirection(Transform target)
    {
        Vector3 direction = target.position - transform.position;
        if (bCanRotate)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // for rotation only
            rb.rotation = angle; // for rotation only
        }
        direction.Normalize();
        movement = direction;
    }
}
