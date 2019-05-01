 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeState : IEnemyState
{
    private Enemy enemy;
    private float meleeAttackTimer;
    private float meleeAttackCooldown = 3;
    private bool canPerformMeleeAttack = true;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {

        MeleeAttack();

        // when player not in melee range 
        if(!enemy.InMeleeRange)
        {
            enemy.ChangeState(new RangedState()); 
        }
        else if(enemy.Target == null)
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

    private void MeleeAttack()
    {
        //Debug.Log("Performing Melee Attack");
        meleeAttackTimer += Time.deltaTime;
        if (meleeAttackTimer >= meleeAttackCooldown)
        {
            canPerformMeleeAttack = true;
            meleeAttackTimer = 0;
        }
        if (canPerformMeleeAttack)
        {
            canPerformMeleeAttack = false;
            enemy.MyAnimator.SetTrigger("attack");
        }
    }
}
