using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponNoRotation : MonoBehaviour
{
    [SerializeField]
    GameObject projectile;

    [SerializeField]
    Transform fireStartPosition;

    [SerializeField]
    GameObject reticle;

    [SerializeField]
    float fireDelay = .2f;

    bool canShoot = false;
    float timeSinceShot = 0f;
    Quaternion rotation;
    Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //RotateWeapon();
        Fire();
    }
    void Fire() {
        direction = (reticle.transform.position - transform.position).normalized;
        timeSinceShot += Time.deltaTime;
        //if (Input.GetButton("Fire1")){
        if (Input.GetAxisRaw("Right Stick Horizontal") != 0 || Input.GetAxisRaw("Right Stick Vertical") !=  0) {
            //StartCoroutine(ShootDelay());
            if (timeSinceShot >= fireDelay) {
                GameObject bullet = Instantiate(projectile, fireStartPosition.position, rotation);
                //GameObject bullet = Instantiate(projectile, fireStartPosition.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y);
                timeSinceShot = 0;
            } 
        }
    }

    IEnumerator ShootDelay() {
        yield return new WaitForSeconds(fireDelay);
        GameObject bullet = Instantiate(projectile, fireStartPosition.position, rotation);
    }
}
