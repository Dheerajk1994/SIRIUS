using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spacegun : MonoBehaviour {

    public Transform firePoint;
    public GameObject bullet;

    public float fireRate = 0;
    public float Damage = 10;
    public LayerMask whatToHit;

    float timeToFire = 0;
   
    void Awake()
    {
        firePoint = transform.Find("FirePoint");
        if(firePoint == null)
        {
            Debug.LogError("No FirePoint attached to Spacegun!");
        }
    }

    // Update is called once per frame
    void Update () {
       
        if (fireRate == 0)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if(Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + (1 / fireRate);
                Shoot();
            }
                
        }
	}
    void Shoot()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }
}
