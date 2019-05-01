using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : WeaponClass {
    public void startAttack()
    {
        GetComponentInChildren<CapsuleCollider2D>().enabled = true;
    }

    public void endAttack()
    {
        GetComponentInChildren<CapsuleCollider2D>().enabled = false;
    }
}
