using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character 
{
    private IEnemyState currentState;

    [SerializeField]
    private float meleeRange;
    [SerializeField]
    private float rangedAttackRange;


    public bool InMeleeRange 
    { 
        get 
        { 
            if (Target!=null)
            {
                // gives length between enemy position and player
                return Vector2.Distance(transform.position, Target.transform.position) <= meleeRange;
            }
            return false;
        }
    }

    public bool InRangedAttackRange
    {
        get
        {
            if (Target != null)
            {
                // gives length between enemy position and player
                return Vector2.Distance(transform.position, Target.transform.position) <= rangedAttackRange;
            }
            return false;
        }
    }

    public GameObject Target { get; set; }

    public override bool IsDead
    {
        get
        {
            return currentHealth <= 0.0;
        }
    }

    // Use this for initialization 
    public override void Start()
    {
        base.Start();
        ChangeState(new IdleState());
        this.GetComponent<SpriteRenderer>().sortingOrder = 13;
    }

    // Update is called once per frame
    void Update()
    {
        //if (!IsDead) 
        //{ 
        //    if(!IsTakingDamage)
        //    {
        //        currentState.Execute();
        //    }
        // LookAtTarget();
        //}
            currentState.Execute();
            LookAtTarget();
        
    }

    private void LookAtTarget()
    {
        if (Target != null)
        {
            float xDir = Target.transform.position.x - transform.position.x;
            if (xDir < 0 && !facingRight || xDir > 0 && facingRight)
            {
                ChangeDirection();
            }
        }
    }


    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter(this);
    }

    public void Move() 
    {
        if (!attack) 
        {
            runSpeed = 3;
            Animator.SetFloat("speed", 3);
            transform.Translate(GetDirection() * (runSpeed * Time.deltaTime));
        }
    }

    public Vector2 GetDirection() 
    {
        return facingRight ? Vector2.left : Vector2.right;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        currentState.OnTriggerEnter(other);
    }

    public override IEnumerator TakeDamage(float damage)
    {
        currentHealth -= damage;

        if(!IsDead)
        {
            Debug.Log("Taken Damage");
            Animator.SetTrigger("damage");  
        }
        else
        {
            Animator.SetTrigger("die");
            yield return null;  
        }
    }





}

