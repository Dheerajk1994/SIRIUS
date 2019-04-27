using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    public GameObject ChestPanel;

    public override void Interact()
    {
        base.Interact();
        isInteracting = !isInteracting;
        ChestPanel.GetComponent<Animator>().SetBool("isOpen", isInteracting);
        Debug.Log("opening chest");
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
            Interact();
        else if (!canInteract)
            ChestPanel.GetComponent<Animator>().SetBool("isOpen", false);
    }
}
