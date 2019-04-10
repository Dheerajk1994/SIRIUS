using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    public override void Interact()
    {
        base.Interact();
        isInteracting = !isInteracting;
        Debug.Log("opening chest");
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
            Interact();
    }
}
