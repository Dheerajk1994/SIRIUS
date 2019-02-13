using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humanoid : NonStaticClassScript
{

    string Name;
    Inventory inventory;
    // Use Transform? 


    protected  virtual void Talk()
    {
        Debug.Log("Loading");
    }

    // Use this for initialization
    protected override void Start () 
    {
    

	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}
}
