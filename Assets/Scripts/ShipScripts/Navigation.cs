using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation : Interactable
{
    public GameObject NavigationPanel;

    public override void Interact()
    {
        base.Interact();
        isInteracting = !isInteracting;
        panelOpen = !panelOpen;
        //NavigationPanel.GetComponent<Animator>().SetBool("isOpen", isInteracting);
        NavigationPanel.GetComponent<NavigationScript>().ToggleNavPanel(isInteracting);
        Debug.Log("where ya travelin?");
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
            Interact();
        else if (!canInteract && panelOpen)
        {
            panelOpen = false;
            //NavigationPanel.GetComponent<Animator>().SetBool("isOpen", false);
            NavigationPanel.GetComponent<NavigationScript>().ToggleNavPanel(false);
        }
    }
}
