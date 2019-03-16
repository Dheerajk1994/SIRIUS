using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//PARENT CLASS FOR SCRIPTS ATTACHED TO PLAYERS/CHESTS INVENTORY
public class ItemHolder : MonoBehaviour
{
    const ushort ROW_ID = 0;
    const ushort ROW_AMOUNT = 1; 
    [SerializeField]
    private ushort inventorySize;   //size of the inventory 

    [SerializeField]
    private ushort [,] inventoryArray;    //items that are in the inventory row 0 = item id, row 1 = amount of that item
                                            //[0,0] id [1,0] amount

    void Start(){
        inventoryArray = new ushort[2, inventorySize];
    }
     
    //Items picked up add to any slot that already contains that item or the first empty slot
    public ushort AddItem(ItemDescription itemDescription, ushort amount, InventoryHandlerScript inventoryHandler, ushort slotIndex){
        //base case if amount is 0
        if (amount == 0) return amount;    
        //go through the inventory and see if there are any empty slots
        if (slotIndex >= inventorySize)      
        {
            for(ushort i = 0; i < inventorySize; ++i)
            {
                if(inventoryArray[ROW_ID, i] == 0)
                {
                    inventoryArray[ROW_ID, i] = itemDescription.id;
                    while (inventoryArray[ROW_AMOUNT, i] < itemDescription.stackAmnt && amount > 0)
                    {
                        inventoryArray[ROW_AMOUNT, i] += 1;
                        amount--;
                    }
                    if(amount == 0)return amount;
                }
            }
            return amount;
        }
        //if there is already similar item but there is space
        if (inventoryArray[ROW_ID, slotIndex] == itemDescription.id && inventoryArray[ROW_AMOUNT, slotIndex] < itemDescription.stackAmnt)
        {
            while (inventoryArray[ROW_AMOUNT, slotIndex] < itemDescription.stackAmnt && amount > 0)
            {
                Debug.Log("Stack amount " + inventoryArray[ROW_AMOUNT, slotIndex]);
                inventoryArray[ROW_AMOUNT, slotIndex] += 1;
                amount--;
            }
            return AddItem(itemDescription, amount, inventoryHandler, (ushort)(slotIndex + 1));
        }
        //recursively call the next slot
        return AddItem(itemDescription, amount, inventoryHandler, (ushort)(slotIndex+1));
    }

    //Add item to a specific itemarray index
    public ushort AddItemToIndex(ItemDescription droppedItem, InventoryItem inventoryItem, ushort amount, ushort parentIndex, ushort droppedIndex)
    {
        ushort itemIDinSlot = inventoryArray[ROW_ID, droppedIndex];
        //Debug.Log("add item to index called");
        ushort stackDifference = (ushort)(droppedItem.stackAmnt - inventoryArray[ROW_AMOUNT, droppedIndex]);
        //Inventory Slot is not an empty item
        if (itemIDinSlot != 0)
        {
            //Dropped item is the same item in the inventory slot
            if (droppedItem.id == itemIDinSlot)
            {
                //If item slot stack < item slot's item's max stack
                if (inventoryArray[ROW_AMOUNT, droppedIndex] < droppedItem.stackAmnt)
                {
                    //If dropped item amount <= max stack of item - current stack in slot(stack difference)
                    if (amount <= stackDifference)
                    {
                        //Add dropped amount to current stack
                        inventoryArray[ROW_AMOUNT, droppedIndex] += amount;

                        //Empty original slot
                        inventoryArray[ROW_ID, parentIndex] = 0;
                        inventoryArray[ROW_AMOUNT, parentIndex] = 0;
                        return 0;
                    }
                    else
                    {
                        //Add dropped item stack difference to inventory slot stack
                        inventoryArray[ROW_AMOUNT, droppedIndex] += stackDifference;
                        //Add remaining item to origin slot
                        inventoryArray[ROW_AMOUNT, parentIndex] = (ushort)(amount - stackDifference);
                        return (ushort)(amount - stackDifference);

                    }
                }
                return 0;
            }
            return 0;
        }
        else
        {
            // Add item to empty slot
            inventoryArray[ROW_ID, parentIndex] = 0;
            inventoryArray[ROW_AMOUNT, parentIndex] = 0;

            inventoryArray[ROW_ID, droppedIndex] = droppedItem.id;
            inventoryArray[ROW_AMOUNT, droppedIndex] = amount;
            inventoryItem.UpdateInventoryItem(amount);
            return 0;
        }
    }

