using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : WeaponClass {
    public void startAttack()
    {
        GetComponent<CapsuleCollider2D>().enabled = true;
    }

    public void endAttack()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if(enemy !=null)
        {
            enemy.TakeDamage(damage);
        }
    }
}
