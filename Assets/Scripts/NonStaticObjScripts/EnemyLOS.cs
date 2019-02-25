using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLOS : MonoBehaviour {

    [SerializeField]
    private Enemy enemy;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            // set player as the target to follow
            enemy.Target = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            enemy.Target = null; 
        }
    }
}
