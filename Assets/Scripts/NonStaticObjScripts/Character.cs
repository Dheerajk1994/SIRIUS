using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public abstract class Character : MonoBehaviour
{
    public Animator Animator { get; private set;}

    protected bool facingRight;
    protected bool attack;
    public float horizontalMove = 0f;

    [SerializeField]
    protected float runSpeed;

   

    

    // Use this for initialization
    public virtual void Start ()
    {
        Debug.Log("Character start");
        Animator = GetComponent<Animator>();
        facingRight = true;
	}

 
    // Update is called once per frame
    void Update ()
    {

	}

    public void changeDirection(float horizontal) 
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }

}
