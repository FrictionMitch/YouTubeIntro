using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject projectile;

    [SerializeField]
    Transform fireStartPosition;

    [SerializeField]
    GameObject reticle;

    [SerializeField]
    float fireDelay = .2f;

    public Vector3 strumPosition;

    bool canShoot = false;
    float timeSinceShot = 0f;
    Quaternion rotation;
    Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        strumPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        RotateWeapon();
        Fire1();
    }

    void RotateWeapon() {
        //direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        direction = reticle.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        //rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //transform.rotation = rotation;  // used to rotate the actual weapon
    }

    void Fire1() 
    {
        timeSinceShot += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            //if (Input.GetAxisRaw("Right Stick Horizontal") != 0 || Input.GetAxisRaw("Right Stick Vertical") !=  0) 
            {
                if (timeSinceShot >= fireDelay) {
                    GameObject bullet = Instantiate(projectile, fireStartPosition.position, rotation);
                    strumPosition = player.transform.position;
                    timeSinceShot = 0;
                } 
            }
        }
    }

    IEnumerator ShootDelay() {
        yield return new WaitForSeconds(fireDelay);
        GameObject bullet = Instantiate(projectile, fireStartPosition.position, rotation);
    }
}
