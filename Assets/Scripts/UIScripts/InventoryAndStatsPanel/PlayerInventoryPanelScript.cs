using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryPanelScript : GenericInvoPanelScript {

    public Inventory playerInventoryReference;


	// Use this for initialization
	void Start () {
        slots = new GameObject[40]; //NEED OPTIMIZATION
        for (ushort i = 0; i < 40; i++)
        {
            GameObject slot = Instantiate(inventorySlotPrefab);
            slot.name = "InventorySlot (" + i + ")";
            slot.transform.SetParent(genericInvoPanel.transform, false);
            slot.GetComponent<InventorySlot>().slotID = i;
            slots[i] = slot;
            //slots in the PLAYER INVENTORY panel
            slot.GetComponent<InventorySlot>().inventoryReference = playerInventoryReference;
            slot.GetComponent<InventorySlot>().genericInvoHandler = this.genericInvoHandler;
        }
        genericInvoHandler.slots = slots;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            GetComponent<InventoryAndStatsPanelScript>().ToggleInventoryAndStatsPanel();
        }
    }

}
