using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedState : IEnemyState {

    private Enemy enemy;

    /*---- IF OUR MOB WILL HAVE RANGED ATTACKS----*/
    private float rangedAttackTimer;
    private float rangedAttackCooldown = 3;
    private bool canPerformRangedAttack = true; //Attack without 3 second cooldown
    
    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {

        // RangedAttack();

        // check if in melee range
        if (enemy.InMeleeRange)
        {
            enemy.ChangeState(new MeleeState());
        }

        if (enemy.Target != null)
        {
            Debug.Log("Target Acquired... Following");
            enemy.Move(); 
        } 
        else
        {
            enemy.ChangeState(new IdleState());
        }
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {

    }

    // for future use (16.5)
    private void RangedAttack()
    {
        rangedAttackTimer += Time.deltaTime;
        if (rangedAttackTimer >= rangedAttackCooldown)
        {
            canPerformRangedAttack = true;
            rangedAttackTimer = 0; 
        }
        if (canPerformRangedAttack)
        {
            canPerformRangedAttack = false;
            enemy.Animator.SetTrigger("rangedAttack"); 
        }
    }
}
