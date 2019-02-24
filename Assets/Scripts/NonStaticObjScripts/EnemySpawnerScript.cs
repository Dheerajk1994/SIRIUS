using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour {

    public GameObject enemy;
    float randX;
    public float initialX = 60;
    public float initialY = 110;
    Vector2 spawnGround;
    public float defaultSpawnRate = 2; // 2 second spawn rate
    float nextSpawn = 0;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        /*
        if (Time.time > nextSpawn) 
        {
            nextSpawn = Time.time + defaultSpawnRate;
            randX = Random.Range(-8.4f, 8.4f);
            spawnGround = new Vector2(randX + initialX , initialY);
            Instantiate(enemy, spawnGround, Quaternion.identity);
        }
        */
    }
    public void SpawnBlob(Vector2 loc)
    {
        randX = Random.Range(-8.4f, 8.4f);
        spawnGround = new Vector2(randX + initialX, initialY);
        Instantiate(enemy, loc, Quaternion.identity);
        Debug.Log("Hello World");
    }
    public void SpawnBlobs(List<Vector2> spawnLocations)
    {
        foreach(Vector2 location in spawnLocations) {
            Instantiate(enemy, location, Quaternion.identity);
        }
    }
}
