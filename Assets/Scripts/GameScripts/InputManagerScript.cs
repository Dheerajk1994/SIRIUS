using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerScript : MonoBehaviour {

    //references needed
    //one to terrain manager
    TerrainManagerScript terrainManager;
    //one to hotbar panel script
    public PlayerInventoryPanelScript inventoryPanel;
    public PlayerHotbarPanelScript hotbarPanel;
    //character, etc.. later on
    //????

    private void Update()
    {
        // Player Action
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch(hotbarPanel.GetEquippedSlot())
            {

                // Block Tiles
                case 1:
                case 2:
                case 3:
                case 4:
                    //terrainManager.PlaceTile((int)Input.mousePosition.x, (int)Input.mousePosition.y, hotbarPanel.GetEquippedSlot(),(ushort)EnumClass.LayerIDEnum.FRONTLAYER);
                    // Remove Item from Inventory
                    break;
                case 1000:  // Pickaxe   
                    terrainManager.MineTile((int)Input.mousePosition.x, (int)Input.mousePosition.y, (ushort)EnumClass.LayerIDEnum.FRONTLAYER, this);
                    break;
            }
            //MineTile(int x, int y, InputManagerScript inputManager)
        }

        // Hotbar Equipping
        if (Input.GetKeyDown(KeyCode.Alpha1))
            hotbarPanel.EquipSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            hotbarPanel.EquipSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            hotbarPanel.EquipSlot(2);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            hotbarPanel.EquipSlot(3);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            hotbarPanel.EquipSlot(4);
        if (Input.GetKeyDown(KeyCode.Alpha6))
            hotbarPanel.EquipSlot(5);
        if (Input.GetKeyDown(KeyCode.Alpha7))
            hotbarPanel.EquipSlot(6);
        if (Input.GetKeyDown(KeyCode.Alpha8))
            hotbarPanel.EquipSlot(7);
        if (Input.GetKeyDown(KeyCode.Alpha9))
            hotbarPanel.EquipSlot(8);
        if (Input.GetKeyDown(KeyCode.Alpha0))
            hotbarPanel.EquipSlot(9);
        // End Hotbar Equipping
    }


    //a function that is called by terrain manager
    //so if the mined tile is stone and coal
    //i can call this fucntion two times
    //first with stone
    //second with coal

    public void BadFunctionCalledByTerrainManager(ushort id, ushort amount)
    {
        ushort remaining = hotbarPanel.GetComponent<GenericInvoPanelScript>().genericInvoHandler.AddItemToGenericInventory(id, amount);   //try adding to hotbar
        if(remaining > 0) inventoryPanel.GetComponent<GenericInvoPanelScript>().genericInvoHandler.AddItemToGenericInventory(id, amount); //add to inventory remaining
    }

    public void BadFunctionCalledByTerrainManager()
    {
        ushort remaining = hotbarPanel.GetComponent<GenericInvoPanelScript>().genericInvoHandler.AddItemToGenericInventory(1, 1);   //try adding to hotbar
        if (remaining > 0) inventoryPanel.GetComponent<GenericInvoPanelScript>().genericInvoHandler.AddItemToGenericInventory(1, remaining); //add to inventory remaining
    }

    public void BadFunctionCalledByButton()
    {
        //if(hotbarhasitem1 || inventoryhasitem1 && hotbarhasitem2 || inventoryhasitem2) remove item 
        ushort item1id = 1; //dirt
        ushort item2id = 2; //stone

        ushort item1Amount = 12; //12 dirt
        ushort item2Amount = 7;  //7 stone

        ushort amount1InHotbar = hotbarPanel.genericInvoHandler.CheckItemAmountInGenericInventory(item1id, item1Amount);
        ushort amount1InInventory = inventoryPanel.genericInvoHandler.CheckItemAmountInGenericInventory(item1id, item1Amount);

        ushort amount2InHotbar = hotbarPanel.genericInvoHandler.CheckItemAmountInGenericInventory(item2id, item2Amount);
        ushort amount2InInventory = hotbarPanel.genericInvoHandler.CheckItemAmountInGenericInventory(item2id, item2Amount);

        if ( amount1InHotbar + amount1InInventory >= item1Amount && amount2InHotbar + amount2InInventory >= item2Amount )
        {
            inventoryPanel.genericInvoHandler.RemoveItemsFromGenericInventory
                (item1id,
                 hotbarPanel.genericInvoHandler.RemoveItemsFromGenericInventory(item1id, item1Amount));

            inventoryPanel.genericInvoHandler.RemoveItemsFromGenericInventory
                (item2id,
                 hotbarPanel.genericInvoHandler.RemoveItemsFromGenericInventory(item2id, item2Amount));
        }


    }
}
