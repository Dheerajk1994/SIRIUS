using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerTest : MonoBehaviour {

    public GameObject enemyPrefab;

    public void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab);
        enemy.transform.position = GameObject.Find("Sam(Clone)").transform.position;
    }

}
