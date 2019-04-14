using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterFinal
{
    private IEnemyState currentState;

    [SerializeField]
    private float meleeRange;

    [SerializeField]
    private float rangedAttackRange;
    private Vector2 targetPos;

    [SerializeField]
    private bool isRangedAI;
    public GameObject Target { get; set; }

    // Use this for initialization 
    protected override void Start()
    {
        base.Start();
        ChangeState(new IdleState());
        this.GetComponent<SpriteRenderer>().sortingOrder = 13;
        path = new List<Vector2>();
        targetPos = new Vector2();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        //if (!IsDead)
        //{
        if (!TakingDamage)
        {
            currentState.Execute();
        }

        LookAtTarget();
        // }

    }

    public bool InMeleeRange
    {
        get
        {
            if (Target != null)
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

    public override bool IsDead
    {
        get
        {
            return currentHealth <= 0.0;
        }
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

    public void SetPotentialPath(List<Vector2> pPath)
    {
        //Debug.Log("a* returned with path " + pPath.Count);
        if (pPath.Count > 0)
        {
            path = pPath;
            targetPos = path[0];
        }
    }

    // hostile

    bool isPatrolling = false;
    public void MoveRanged()
    {
        movementSpeed = chaseSpeed;
        if(isRangedAI)
        {
            
        }
        else
        {
            //if path is not empty
            if(isPatrolling)
            {
                path.Clear();
                isPatrolling = false;
                AStar.FindPath(this.currentPosition, Target.GetComponent<Player>().currentPosition, terrainManagerScript.frontTilesValue, this);
                targetPos = path[0];
                Move();
            }
            else{
                if(path.Count == 0)
                {
                    AStar.FindPath(this.currentPosition, Target.GetComponent<Player>().currentPosition, terrainManagerScript.frontTilesValue, this);
                    targetPos = path[0];
                    Move();
                }
                else{
                    Move();
                }
            }

        } 
    }

    //walk around randomly
    public void MovePatrol()
    {
        isPatrolling = true;
        movementSpeed = patrolSpeed;
        if(path.Count > 0)
        {
            Move();
        }
        else
        {
            AStar.FindPath(currentPosition, FindTileToMoveTo(), terrainManagerScript.frontTilesValue, this);
        }
    }



    public void Move()
    {
        if (Vector2.Distance(currentPosition, targetPos) < 0.5f) path.Remove(targetPos);
        if (path.Count == 0) return;
        targetPos = path[0];
        if (targetPos.y > currentPosition.y) this.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 50f);
        transform.position = Vector2.MoveTowards(currentPosition, targetPos, Time.deltaTime * movementSpeed);
        //if (!Attack)
        //{
        //    movementSpeed = 3;
        //    MyAnimator.SetFloat("speed", 3); 
        //    transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));//we might need to change this
        //}
    }

    public Vector2 FindTileToMoveTo()//terrible 
    {
        ushort tries = 0;

        int x = Mathf.FloorToInt(currentPosition.x);
        int y = Mathf.FloorToInt(currentPosition.y);

        int xLook = 20;
        int yLook = 10;

        int leftOrRight = UnityEngine.Random.Range(0, 2);
        if (leftOrRight == 0)//left
        {
            x -= UnityEngine.Random.Range(0, xLook);
            for(int yPos = yLook / 2; yPos > -yLook/2; --yPos)
            {
                if(terrainManagerScript.frontTilesValue[x,y + yPos] == 0 && terrainManagerScript.frontTilesValue[x, y + yPos - 1] != 0)
                {
                    return new Vector2(x, y + yPos);
                }
            }
        }
        else
        {
            x += UnityEngine.Random.Range(0, xLook);
            for (int yPos = yLook / 2; yPos > -yLook / 2; --yPos)
            {
                if (terrainManagerScript.frontTilesValue[x, y + yPos] == 0 && terrainManagerScript.frontTilesValue[x, y + yPos - 1] != 0)
                {
                    return new Vector2(x, y + yPos);
                }
            }
        }

        return currentPosition;
    }



    public Vector2 GetDirection() 
    {
        return facingRight ? Vector2.left : Vector2.right;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        currentState.OnTriggerEnter(other);
    }



    public override IEnumerator TakeDamage(float damage)
    {
        currentHealth -= damage;

        if(!IsDead)
        {
            Debug.Log("Taken Damage");
            MyAnimator.SetTrigger("damage");  
        }
        else
        {
            MyAnimator.SetTrigger("die");
            //Destroy(this, 2f);
            yield return null;  
        }
    }


    private void OnDrawGizmos()
    {
        if (path == null || path.Count == 0) return;
        Gizmos.color = Color.red;
        foreach (Vector2 pos in path)
        {
            Gizmos.DrawCube(pos, Vector3.one * 0.3f);
        }
    }



}

