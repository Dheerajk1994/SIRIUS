using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WTAF : Interactable
{

    public override void Interact()
    {
        base.Interact();
        isInteracting = !isInteracting;
        Debug.Log("Beep boop quest shit...");
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
