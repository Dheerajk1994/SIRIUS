using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericInvoHandlerScript : MonoBehaviour {

    //REFERENCES
    public GameObject[] slots; //slots in inventory, slots in hotbar, slots in chest
    [SerializeField]
    public ItemHolder genericInventory; //players inventory, hotbar invo, chest invo
    public GameObject genericInventoryPanel; // player inventory panel, hotbar panel, chest panel

    public GameObject inventoryItemPrefab;

    private void Start()
    {
        ItemDictionary.GenerateDictionary();
        //slots = genericInventoryPanel.GetComponent<GenericInvoPanelScript>().slots;
        //UpdatePanelSlots();
    }

    //FUNCTIONS
    //adding items
    public ushort AddItemToGenericInventory(ushort id, ushort amount)
    {
        CompleteItem item = ItemDictionary.GetItem(id);
        amount = genericInventory.AddItem(item.itemDescription, amount, 0);
        UpdatePanelSlots();
        return amount;
    }

    //removing items
    public ushort CheckItemAmountInGenericInventory(ushort id, ushort amount)
    {
        return genericInventory.GetItemAmount(id, amount);
    }

    public ushort RemoveItemsFromGenericInventory(ushort id, ushort amount)
    {
        ushort result =  genericInventory.RemoveItemFromInventory(id, amount);
        UpdatePanelSlots();
        return result;
    }

    //handle item drop
    //when called by slot - the slot will call this function in the handler that its associated to
    public void HandleItemDrop(InventorySlot parentSlot, InventorySlot newSlot, InventoryItem itemBeingDragged)
    {
        //get the id of the item in the slot - teritiary operator if there slot is empty then 0 otherwise the id of the child
        ushort idAtNewSlot = 
            (newSlot.isHoldingAnItem) ? 
            (newSlot.transform.GetChild(0).GetComponent<InventoryItem>().completeItem.itemDescription.id)
            : 
            (ushort)0; 

        //if there is no item in the new slot or if the slot has the same item
        if (idAtNewSlot == 0 || idAtNewSlot == itemBeingDragged.completeItem.itemDescription.id)
        {
            //add the item to the new slot index and get the remaining amount
            ushort remainingAmount = genericInventory.AddItemToIndex(
                itemBeingDragged.completeItem.itemDescription, 
                itemBeingDragged, 
                itemBeingDragged.stackCount, 
                newSlot.slotID);
            Debug.Log("remaining amount: " + remainingAmount);
            //call the original slots handler with remaining amount
            parentSlot.GetComponent<InventorySlot>().genericInvoHandler.genericInventory.SetItemAmountAtIndex(remainingAmount, parentSlot.slotID);
            //call both handlers update functions
            //Debug.Log("stack at new slot: " + genericInventory.FetchItemAmountInInventorySlot(newSlot.slotID));
            //Debug.Log("stack at parent slot: " + parentSlot.GetComponent<InventorySlot>().genericInvoHandler.genericInventory.FetchItemAmountInInventorySlot(parentSlot.slotID));

            UpdatePanelSlots();
            parentSlot.GetComponent<InventorySlot>().genericInvoHandler.UpdatePanelSlots();

        }
        else//swap
        {
            //Debug.Log("swap called");
            //call the parent slot with the item in the new slot
            InventoryItem itemInNewSlot = newSlot.transform.GetChild(0).GetComponent<InventoryItem>();
            parentSlot.GetComponent<InventorySlot>().genericInvoHandler.genericInventory.AddItemToIndex(
                itemInNewSlot.completeItem.itemDescription,
                itemInNewSlot.GetComponent<InventoryItem>(),
                itemInNewSlot.GetComponent<InventoryItem>().stackCount,
                parentSlot.slotID);
            //call the new slot with itembeing dragged
            newSlot.GetComponent<InventorySlot>().genericInvoHandler.genericInventory.AddItemToIndex(
               itemBeingDragged.completeItem.itemDescription,
               itemBeingDragged,
               itemBeingDragged.stackCount,
               newSlot.slotID);

            UpdatePanelSlots();
            parentSlot.GetComponent<InventorySlot>().genericInvoHandler.UpdatePanelSlots();
            //Debug.Log("item in new slot stack: " + itemInNewSlot.GetComponent<InventoryItem>().stackCount + "item being dragged stack: " + itemBeingDragged.stackCount);
        } 
    }

    //for updating the associated slots
    public void UpdatePanelSlots()
    {
        //go through each inventory index
        for (ushort i = 0; i < genericInventory.GetInventorySize(); ++i)
        {
            ushort id = genericInventory.FetchItemIdInInventorySlot(i);
            if (id != 0)
            {
                foreach (Transform child in slots[i].transform) { Destroy(child.gameObject); }

                ushort amnt = genericInventory.FetchItemAmountInInventorySlot(i);
                CompleteItem item = ItemDictionary.GetItem(id);
                GameObject invItem = Instantiate(inventoryItemPrefab);
                invItem.GetComponent<InventoryItem>().UpdateInventoryItem(item, amnt);

                invItem.transform.SetParent(slots[i].transform, false);
                slots[i].GetComponent<InventorySlot>().isHoldingAnItem = true;
            }
            else
            {
                foreach (Transform child in slots[i].transform) { Destroy(child.gameObject); }
                slots[i].GetComponent<InventorySlot>().isHoldingAnItem = false;
            }
        }
    }

}
