using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHotbarPanelScript : GenericInvoPanelScript {

    const ushort NOTHING_EQUIPPED = 100;

    public Hotbar playerHotbarReference;

    public ushort equippedSlot = 1;

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
        equippedSlot = 1;
    }

    private void Update()
    {
        HandleInput();
    }

    public void EquipSlot(ushort slotIndex)
    {
        Debug.Log("equip slot called with index " + slotIndex);
        //variable that tells you which slot is active 
        equippedSlot = slotIndex;
    }

    //return ID of item in slot
    public ushort GetEquippedSlot()
    {
        Debug.Log("Equipped Slot variable: " + equippedSlot);
        if (slots[equippedSlot].GetComponent<InventorySlot>().isHoldingAnItem)
        {
            return slots[equippedSlot].transform.GetChild(0).GetComponent<InventoryItem>().completeItem.itemDescription.id;
        }
        else
        {
            return 0;
        }
    }

    private void  HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            EquipSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            EquipSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            EquipSlot(2);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            EquipSlot(3);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            EquipSlot(4);
        if (Input.GetKeyDown(KeyCode.Alpha6))
            EquipSlot(5);
        if (Input.GetKeyDown(KeyCode.Alpha7))
            EquipSlot(6);
        if (Input.GetKeyDown(KeyCode.Alpha8))
            EquipSlot(7);
        if (Input.GetKeyDown(KeyCode.Alpha9))
            EquipSlot(8);
        if (Input.GetKeyDown(KeyCode.Alpha0))
            EquipSlot(9);
    }
}

