using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHotbarPanelScript : GenericInvoPanelScript {

    const ushort NOTHING_EQUIPPED = 100;

    public Hotbar playerHotbarReference;

    public ushort equippedSlot = 0;
    public  Sprite selectedSlotSprite;
    private Sprite defaultSprite;

    Player playerScript;


    public void SetPlayerHotbarPanel(Hotbar phr, Player player)
    {
        playerScript = player;
        playerHotbarReference = phr;

        numberOfSlots = playerHotbarReference.GetInventorySize();
        slots = new GameObject[numberOfSlots]; //NEED OPTIMIZATION
        for (ushort i = 0; i < numberOfSlots; i++)
        {
            GameObject slot = Instantiate(inventorySlotPrefab);
            slot.name = "hotbarSlot_" + i;
            slot.transform.SetParent(genericInvoPanel.transform, false);
            slot.GetComponent<InventorySlot>().slotID = i;
            slots[i] = slot;

            slot.GetComponent<InventorySlot>().genericInvoHandler = this.genericInvoHandler;
        }
        genericInvoHandler.slots = slots;
        equippedSlot = 0;
        defaultSprite = slots[equippedSlot].GetComponent<Image>().sprite;
        slots[equippedSlot].GetComponent<Image>().sprite = selectedSlotSprite;
    }

    void Start()
    {
        
    }

    private void Update()
    {
        //HandleInput();
    }

    public void EquipSlot(ushort slotIndex)
    {
        slots[equippedSlot].GetComponent<Image>().sprite = defaultSprite;//set the old slot back to default sprite
        equippedSlot = slotIndex;
        slots[equippedSlot].GetComponent<Image>().sprite = selectedSlotSprite;//view selected sprite on new slot
        playerScript.HandleEquip();
    }

    //return ID of item in slot
    public ushort GetEquippedSlot()
    {
        //Debug.Log("Equipped Slot variable: " + equippedSlot);
        if (slots[equippedSlot].GetComponent<InventorySlot>().isHoldingAnItem)
        {

            if (slots[equippedSlot].transform.childCount > 0)
            {
                return slots[equippedSlot].transform.GetChild(0).GetComponent<InventoryItem>().completeItem.itemDescription.id;
            }

            return 0;
        }
        else
        {
            return 0;
        }
    }

   
}

/*
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
     */