    /*
     * protected void HandleItemDrop(PointerEventData eventData)
public void AddItemToSlot(ItemDescription item, ushort amount, InventorySlot newslot, InventorySlot parentSlot)
     */

    public ushort RemoveItem(ushort itemID, ushort amount)
    {
        return amount;
    }


    //swap two slots
    public bool SwapInventoryItems(ushort slot1Index, ushort slot2Index)
    {
        if(slot1Index < inventorySize && slot2Index < inventorySize)
        {
            ushort tempID = inventoryArray[ROW_ID, slot1Index];
            ushort tempAmount = inventoryArray[ROW_AMOUNT, slot1Index];

            inventoryArray[ROW_ID,     slot1Index] = inventoryArray[ROW_ID,     slot2Index];
            inventoryArray[ROW_AMOUNT, slot1Index] = inventoryArray[ROW_AMOUNT, slot2Index];

            inventoryArray[ROW_ID,     slot2Index] = tempID;
            inventoryArray[ROW_AMOUNT, slot2Index] = tempAmount;

            return true;
        }
        else
        {
            return false;
        }
    }

    public ushort FetchItemIdInInventorySlot(ushort slot)
    {
        if(slot < inventorySize)
        {
            return inventoryArray[0, slot];
        }
        return 0;
    }

    public ushort FetchItemAmountInInventorySlot(ushort slot)
    {
        if (slot < inventorySize)
        {
            return inventoryArray[1, slot];
        }
        return 0;
    }

    public ushort GetInventorySize()
    {
        return inventorySize;
    }

    public void TestFunctionAddToIndex(ItemDescription item, ushort amount, ushort index)
    {
        inventoryArray[ROW_ID, index] = item.id;
        inventoryArray[ROW_AMOUNT, index] = amount;
    }

}
/*
 if (amount == 0 || slotIndex >= inventorySize)
        {
            return amount;
        }
        for (int column = slotIndex; column < inventorySize; column++)
        {
            if(itemsInInventory[ROW_ID, column] == (ushort)EnumClass.TileEnum.EMPTY)
            {
                itemsInInventory[ROW_ID, column] = itemDescription.id;
                itemsInInventory[ROW_AMOUNT, column] = (ushort)Mathf.Clamp(amount, 0, itemDescription.stackAmnt);
                amount -= itemsInInventory[ROW_AMOUNT, column];
                return (AddItem(itemDescription, amount, inventoryHandler, (ushort)(slotIndex++)));
            }
            if (itemsInInventory[ROW_ID, column] == itemDescription.id)
            {
                //itemsInInventory[ROW_AMOUNT,column] =
                //int rAmount = amount - (itemDescription.stackAmnt - itemsInInventory[1, column]);
                //amount = (ushort)(Mathf.Clamp(rAmount, 0, amount));
                //return (AddItem(itemDescription, amount, inventoryHandler, (ushort)(slotIndex ++)));
                itemsInInventory[ROW_AMOUNT, column] += amount;
                return 0;
            }
            else{
                return (AddItem(itemDescription, amount, inventoryHandler, (ushort)(slotIndex++)));
            }
        }
        return amount;
     */


////check if the item in the index is empty
//if(inventoryArray[ROW_ID, slotIndex] == 0)      
//{
//    inventoryArray[ROW_ID, slotIndex] = item.id;
//    while (inventoryArray[ROW_AMOUNT, slotIndex] < item.stackAmnt && amount > 0)
//    {
//        inventoryArray[ROW_AMOUNT, slotIndex] += 1;
//        amount--;
//    }
//    return amount;
//}
////Check if the item in the index is the same as item being passed
//if(item.id == inventoryArray[ROW_ID, slotIndex])
//{
//    // increased stored amount
//    while(inventoryArray[ROW_AMOUNT, slotIndex] < item.stackAmnt && amount > 0)
//    {
//        inventoryArray[ROW_AMOUNT, slotIndex] += 1;
//        amount--;
//    }
//    return amount;
//}
//return amount;