using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : WeaponClass {

    public Transform firePoint;
    public Transform firePoint2;
    public GameObject bullet;
   
    public float timeToFire = 0;

    void Awake()
    {
        firePoint = transform.Find("FirePoint");
        if (firePoint == null)
            Debug.LogError("No FirePoint attached to Spacegun!");
    }
	
    void Shoot()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }
}
