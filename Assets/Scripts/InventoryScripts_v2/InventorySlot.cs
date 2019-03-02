using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//ATTACH THIS TO INVENTORY BUTTONS
public class InventorySlot : MonoBehaviour, IDropHandler
{
    private GameObject itemInSlot;

    private void Start()
    {
        itemInSlot = null;
    }

    //DROP HANDLER IMPLEMENTATION -
    //WHAT HAPPENS WHEN AN ITEMS IS DROPPED ONTO THE SLOT
    public void OnDrop(PointerEventData eventData)
    {
        if (!itemInSlot) //THERE IS NO ITEM IN THE SLOT
        {
            itemInSlot = InventoryItem.itemBeingDragged;
            InventoryItem.itemBeingDragged.transform.parent.GetComponent<InventorySlot>().itemInSlot = null;
            InventoryItem.itemBeingDragged.transform.SetParent(this.transform);
        }
        else//IF THERE IS AN ITEM IN THE SLOT - SWAP
        {
            //NEEDS WORK
        }
    }
}


