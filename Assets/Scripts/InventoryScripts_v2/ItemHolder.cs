using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//PARENT CLASS FOR SCRIPTS ATTACHED TO PLAYERS/CHESTS INVENTORY
public class ItemHolder : ScriptableObject
{
    [SerializeField]
    private ushort inventorySize;   //size of the inventory

    [SerializeField]
    private ushort [,] itemsInInventory;    //items that are in the inventory row 0 = item id, row 1 = amount of that item
                                            //[0,0] id [1,0] amount

    void Start(){
        itemsInInventory = new ushort[2, inventorySize];
    }
     
    //Items picked up adding to first slot
    public ushort AddItem(ItemDescription item, ushort amount, InventoryHandlerScript inventoryHandler, ushort slotIndex){
        if (amount == 0 || slotIndex >= inventorySize)
        {
            return amount;
        }
        for (int column = slotIndex; column < inventorySize; ++column)
        {
            if (itemsInInventory[0, column] == item.id)
            {
                int rAmount = amount - (item.stackAmnt - itemsInInventory[1, column]);
                amount = (ushort)(Mathf.Clamp(rAmount, 0, amount));
                return (AddItem(item, amount, inventoryHandler, (ushort)(slotIndex ++)));
            }
            else{
                return (AddItem(item, amount, inventoryHandler, (ushort)(slotIndex++)));
            }
        }
        return amount;
    }


    //max stack is 1000
    //990 current    5 given
    // abs(20 - (1000 - 990))
    //


    public void AddItemToSlot(ItemDescription item, ushort amount, InventorySlot newslot, InventorySlot parentSlot)
    {
        if(item.id == newslot.id)
        {
            // Amount being stored is less than max amount
            if(amount < item.stackAmnt)
            {
                ushort stackDifference = (ushort)(item.stackAmnt - amount);
                if(amount <= stackDifference)
                {
                    newslot.currentstack += amount;     // Update stack count
                    //parentslot to null
                }
                else
                {
                    stackDifference -= amount;
                    newslot.currentstack += stackDifference;    // Add what can be added to newSlot
                    AddItemToSlot(item, stackDifference, parentSlot, parentSlot);    //Return rest to parentSlot
                }
            }
            else
            {
                AddItemToSlot(item, amount, parentSlot, parentSlot);
            }
        }

    }

    
}
