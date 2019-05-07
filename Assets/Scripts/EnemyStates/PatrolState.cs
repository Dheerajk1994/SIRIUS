using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState {

    private Enemy enemy;
    private float patrolTimer;
    private float patrolDuration = 6;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        //Debug.Log("Enemy Patroling");
        Patrol();
        //enemy.Move();
        enemy.MovePatrol();

        // maybe future implementation with ranged attack for diff enemies?  
        // if (enemy.Target !=null && enemy.InRangedAttackRange)
        if (enemy.Target !=null)
        {
            //Debug.Log("Target Acquired... Following");
            enemy.ChangeState(new RangedState()); 
        }
    }

    public void Exit()
    {

    }


    public void OnTriggerEnter(Collider2D other)
    {
        // for big monsters that we restrict where it can move on the map move
        // future direciton changing things done here
        // eg. if (other.tag == "some obstacle") enemy.ChangeDirection();
    }

    private void Patrol()
    {
        patrolTimer += Time.deltaTime;

        if (patrolTimer >= patrolDuration)
        {
            enemy.ChangeState(new IdleState());
        }
    }
}
