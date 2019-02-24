using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState
{
    private Enemy enemy;
    private float idleTimer;
    private float idleDuration = 4f;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        Debug.Log("Idling");
        Idle();
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {

    }

    private void Idle()
    {
        enemy.Animator.SetFloat("speed", 0);
        // increase idleTimer by the time that has passed since a frame was rendered.
        idleTimer += Time.deltaTime;

        if(idleTimer >= idleDuration) 
        {
            enemy.ChangeState(new PatrolState());
        }
    }
}
