using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class CharacterFinal : MonoBehaviour {

    #region COMPONENTS
    public Animator MyAnimator { get; private set; }
    #endregion

    #region VARIABLES
    [SerializeField]
    private EdgeCollider2D BarkCollider;

    [SerializeField]
    protected float currentHealth;

    protected bool facingRight;
    public Vector2 currentPosition;
    protected List<Vector2> path;

    [SerializeField] private List<string> damageSources;

    [SerializeField] protected float movementSpeed;

    [SerializeField] protected float chaseSpeed = 5f;
    [SerializeField]
    protected float patrolSpeed = 2f;
    #endregion


    #region REFERENCES
    protected TerrainManagerScript terrainManagerScript;
    
#endregion
    public float standardDamage;

    public abstract bool IsDead { get;  }
    public bool Attack { get; set; }
    public bool TakingDamage { get; set; }

    /*
     * public float currentHealth, maxHealth;
    public float currentStamina, maxStamina;
    public float currentHunger, maxHunger;
    public float temperature;
*/  

    // Use this for initialization
    protected virtual void Start () 
    {
        facingRight = true;
        MyAnimator = GetComponent<Animator>();
        terrainManagerScript = GameObject.Find("TerrainManager(Clone)").GetComponent<TerrainManagerScript>();
    }
	
	// Update is called once per frame
	protected virtual void Update ()
    {
		currentPosition = this.transform.position;
	}

    public void ChangeDirection()
    {
        facingRight = !facingRight;
        //transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
        transform.Rotate(0f, 180, 0f);
    }

    public void BarkAttack()
    {
       // BarkCollider.enabled = !BarkCollider.enabled;
    }

    public bool getFacingDirection()
    {
        return facingRight;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (damageSources.Contains(other.tag))
        {
            TakeDamage(standardDamage);
        }
    }



    public abstract void TakeDamage(float damage);


}
