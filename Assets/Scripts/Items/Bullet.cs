using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float speed = 200f;
    public Rigidbody2D rb;
    public float BulletDamage = 10f; 

	// Use this for initialization
	void Start () {
        
	}
	
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        //Debug.Log(hitInfo.name);
        if(hitInfo.gameObject.CompareTag("Bullet")){
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), hitInfo.GetComponent<Collider2D>());
        }
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.TakeDamage(BulletDamage);
        }

        Destroy(gameObject);

    }
	// Update is called once per frame
    public void AddBulletForce(Vector2 direction)
    {
        rb.AddForce(direction * speed);
    }

}
