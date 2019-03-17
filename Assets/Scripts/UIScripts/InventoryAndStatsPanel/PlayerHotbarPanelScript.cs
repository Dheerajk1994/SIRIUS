using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHotbarPanelScript : GenericInvoPanelScript {

    public Hotbar playerHotbarReference;


    // Use this for initialization
    void Start()
    {
        slots = new GameObject[10]; //NEED OPTIMIZATION
        for (ushort i = 0; i < slots.Length; i++)
        {
            GameObject slot = Instantiate(inventorySlotPrefab);
            slot.name = "hotbarSlot_" + i;
            slot.transform.SetParent(genericInvoPanel.transform, false);
            slot.GetComponent<InventorySlot>().slotID = i;
            slots[i] = slot;

            slot.GetComponent<InventorySlot>().genericInvoHandler = this.genericInvoHandler;
        }
        genericInvoHandler.slots = slots;
    }

    private void Update()
    {

    }
}
