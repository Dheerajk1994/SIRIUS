using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float speed = 20f;
    public Rigidbody2D rb;
    public float BulletDamage = 10f;

	// Use this for initialization
	void Start () {
        rb.velocity = transform.right * speed;
	}
	
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name);
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.TakeDamage(BulletDamage);
        }

        Destroy(gameObject);

    }
	// Update is called once per frame

}
