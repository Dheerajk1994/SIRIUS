using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation : Interactable
{
    public override void Interact()
    {
        base.Interact();
        isInteracting = !isInteracting;
        Debug.Log("where ya travelin?");
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
