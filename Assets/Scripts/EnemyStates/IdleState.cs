using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState
{
    private Enemy enemy;
    private float idleTimer;
    [SerializeField] private float idleDuration = 3;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        // Debug.Log("Enemy Idling");
        Idle();

        // switch to new state when we see something, switch to patrol state
        // since patrol state already making the subject move we will use this 
        if (enemy.Target != null)
        {
            //Debug.Log("Target Acquired");
            enemy.ChangeState(new PatrolState()); 
        }
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {

    }

    private void Idle()
    {
        enemy.MyAnimator.SetFloat("speed", 0);
        // increase idleTimer by the time that has passed since a frame was rendered.
        idleTimer += Time.deltaTime;

        if(idleTimer >= idleDuration) 
        {
            enemy.ChangeState(new PatrolState());
        }
    }
}
