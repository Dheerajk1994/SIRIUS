using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : Character
{

    public GameManagerScript gameManagerScript;
    public UIScript uiScript;
    public CharacterController2D controller;
    //public Animator Animator;
    public GameObject player;
    [SerializeField]
    private EdgeCollider2D BarkCollider;
    private PlayerScript playerScript;

    // private Rigidbody2D rigidbody;

    /*----------- PLAYER STATS -----------*/
    //protected float currentHealth, maxHealth;
    //protected float currentStamina, maxStamina;
    //protected float currentHunger, maxHunger;
    //protected float temperature;

    public bool healState;
    public float armor;
    public float insulation;

    public float healthRecoveryRate;
    public float staminaRecoveryRate;
    public float hungerRecoveryRate;



    /*------------------------------------*/


    public override void Start()
    {
        //Debug.Log("PlayerScript start");
        // base.Start();
        player = gameManagerScript.player;
        playerScript = player.GetComponent<PlayerScript>();
        // rigidbody = GetComponent<Rigidbody2D>();

    }
    // checking on every frame
    private void Update()
    {


    }

    //called by game manager
    public void SetPlayerScript(GameManagerScript gmScript, UIScript uScript)
    {
        gameManagerScript = gmScript;
        uiScript = uScript;
    }

    private void FixedUpdate()
    {
        // Natural Recovery
        if (currentHealth < maxHealth)
        {
            ChangeHealth(healthRecoveryRate);
        }
        if (currentStamina < maxStamina)
        {
            ChangeStamina(staminaRecoveryRate);
        }
        if (currentHunger > 0)
        {
            ChangeHunger(hungerRecoveryRate);
        }

        // Check states
        if (healState)
        {
            if (currentHealth < maxHealth)
            {
                ChangeHealth(healthRecoveryRate * 2);
            }
        }
    }


    public void ChangeHealth(float health)
    {
        currentHealth += health;

        if (currentHealth >= maxHealth) currentHealth = maxHealth;
        else if (currentHealth <= 0) currentHealth = 0.0f;

        uiScript.PlayerAttributePanel.GetComponent<PlayerAttributesPanelScript>().UpdateHealth((currentHealth / maxHealth) * 100.0f);
        if (currentHealth <= 0.0) { Die(); }
    }

    public void ChangeStamina(float stamina)
    {
        currentStamina += stamina;

        if (currentStamina >= maxStamina) currentStamina = maxStamina;
        else if (currentStamina <= 0) currentStamina = 0.0f;

        uiScript.PlayerAttributePanel.GetComponent<PlayerAttributesPanelScript>().UpdateStamina((currentStamina / maxStamina) * 100.0f);
    }

    public void ChangeHunger(float hunger)
    {
        currentHunger += hunger;

        if (currentHunger >= maxHunger) currentHunger = maxHunger;
        else if (currentHunger <= 0) currentHunger = 0.0f;

        uiScript.PlayerAttributePanel.GetComponent<PlayerAttributesPanelScript>().UpdateHunger((currentHunger / maxHunger) * 100.0f);
    }


    public void Die()
    {

    }

    public void MeleeAttack()
    {
        //BarkCollider.enabled = !BarkCollider.enabled;
    }


    public void TakeDamage1(float damage)
    {
        currentHealth -= damage;

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0)
        {
            currentHealth = 0.0f;
        }
        uiScript.PlayerAttributePanel.GetComponent<PlayerAttributesPanelScript>().UpdateHealth((currentHealth / maxHealth) * 100.0f);
        if (currentHealth <= 0.0)
        {
            Die();
        }
    }

    public override bool IsDead
    {
        get
        {
            return currentHealth <= 0.0;
        }
    }

    public override IEnumerator TakeDamage(float damage)
    {
        yield return null;
    }
}
