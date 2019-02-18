using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour {

    public GameObject enemy;
    float randX;
    Vector2 spawnGround;
    public float defaultSpawnRate = 2; // 2 second spawn rate
    float nextSpawn = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextSpawn) 
        {
            nextSpawn = Time.time + defaultSpawnRate;
            randX = Random.Range(-8.4f, 8.4f);
            spawnGround = new Vector2(randX, transform.position.y);
            Instantiate(enemy, spawnGround, Quaternion.identity);
        }
    }
}
