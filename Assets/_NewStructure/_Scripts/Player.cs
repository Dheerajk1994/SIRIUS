using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{

    public Rigidbody2D MyRigidbody { get; set; }

    public bool Attack      { get; set; }
    public bool Jump        { get; set; }
    public bool OnGround    { get; set; }


    private Animator        myAnimator;

    [SerializeField]
    private float movementSpeed;

    private bool facingRight;
    //private bool attack;

    [SerializeField]
    private bool airControl;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private LayerMask whatIsGround;

    /*
    private bool isGrounded;
    private bool jump;
    private bool jumpAttack;
    */

    

	// Use this for initialization
	void Start () 
    {
        facingRight = true;
        MyRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
	}

    void Update()
    {
        HandleInput();
    }

    // Update is called once per frame
    void FixedUpdate () 
    {
        float horizontal = Input.GetAxis("Horizontal");
        OnGround = IsGrounded();
        HandleMovement(horizontal);
        //HandleAttacks();
        HandleLayers();
        Flip(horizontal);
        //ResetValues();
	}

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //jump = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //attack = true;
            //jumpAttack = true;
        }

    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    private bool IsGrounded()
    {
        if (MyRigidbody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        //myAnimator.ResetTrigger("jump");
                        //myAnimator.SetBool("land", false);
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void HandleLayers()
    {
        if (!OnGround)
        {
            myAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            myAnimator.SetLayerWeight(1, 0);
        }

    }

    private void HandleMovement(float horizontal) 
    {
        if (MyRigidbody.velocity.y <0)
        {
            myAnimator.SetBool("land", true); 
        }
        if (!Attack && (OnGround || airControl))
        {
            MyRigidbody.velocity = new Vector2(horizontal * movementSpeed, MyRigidbody.velocity.y); 
        }
        if (Jump && MyRigidbody.velocity.y == 0)
        {
            MyRigidbody.AddForce(new Vector2(0, jumpForce)); 
        }
        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));

        /*
        if(myRigidBody.velocity.y < 0)
        {
            myAnimator.SetBool("land", true);
        }

        if (isGrounded && jump)
        {
            isGrounded = false;
            myRigidBody.AddForce(new Vector2(0, jumpForce));
            myAnimator.SetTrigger("jump");
        }

        if (!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") ) // check if need to be && (isGrounded || airControl)
        {
            myRigidBody.velocity = new Vector2(horizontal * movementSpeed, myRigidBody.velocity.y);
        }
        else if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) 
        {
            myRigidBody.velocity = new Vector2(0, 0);
        }
        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
        */
    }
    /*

    private void HandleAttacks()
    {
        if(attack && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && (isGrounded || airControl))
        {
            myAnimator.SetTrigger("attack");
            myRigidBody.velocity = Vector2.zero;
        }
        if(jump && !isGrounded && !this.myAnimator.GetCurrentAnimatorStateInfo(1).IsName("JumpAttack"))
        {
            myAnimator.SetBool("jumpAttack", true);
        }
        if(!jumpAttack && !this.myAnimator.GetCurrentAnimatorStateInfo(1).IsName("JumpAttack"))
        {
            myAnimator.SetBool("jumpAttack", false); 
        }
    }
    */

    
    /*

    private void ResetValues() 
    {
        attack = false;
        jump = false;
        jumpAttack = false;
    }
    */

}
