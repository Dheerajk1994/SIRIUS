using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestPanelScript : GenericInvoPanelScript {

    public Inventory chestInventoryReference;

    private UIScript uIScript;
    private Animator chestPanelAnimator;
    public Button exitButn;
    private bool isOpen = false;

    public void SetChestPanel(UIScript ui)
    {
        uIScript = ui;
        chestPanelAnimator = GetComponent<Animator>();
    }

    private void PopulateInventorySlots(Inventory chestInvo, GenericInvoHandlerScript chestInvoHandler)
    {
        chestInventoryReference = chestInvo;
        genericInvoHandler = chestInvoHandler;
        ClearPanel(genericInvoPanel.transform);
        numberOfSlots = chestInventoryReference.GetInventorySize();
        slots = new GameObject[numberOfSlots]; //NEED OPTIMIZATION
        for (ushort i = 0; i < numberOfSlots; i++)
        {
            GameObject slot = Instantiate(inventorySlotPrefab);
            slot.name = "InventorySlot (" + i + ")";
            slot.transform.SetParent(genericInvoPanel.transform, false);
            slot.GetComponent<InventorySlot>().slotID = i;
            slots[i] = slot;
            //slots in the CHEST INVENTORY panel
            slot.GetComponent<InventorySlot>().inventoryReference = chestInventoryReference;
            slot.GetComponent<InventorySlot>().genericInvoHandler = this.genericInvoHandler;
        }
        genericInvoHandler.slots = slots;
        genericInvoHandler.UpdatePanelSlots();
    }

    private void ClearPanel(Transform panel)
    {
        if (panel.transform.childCount == 0) return;
        foreach (Transform child in panel.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void ToggleChestPanel(bool toggle, Inventory chestInvo, GenericInvoHandlerScript chestInvoHandler)
    {
        isOpen = toggle;
        if (isOpen)
        {
            PopulateInventorySlots(chestInvo, chestInvoHandler);
            uIScript.InventoryAndStatsPanel.GetComponent<InventoryAndStatsPanelScript>().ToggleInventoryAndStatsPanel(true);
        }
        chestPanelAnimator.SetBool("isOpen", toggle);
    }

    public void ToggleChestPanel()
    {
        isOpen = !isOpen;
        chestPanelAnimator.SetBool("isOpen", isOpen);
    }

}
