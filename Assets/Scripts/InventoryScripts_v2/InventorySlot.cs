using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
 
//ATTACH THIS TO INVENTORY BUTTONS
public class InventorySlot : MonoBehaviour, IDropHandler
{
    public GameObject itemInSlot;
    public InventoryHandlerScript inventoryHandler;

    public ItemHolder inventoryReference;

    public ushort slotID;
    public bool isHoldingAnItem;
     
    private void Start()
    {
        itemInSlot = null;
    }

    //DROP HANDLER IMPLEMENTATION -
    //WHAT HAPPENS WHEN AN ITEMS IS DROPPED ONTO THE SLOT
    public void OnDrop(PointerEventData eventData)
    {
        //call a function in inventory handler pass it the parent slot(item.parentslot), item , new slot (this)   
        inventoryHandler.HandleItemDrop(InventoryItem.itemBeingDragged.GetComponent<InventoryItem>().parentSlot.GetComponent<InventorySlot>(), this, InventoryItem.itemBeingDragged.GetComponent<InventoryItem>());

    }
}


