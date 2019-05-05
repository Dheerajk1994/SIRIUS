using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(GetComponentInParent<Enemy>().standardDamage);
            Debug.Log("Enemy.OnTriggerEnter2D: Called player.TakeDamage(" + GetComponentInParent<Enemy>().standardDamage);

        }
    }
}
