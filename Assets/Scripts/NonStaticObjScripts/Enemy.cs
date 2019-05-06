using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterFinal
{
    public string thisEnemiesName;
    private IEnemyState currentState;
    private AudioSource[] enemySounds;
    private AudioSource deathSound;
    private AudioSource damageSound;

    //[SerializeField]
    //private InventoryControllerScript inventoryControllerScript;
    //
    //[SerializeField]
    //private InventorySpritesScript inventorySprites;

    [SerializeField]
    private float meleeRange;

    [SerializeField]
    private float knockBackForce = 100f;

    [SerializeField]
    private float rangedAttackRange;

    private Vector2 targetPos;

    [SerializeField]
    private bool isRangedAI;
    public GameObject Target { get; set; }

    [SerializeField]
    private GameObject LootPrefab;

    //test
    private ushort[,] terrain;
    private bool waitingForPath;

    // Use this for initialization 
    protected override void Start()
    {
        base.Start();
        ChangeState(new IdleState());
        this.GetComponent<SpriteRenderer>().sortingOrder = 13;
        path = new List<Vector2>();
        targetPos = new Vector2();
        enemySounds = this.GetComponents<AudioSource>();
        deathSound = enemySounds[0];
        damageSound = enemySounds[1];
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        
        if (!TakingDamage)
        {
            currentState.Execute();
        }

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
    //COLLISION IGNORE
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision.collider, this.gameObject.GetComponent<Collider2D>());
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
        waitingForPath = false;
        if (pPath.Count > 0)
        {
            path = pPath;
            targetPos = path[0];
            //targetPos = path[0];
            Move();
        }
    }

    // hostile

    bool isPatrolling = false;
    public void MoveRanged()
    {
        movementSpeed = chaseSpeed;
        Player target = Target.GetComponent<Player>();
        Vector2 targetPos = new Vector2(target.currentPosition.x, target.currentPosition.y - 1);
        ushort pathFindingRadius = (ushort)(Vector2.Distance(targetPos, this.currentPosition) + 5);

        if (isRangedAI)
        {
            
        }
        else if(!waitingForPath)
        {
            //if path is not empty
            if(isPatrolling)
            {
                //Debug.Log("moving from patrol to chase");
                path.Clear();
                isPatrolling = false;
                terrain = terrainManagerScript.GetSurroundingTileValues(this.currentPosition, pathFindingRadius);
                //AStar.FindPath(this.currentPosition, targetPos, terrain, this);
                AIPathManagerScript.instance.SubmitHighPrioPathRequest(currentPosition, targetPos, terrain, SetPotentialPath);//new
                waitingForPath = true;
            }
            else
            {
                if(path.Count == 0)
                {
                    terrain = terrainManagerScript.GetSurroundingTileValues(this.currentPosition, pathFindingRadius);
                    //AStar.FindPath(this.currentPosition, targetPos, terrain, this);
                    AIPathManagerScript.instance.SubmitHighPrioPathRequest(currentPosition, targetPos, terrain, SetPotentialPath);//new
                    waitingForPath = true;
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
        else if(!waitingForPath)
        {
            //terrainManagerScript.GetSurroundingTileValues(this.currentPosition, 5);
            Vector2 patrolToPosition = FindTileToMoveTo();
            ushort pathFindingRadius = (ushort)(Vector2.Distance(patrolToPosition, this.currentPosition) + 5);
            terrain = terrainManagerScript.GetSurroundingTileValues(this.currentPosition, pathFindingRadius);
            //AStar.FindPath(currentPosition, patrolToPosition, terrainManagerScript.GetSurroundingTileValues(this.currentPosition, pathFindingRadius), this);
            AIPathManagerScript.instance.SubmitLowPrioPathRequest(currentPosition, patrolToPosition, terrain, SetPotentialPath);//new
            waitingForPath = true;
        }
    }


    public void Move()
    {
        //Debug.Log("move called");
        if (Vector2.Distance(currentPosition, targetPos) < 0.5f) path.Remove(targetPos);
        if (path.Count == 0) return;
        targetPos = path[0];
        if (Vector2.Distance(targetPos, currentPosition) > 3f) path.Clear();//so it wont jump
        if (targetPos.y > currentPosition.y) this.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 30f);
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
        //Debug.Log("FindTileToMoveTo called");
        ushort tries = 0;

        int relXPos = Mathf.FloorToInt(currentPosition.x);
        int y = Mathf.FloorToInt(currentPosition.y);

        int xLook = 10;
        int yLook = 6;

        int leftOrRight = UnityEngine.Random.Range(0, 2);
        if (leftOrRight == 0)//left
        {
            relXPos -= UnityEngine.Random.Range(0, xLook);
            relXPos = terrainManagerScript.GetRelativeXPos(relXPos);
            for (int yPos = yLook / 2; yPos > -yLook/2; --yPos)
            {
                if(terrainManagerScript.frontTilesValue[relXPos,y + yPos] == 0 && terrainManagerScript.frontTilesValue[relXPos, y + yPos - 1] != 0)
                {
                    return new Vector2(relXPos, y + yPos);
                }
            }
        }
        else
        {
            relXPos += UnityEngine.Random.Range(0, xLook);
            terrainManagerScript.GetRelativeXPos(relXPos);
            for (int yPos = yLook / 2; yPos > -yLook / 2; --yPos)
            {
                if (terrainManagerScript.frontTilesValue[relXPos, y + yPos] == 0 && terrainManagerScript.frontTilesValue[relXPos, y + yPos - 1] != 0)
                {
                    return new Vector2(relXPos, y + yPos);
                }
            }
        }

        return currentPosition;
    }



    public Vector2 GetDirection() 
    {
        return facingRight ? Vector2.left : Vector2.right;
    }

    //public override void OnTriggerEnter2D(Collider2D other)
    //{
    //    base.OnTriggerEnter2D(other);
    //    currentState.OnTriggerEnter(other);
    //}



    public override void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(!IsDead)
        {
            damageSound.Play();
            //Debug.Log("Taken Damage");
            MyAnimator.SetTrigger("damage");  
        }
        else
        {
            
            //MyAnimator.SetTrigger("Enemy.TakeDamage: Destroying this.GameObject and Calling SetLootDrop");
            SetLootDrop(700, 1, InventorySpritesScript.instance.GetSprite(700), this.gameObject.transform.localPosition);

            //deathSound.Play();
            QuestManagerScript.instance.KilledMob(thisEnemiesName, 1);
            Destroy(this.gameObject); 
        }
    }

    public void TakeDamage(Vector3 attackerPosition, float damage)
    {
        currentHealth -= damage;
       

        if (!IsDead)
        {
            damageSound.Play();
            Vector2 knockBackDirection =  new Vector2(transform.position.x - attackerPosition.x, 0);
            GetComponent<Rigidbody2D>().AddForce(knockBackDirection * knockBackForce);
            Debug.Log("Enemy.TakeDamage: Enemey was knocked back");
            //Debug.Log("Taken Damage");
            MyAnimator.SetTrigger("damage");
        }
        else
        {

            //MyAnimator.SetTrigger("Enemy.TakeDamage: Destroying this.GameObject and Calling SetLootDrop");
            SetLootDrop(700, 1, InventorySpritesScript.instance.GetSprite(700), this.gameObject.transform.localPosition);

            //deathSound.Play();
            QuestManagerScript.instance.KilledMob(thisEnemiesName, 1);
            Destroy(this.gameObject);
        }
    }

    private void SetLootDrop(ushort id, ushort amnt, Sprite img, Vector2 pos)
    {
        GameObject lootDrop = Instantiate(LootPrefab);
        lootDrop.GetComponent<TilePickUpScript>().SetTilePickup(null, id, amnt, img);
        lootDrop.transform.position = pos;
        //lootDrop.GetComponent<Rigidbody2D>().AddForce((this.gameObject.transform.localPosition - lootDrop.transform.localPosition) * 50f);
    }

    //private void OnDrawGizmos()
    //{
    //    if (terrain != null)
    //    {
    //        Gizmos.color = Color.blue;
    //        for (int x = 0; x < terrain.GetLength(0); ++x)
    //        {
    //            for (int y = 0; y < terrain.GetLength(1); ++y)
    //            {
    //                if (terrain[x, y] == 0)
    //                {
    //                    Gizmos.DrawCube(new Vector2(x + this.currentPosition.x - terrain.GetLength(0) / 2, y + this.currentPosition.y - terrain.GetLength(1) / 2), Vector3.one * 0.3f);
    //                }
    //            }
    //        }
    //    }
    //    if (path == null || path.Count == 0) return;
    //    Gizmos.color = Color.red;
    //    foreach (Vector2 pos in path)
    //    {
    //        Gizmos.DrawCube(pos, Vector3.one * 0.3f);
    //    }
    //}


}

