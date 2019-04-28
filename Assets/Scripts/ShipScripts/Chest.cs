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
        panelOpen = !panelOpen;
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
        else if (!canInteract && panelOpen)
        {
            panelOpen = false;
            ChestPanel.GetComponent<Animator>().SetBool("isOpen", false);
        }
    }
}
