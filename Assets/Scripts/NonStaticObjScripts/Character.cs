using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public abstract class Character : MonoBehaviour
{
    public Animator Animator { get; set;}
   //[SerializeField]
    protected float runSpeed;
    public float standardDamage;

    [SerializeField]
    protected float currentHealth, maxHealth;
    public float currentStamina, maxStamina;
    [SerializeField]
    protected float currentHunger, maxHunger;
    [SerializeField]
    protected float temperature;


    public abstract bool IsDead { get; }
    public bool IsTakingDamage { get; set;  }
    public bool facingRight;
    public bool attack;
    public float horizontalMove = 0f;

    // Use this for initialization
    public virtual void Start ()
    {
        Debug.Log("Character start");
        Animator = GetComponent<Animator>();
        //Animator.GetComponent<Character>().attack = true;
        facingRight = true;
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

    public virtual void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag =="Attack")
        {
            StartCoroutine(TakeDamage(standardDamage)); 
        }
    }



    public abstract IEnumerator TakeDamage(float damage);
}
