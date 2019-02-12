using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoggoMovement : MonoBehaviour {

    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;

    public GameObject player;
    private PlayerScript playerScript;

    private void Start()
    {
        player = GameObject.Find("GameManager").GetComponent<GameManagerScript>().player;
        playerScript = player.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update () {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if(Input.GetButtonDown("Jump"))
        {
            if(playerScript.currentStamina >= 10)
            {
                jump = true;
                animator.SetBool("isJump", true);
                playerScript.ChangeStamina(-10);
            }
            
        }
	}

    // Stops repeated jumping 
    public void OnLanding()
    {
        animator.SetBool("isJump", false);
    }
   
    void FixedUpdate () {
        //Move the character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;  
    }
    
}
