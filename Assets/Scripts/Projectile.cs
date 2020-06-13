using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    float force;

    [SerializeField]
    float lifeTime;

    [SerializeField]
    public int damage;

    [SerializeField]
    GameObject particleEffect;

    private Rigidbody2D rb;

    private Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("DestroyProjectile", lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        //FireDirection();
        transform.Translate(Vector2.up * Time.deltaTime * force);
        //direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //transform.LookAt(direction);
    }

    void FixedUpdate() {
        //Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //rigidbody2D.AddForce(direction * Time.fixedDeltaTime * force, ForceMode2D.Impulse);
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    }

    void FireDirection() {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        rotation = Quaternion.AngleAxis(angle - 90, Vector3.down);

        //transform.LookAt(direction);
    }

    void OnTriggerEnter2D(Collider2D other) {
        //if(other.tag == "Enemy") {
        if (other.gameObject.CompareTag("Enemy")) 
        { 
            other.GetComponent<Enemy>().TakeDamage(damage);
            DestroyProjectile();
        }
       if(other.tag != "Player") {
            DestroyProjectile();
        }
    }

    void DestroyProjectile() {
        Instantiate(particleEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
