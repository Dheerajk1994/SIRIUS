using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoggoMovement : MonoBehaviour {

    public CharacterController2D controller;
    public Animator animator;
    public GameObject player;
    public  PlayerScript playerScript;
   
    public float runSpeed = 40f;
    public float horizontalMove = 0f;
    public bool jump = false;

    public GameObject rotatingArmPrefab;
    public GameObject rotatingArm;

    private bool spaceGunEquipped;
    private bool attack;

    public Rigidbody2D rigidbody;

    private void Start()
    {
        player = GameObject.Find("GameManagerV2").GetComponent<GameManagerScript>().player;
        playerScript = player.GetComponent<PlayerScript>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update ()
    {
        HandleInput();

        /*if (Input.GetKeyDown(KeyCode.G))
        {
            if(GameObject.Find("/PlayArea/Sam(Clone)/ArmPivot(Clone)") == null)
            {
                rotatingArm = Instantiate(rotatingArmPrefab, new Vector3(), Quaternion.identity) as GameObject;
                rotatingArm.transform.parent = player.transform;
                rotatingArm.transform.localPosition = new Vector3(-0.197f, -0.43f);
            }
            Debug.Log("Button G was pressed.");
        }*/
    }

    void FixedUpdate()
    {
        HandleMovements();
        HandleAttacks();
        ResetValues();
    }

    public void equipSpacegun()
    {
        if (GameObject.Find("/PlayArea/Sam(Clone)/ArmPivot(Clone)") == null)
        {
            rotatingArm = Instantiate(rotatingArmPrefab, new Vector3(), Quaternion.identity) as GameObject;
            rotatingArm.transform.parent = this.player.transform;
            rotatingArm.transform.localPosition = new Vector3(-0.197f, -0.43f);
            animator.SetBool("spaceGunEquipped", true);
        }
    }
    // Stops repeated jumping 
    public void OnLanding()
    {
        animator.SetBool("jump", false);
    }
   
    private void HandleMovements() 
    {
        /* Prevent Run and Attack at the same time
         * Adding layer 0, if it's not "Attack" then we move player
         */       
        if (!this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            // Move the character
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        }
    }

    private void HandleAttacks()
    {
        if (attack && !this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            animator.SetTrigger("attack");
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
        animator.SetFloat("speed", Mathf.Abs(horizontalMove));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (playerScript.currentStamina >= 10)
            {
                jump = true;
                animator.SetBool("jump", true);
                playerScript.ChangeStamina(-10);
            }

        }


    }

    private void ResetValues() 
    {
        jump = false;
        attack = false;
    }
}
