using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealState : MonoBehaviour {

    public bool healToggle;
    public GameObject player;
    public PlayerScript playerScript;

    public void Start()
    {
        player = GameObject.Find("GameManager").GetComponent<GameManagerScript>().player;
        playerScript = player.GetComponent<PlayerScript>();
    }
    public void Update()
    {
        if (playerScript.currentHealth < playerScript.maxHealth)
        {
            playerScript.ChangeHealth(playerScript.healthRecoveryRate);
        }
    }
}
