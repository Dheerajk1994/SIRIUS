using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Humanoid 
{


    protected override void Talk()
    {
        Debug.Log("Loading Text for NPC");
    }

    protected override void Start()
    {
     
        Inventory NPCinv = new Inventory();
    }
}
