using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoggoMovement : MonoBehaviour {

    public CharacterController2D controller;
    public Animator Animator;
    public GameObject player;
    public  PlayerScript playerScript;
   
    public float runSpeed = 40f;
    public float horizontalMove = 0f;
    public bool jump = false;

    // Sprint 8 
    private bool attack;
    public Rigidbody2D rigidbody;

    private void Start()
    {
        player = GameObject.Find("GameManager").GetComponent<GameManagerScript>().player;
        playerScript = player.GetComponent<PlayerScript>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update ()
    {
        // Sprint 8
        HandleInput();

    }

    void FixedUpdate()
    {
        /*
        //Move the character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false
        */
        HandleMovements();
        HandleAttacks();
        ResetValues();

    }


    // Stops repeated jumping 
    public void OnLanding()
    {
        Animator.SetBool("jump", false);
    }
   
    private void HandleMovements() 
    {
        /* Prevent Run and Attack at the same time
         * Adding layer 0, if it's not "Attack" then we move player
         */       
        if (!this.Animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            // Move the character
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        }
    }

    // Adding Attacking stuff (Sprint 8)
    private void HandleAttacks()
    {
        if (attack && !this.Animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            Animator.SetTrigger("attack");
            rigidbody.velocity = Vector2.zero; 
        }
    }
    

    private void HandleInput()
    {
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            attack = true;
        }

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        Animator.SetFloat("speed", Mathf.Abs(horizontalMove));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (playerScript.currentStamina >= 10)
            {
                jump = true;
                Animator.SetBool("jump", true);
                playerScript.ChangeStamina(-10);
            }

        }

    }

    // function that resets all values for jump/attack
    private void ResetValues() 
    {
        jump = false;
        attack = false;
    }
}
