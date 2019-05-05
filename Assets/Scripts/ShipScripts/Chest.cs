using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    public GameObject ChestPanel;
    private Inventory chestInventory;
    private GenericInvoHandlerScript chestInvoHandler;

    public void SetChest(UIScript uiScript)
    {
        ChestPanel = uiScript.ChestPanel;
        chestInventory = this.GetComponent<Inventory>();
        chestInvoHandler = this.GetComponent<GenericInvoHandlerScript>();
    }

    public override void Interact()
    {
        isInteracting = !isInteracting;
        panelOpen = !panelOpen;
        ChestPanel.GetComponent<ChestPanelScript>().ToggleChestPanel(isInteracting, chestInventory, chestInvoHandler);
        Debug.Log("opening chest");
        base.Interact();
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
