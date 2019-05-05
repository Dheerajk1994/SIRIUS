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

    void Awake(){
        //Debug.Log("item holder awake called " + inventorySize);
        inventoryArray = new ushort[2, inventorySize];
    }

    public ushort GetItemAmount(ushort id, ushort amount) 
    {
        ushort sum = 0;
        // Iterate through the array of inventory items
        for (ushort i = 0; i < inventorySize; ++i)
        {// If the item is in the slot
            if (inventoryArray[ROW_ID, i] == id)
            {
                sum += inventoryArray[ROW_AMOUNT, i];
                if (sum >= amount) return sum;
            }
        }
        return sum;
    } 

    public ushort RemoveItemFromInventory(ushort id, ushort amount) //you need 5 stone and there is 10 stone in the slot
    {
        for (ushort i = 0; i < inventorySize; ++i)
        {
            if (inventoryArray[ROW_ID, i] == id)
            {
                if (amount >= inventoryArray[ROW_AMOUNT, i])
                {
                    amount -= inventoryArray[ROW_AMOUNT, i];
                    inventoryArray[ROW_AMOUNT, i] = 0;
                    inventoryArray[ROW_ID, i] = 0;
                }
                else
                {
                    ushort StackDifference = (ushort)(inventoryArray[ROW_AMOUNT, i] - amount);
                    inventoryArray[ROW_AMOUNT, i] = StackDifference;
                    amount = 0;
                    return amount;
                }
            }
        }
        return amount;
    }

    //Items picked up add to any slot that already contains that item or the first empty slot
    public ushort AddItem(ItemDescription itemDescription, ushort amount, ushort slotIndex){
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
                //Debug.Log("Stack amount " + inventoryArray[ROW_AMOUNT, slotIndex]);
                inventoryArray[ROW_AMOUNT, slotIndex] += 1;
                amount--;
            }
            return AddItem(itemDescription, amount, (ushort)(slotIndex + 1));
        }
        //recursively call the next slot
        return AddItem(itemDescription, amount, (ushort)(slotIndex+1));
    }

    //Add item to a specific itemarray index
    public ushort AddItemToIndex(ItemDescription droppedItem, InventoryItem inventoryItem, ushort amount, ushort droppedIndex)
    {
        ushort itemIDinSlot = inventoryArray[ROW_ID, droppedIndex];
        ushort stackDifference = (ushort)(droppedItem.stackAmnt - inventoryArray[ROW_AMOUNT, droppedIndex]);

        if (amount != 0)
        {
            //If item slot stack < item slot's item's max stack
            if (inventoryArray[ROW_AMOUNT, droppedIndex] < droppedItem.stackAmnt)
            {
                inventoryArray[ROW_ID, droppedIndex] = droppedItem.id;
                //If dropped item amount <= max stack of item - current stack in slot(stack difference)
                if (amount <= stackDifference)
                {
                    //Add dropped amount to current stack
                    inventoryArray[ROW_AMOUNT, droppedIndex] += amount;
                    //Empty original slot

                    //Empty original slot
                    return 0;
                }
                else
                {
                    //Add dropped item stack difference to inventory slot stack
                    inventoryArray[ROW_AMOUNT, droppedIndex] += stackDifference;

                    //Add remaining item to origin slot
                    return (ushort)(amount - stackDifference);

                }
            }
            return amount;
        }
        else
        {
            inventoryArray[ROW_AMOUNT, droppedIndex] = 0;
            inventoryArray[ROW_ID, droppedIndex] = 0;
            return 0;
        }
    }

    //add item to a specific index hardcode
    public void SetItemAmountAtIndex(ushort amount, ushort index)
    {
        //Debug.Log("setitemamountatindex called with amount " + amount + " at index " + index);
        inventoryArray[ROW_AMOUNT, index] = amount;
        if(amount == 0)
        {
            inventoryArray[ROW_ID, index] = 0;
        }
    }

    public void SetItemAtIndexNoQuestionAsked(ushort id, ushort amount, ushort index)
    {
        inventoryArray[ROW_ID, index] = id;
        inventoryArray[ROW_AMOUNT, index] = amount;
    }

    public ushort RemoveItem(ushort itemID, ushort amount)
    {
        return amount;
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

    public void EmptyInventory()
    {
        for (ushort i = 0; i < inventorySize; ++i)
        {
            inventoryArray[ROW_ID, i] = 0;
            inventoryArray[ROW_AMOUNT, i] = 0;
        }
    }

    public void TestFunctionAddToIndex(ItemDescription item, ushort amount, ushort index)
    {
        inventoryArray[ROW_ID, index] = item.id;
        inventoryArray[ROW_AMOUNT, index] = amount;
    }

    public ushort [,] FetchItemsInInventory()
    {
        //Debug.Log("generic holder " + inventoryArray[ROW_ID,0]);
        ushort[,] items = new ushort[inventoryArray.GetLength(0), inventoryArray.GetLength(1)];
        items = inventoryArray;
        return items;
    }

    public void PopulateInventory(ushort [,] inventoryItems)
    {
        inventoryArray = new ushort[inventoryItems.GetLength(0), inventoryItems.GetLength(1)];
        inventoryArray = inventoryItems;
    }

}
