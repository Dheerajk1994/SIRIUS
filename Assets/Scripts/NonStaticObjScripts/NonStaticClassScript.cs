using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonStaticClassScript : MonoBehaviour
{

    public int CurrentHealth;
    public int MaxHealth = 100;
    public int CurrentStamina;
    public int MaxStamina = 100;
    public int AttackPower = 10;

    // Returns current health
    int getHealth()
    {
        return CurrentHealth;
    }
    
    // Returns current stamina
    int getStamina()
    {
        return CurrentStamina;
    }

    // Heals the subject up (might only be used by sam or bosses)
    void heal(int healAmount)
    {
        CurrentHealth += healAmount;
        if (CurrentHealth > MaxHealth)
            CurrentHealth = MaxHealth;
        Debug.Log("Subject has healed");
    }

    // Damages the subject
    void takeDamage(int attackPower)
    {
        CurrentHealth -= attackPower;
        if (CurrentHealth <= 0)
            Die();
        Debug.Log("Subject has taken damage");
    }

    // RIP
    void Die()
    {
        CurrentHealth = 0;
        Debug.Log("Subject has died");
    }

    // Use this for initialization
    protected virtual void Start()
    {
        // Load 
        CurrentHealth = MaxHealth;
        CurrentStamina = MaxStamina;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
