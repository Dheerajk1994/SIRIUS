using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MovableObject {
    public int base_health = 100;
    public int current_health;
    public int armour = 0;
    public int base_stamina = 100;
    public int current_stamina;

    private Animator animator;


	// Use this for initialization
	void Start ()
    {
        animator = GetComponent<Animator>();
	}

    private void OnDisable()
    {
    }

    // Update is called once per frame
    void Update ()
    {

	}


}
