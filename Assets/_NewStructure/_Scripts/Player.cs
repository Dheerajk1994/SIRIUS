using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterFinal 
{
    public GameManagerScript gameManagerScript;
    public UIScript uiScript;

    private static Player instance;
    //there should be only one player
    // void Awake(){
    //     if(instance == null){
    //         instance = this;
    //     }
    //     else if(instance != this){
    //         Destroy(this);
    //     }
    // }

    public void SetPlayerScript(GameManagerScript gmScript, UIScript uScript)
    {
        gameManagerScript = gmScript;
        uiScript = uScript;
    }

    public static Player Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Player>();
            }
            return instance;
        }
    }

    public Rigidbody2D MyRigidbody { get; set; }


    public bool Jump        { get; set; }
    public bool OnGround    { get; set; }

    public override bool IsDead
    {
        get
        {
            return currentHealth <= 0.0;
        }
    }

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
	protected override void Start () 
    {
        base.Start();
        MyRigidbody = GetComponent<Rigidbody2D>();
       
	}

    protected override void Update()
    {
        base.Update();
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
            MyAnimator.SetTrigger("jump");
            //jump = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            MyAnimator.SetTrigger("attack");
            //attack = true;
            //jumpAttack = true;
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
            MyAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            MyAnimator.SetLayerWeight(1, 0);
        }

    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            this.ChangeDirection();
        }
    }

    private void HandleMovement(float horizontal) 
    {
        //Debug.Log(movementSpeed);
        if (MyRigidbody.velocity.y <0)
        {
            MyAnimator.SetBool("land", true); 
        }
        if (!Attack && (OnGround || airControl))
        {
            MyRigidbody.velocity = new Vector2(horizontal * movementSpeed, MyRigidbody.velocity.y); 
        }
        if (Jump && MyRigidbody.velocity.y == 0)
        {
            //MyRigidbody.AddForce(new Vector2(0, jumpForce)); 
            MyRigidbody.AddForce(Vector2.up * jumpForce); 
        }
        MyAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    public override IEnumerator TakeDamage(float damage)
    {
        yield return null;
    } 
}
