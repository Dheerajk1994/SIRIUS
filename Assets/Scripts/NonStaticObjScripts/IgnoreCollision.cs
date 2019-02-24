using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour {

    [SerializeField]
    private Collider2D other;

	// Use this for initialization
	private void Awake () 
    {
        // ignore collision between my own collider with other colliders
        // switch to false if you don't want this implemented
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other, true);
	}
	

}
