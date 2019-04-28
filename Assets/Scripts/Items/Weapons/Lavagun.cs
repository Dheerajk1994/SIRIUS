using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lavagun : RangeWeapon {

    void Awake()
    {
        firePoint = transform.Find("FirePoint");
        if (firePoint == null)
        {
            Debug.LogError("No FirePoint attached to Spacegun!");
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (timeToFire == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + (1 / timeToFire);
                Shoot();
            }

        }
    }
    void Shoot()
    {
        GameObject b = Instantiate(bullet, firePoint.position, firePoint.rotation);
        b.GetComponent<Bullet>().AddBulletForce(firePoint.position - firePoint2.position);
    }
}
