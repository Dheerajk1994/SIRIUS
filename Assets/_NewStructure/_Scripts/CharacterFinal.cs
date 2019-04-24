using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterFinal : MonoBehaviour {

    public Animator MyAnimator { get; private set; }

    [SerializeField]
    private EdgeCollider2D BarkCollider;

    [SerializeField]
    protected float currentHealth;

    protected bool facingRight;

    [SerializeField]
    private List<string> damageSources;

    [SerializeField]
    protected float movementSpeed;

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
    public virtual void Start () 
    {
        facingRight = true;
        MyAnimator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ChangeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }

    public void BarkAttack()
    {
        BarkCollider.enabled = !BarkCollider.enabled;
    }


    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (damageSources.Contains(other.tag))
        {
            StartCoroutine(TakeDamage(standardDamage));
        }
    }



    public abstract IEnumerator TakeDamage(float damage);


}
