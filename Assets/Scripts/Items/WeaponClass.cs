using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponClass : ItemClass {


    public LayerMask whatToHit;

    public int damage = 0;
    public float attackSpeed = 0;
}
