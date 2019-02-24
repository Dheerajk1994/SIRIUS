using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public GameManagerScript gameManagerScript;
    public UIScript uiScript;
    public CharacterController2D controller;
    public Animator animator;
    public GameObject player;
    private PlayerScript playerScript;
    private Rigidbody2D rigidbody;

    /*----------- PLAYER STATS -----------*/
    public float currentHealth, maxHealth;
    public float currentStamina, maxStamina;
    public float currentHunger, maxHunger;
    public float temperature;
   
    public float armor;
    public float insulation;

    public float healthRecoveryRate;
    public float staminaRecoveryRate;
    public float hungerRecoveryRate;
    /*------------------------------------*/

    /*
    public float runSpeed = 40f;
    public float horizontalMove = 0f;
    public bool jump = false;
    private bool attack;
    */

    public void Start()
    {
        Debug.Log("PlayerScript start");
        // base.Start();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        uiScript = GameObject.Find("UI").GetComponent<UIScript>();
        player = GameObject.Find("GameManager").GetComponent<GameManagerScript>().player;
        playerScript = player.GetComponent<PlayerScript>();
        rigidbody = GetComponent<Rigidbody2D>();

    }
    // checking on every frame
    private void Update()
    {
        //HandleInput();

    }

    private void FixedUpdate()
    {
        if(currentHealth < maxHealth)
        {
            ChangeHealth(healthRecoveryRate);
        }
        if(currentStamina < maxStamina)
        {
            ChangeStamina(staminaRecoveryRate);
        }
        if(currentHunger > 0)
        {
            ChangeHunger(hungerRecoveryRate);
        }

        //HandleMovements();
        //HandleAttacks();
        //ResetValues();

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth >= maxHealth) currentHealth = maxHealth;
        else if (currentHealth <= 0) currentHealth = 0.0f;

        uiScript.UpdateHealth((currentHealth / maxHealth) * 100.0f);
        if (currentHealth <= 0.0) { Die(); }
    }

    public void ChangeHealth(float health)
    {
        currentHealth += health;

        if (currentHealth >= maxHealth) currentHealth = maxHealth;
        else if (currentHealth <= 0) currentHealth = 0.0f;

        uiScript.UpdateHealth((currentHealth / maxHealth) * 100.0f);
        if (currentHealth <= 0.0) { Die(); }
    }

    public void ChangeStamina(float stamina)
    {
        currentStamina += stamina;

        if (currentStamina >= maxStamina) currentStamina = maxStamina;
        else if (currentStamina <= 0) currentStamina = 0.0f;

        uiScript.UpdateStamina((currentStamina / maxStamina) * 100.0f);
    }

    public void ChangeHunger(float hunger)
    {
        currentHunger += hunger;

        if (currentHunger >= maxHunger) currentHunger = maxHunger;
        else if (currentHunger <= 0) currentHunger = 0.0f;

        uiScript.UpdateHunger((currentHunger / maxHunger) * 100.0f);
    }


    public void Die()
    {

    }

    /*
    public void OnLanding()
    {
        animator.SetBool("jump", false);
    }

    private void HandleMovements()
    {
        // Prevent Run and Attack at the same time
        // Adding layer 0, if it's not "Attack" then we move player
         
        if (!this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            // Move the character
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        }
    }

    // Adding Attacking stuff (Sprint 8)
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
        if (Input.GetKeyDown(KeyCode.Alpha2))
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
    */

}
