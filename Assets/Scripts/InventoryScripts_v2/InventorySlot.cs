using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//ATTACH THIS TO INVENTORY BUTTONS
public class InventorySlot : MonoBehaviour, IDropHandler
{
    private GameObject itemInSlot;
    private InventoryHandlerScript inventoryHandler;

    public ushort slotID;
     
    private void Start()
    {
        itemInSlot = null;
        inventoryHandler = this.transform.parent.transform.parent.GetComponent<InventoryHandlerScript>();
    }

    //DROP HANDLER IMPLEMENTATION -
    //WHAT HAPPENS WHEN AN ITEMS IS DROPPED ONTO THE SLOT
    public void OnDrop(PointerEventData eventData)
    {
        //steps:
        //call a function in inventory handler pass it the parent slot(item.parentslot), item , new slot (this)   

        if (itemInSlot == null) //THERE IS NO ITEM IN THE SLOT
        {
            itemInSlot = InventoryItem.itemBeingDragged;
            InventoryItem.itemBeingDragged.GetComponent<InventoryItem>().parentSlot.GetComponent<InventorySlot>().itemInSlot = null;
            InventoryItem.itemBeingDragged.transform.SetParent(this.transform, false);
        }
        else//IF THERE IS AN ITEM IN THE SLOT - SWAP
        {
            //inventoryha
        }
    }
}


