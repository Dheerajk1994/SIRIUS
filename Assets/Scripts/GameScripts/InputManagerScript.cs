using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class InputManagerScript : MonoBehaviour {

    GameManagerScript gameManagerScript;
    UIScript uiScript;
    TerrainManagerScript terrainManagerScript;
    CraftingPanelScript craftingPanelScript;


    public PlayerInventoryPanelScript inventoryPanel;
    public PlayerHotbarPanelScript hotbarPanel;

    public Player playerScript;

    public void SetInputManager(GameManagerScript gScript, UIScript uScript, TerrainManagerScript tScript)
    {
        gameManagerScript = gScript;
        uiScript = uScript;
        terrainManagerScript = tScript;
        inventoryPanel = uiScript.PlayerInventoryAndStatsPanel.GetComponent<PlayerInventoryPanelScript>();
        hotbarPanel = uiScript.PlayerHotBarPanel.GetComponent<PlayerHotbarPanelScript>();
        playerScript = gameManagerScript.playerScript;
        craftingPanelScript = uiScript.PlayerCraftingPanel.GetComponent<CraftingPanelScript>();
    }
    private void Start()
    {

    }
    private void Update()
    {
        // Player Action
        if (Input.GetKey(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject())
        {
            switch (hotbarPanel.GetEquippedSlot())
            {
                // Block Tiles
                case 1:
                    PlaceTileFrontLayer(1);
                    break;
                case 2:
                    PlaceTileFrontLayer(2);
                    break;
                case 3:
                    PlaceTileFrontLayer(3);
                    break;
                case 4:
                    PlaceTileFrontLayer(4);
                    break;
                case 1000:  // Pickaxe   
                    MineFrontLayer();
                    break;
            }
        }

        if(Input.GetKeyDown(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject())
        {
            switch (hotbarPanel.GetEquippedSlot())
            {
                case 800:
                case 801:
                    playerScript.MeleeAttack();
                    break;
            }
        }

        else if (Input.GetKey(KeyCode.Mouse1) && !EventSystem.current.IsPointerOverGameObject())
        {
            switch (hotbarPanel.GetEquippedSlot())
            {

                // Block Tiles
                case 1:
                    PlaceTileBackLayer(1);
                    break;
                case 2:
                    PlaceTileBackLayer(2);
                    break;
                case 3:
                    PlaceTileBackLayer(3);
                    break;
                case 4:
                    PlaceTileBackLayer(4);
                    break;
                case 1000:  // Pickaxe   
                    MineBackLayer();
                    break;
            }
        }
        // Hotbar Equipping
        else if (Input.GetKeyDown(KeyCode.Alpha1))
            hotbarPanel.EquipSlot(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            hotbarPanel.EquipSlot(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            hotbarPanel.EquipSlot(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            hotbarPanel.EquipSlot(3);
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            hotbarPanel.EquipSlot(4);
        else if (Input.GetKeyDown(KeyCode.Alpha6))
            hotbarPanel.EquipSlot(5);
        else if (Input.GetKeyDown(KeyCode.Alpha7))
            hotbarPanel.EquipSlot(6);
        else if (Input.GetKeyDown(KeyCode.Alpha8))
            hotbarPanel.EquipSlot(7);
        else if (Input.GetKeyDown(KeyCode.Alpha9))
            hotbarPanel.EquipSlot(8);
        else if (Input.GetKeyDown(KeyCode.Alpha0))
            hotbarPanel.EquipSlot(9);
        // End Hotbar Equipping
        //test for adding a pick to hotbar
        else if (Input.GetKeyDown(KeyCode.P))
            hotbarPanel.GetComponent<GenericInvoPanelScript>().genericInvoHandler.AddItemToGenericInventory(1000, 1);
        else if (Input.GetKeyDown(KeyCode.M))
        {
            hotbarPanel.GetComponent<GenericInvoPanelScript>().genericInvoHandler.AddItemToGenericInventory(800, 1);
            hotbarPanel.GetComponent<GenericInvoPanelScript>().genericInvoHandler.AddItemToGenericInventory(801, 1);
        }
        //test for adding spacegun to hotbar
        else if (Input.GetKeyDown(KeyCode.G))
            hotbarPanel.GetComponent<GenericInvoPanelScript>().genericInvoHandler.AddItemToGenericInventory(900, 1);
        //test for adding lavagun to hotbar
        else if (Input.GetKeyDown(KeyCode.L))
            hotbarPanel.GetComponent<GenericInvoPanelScript>().genericInvoHandler.AddItemToGenericInventory(901, 1);
        //test for adding gyrogun to hotbar
        else if (Input.GetKeyDown(KeyCode.O))
            hotbarPanel.GetComponent<GenericInvoPanelScript>().genericInvoHandler.AddItemToGenericInventory(902, 1);
        else if (Input.GetKeyDown(KeyCode.Q))
            gameManagerScript.uiScript.QuestPanel.GetComponent<QuestPanelScript>().ToggleQuestPanel();
        else if (Input.GetKeyDown(KeyCode.C))
            craftingPanelScript.ToggleCraftingPanel();
        else if (Input.GetKeyDown(KeyCode.Z))
            hotbarPanel.GetComponent<GenericInvoPanelScript>().genericInvoHandler.AddItemToGenericInventory(21, 10);

    }

    private void MineFrontLayer()
    {
        terrainManagerScript.MineTile((int)Camera.main.ScreenToWorldPoint(Input.mousePosition).x, (int)Camera.main.ScreenToWorldPoint(Input.mousePosition).y, (ushort)EnumClass.LayerIDEnum.FRONTLAYER, this);
    }

    private void MineBackLayer()
    {
        terrainManagerScript.MineTile((int)Camera.main.ScreenToWorldPoint(Input.mousePosition).x, (int)Camera.main.ScreenToWorldPoint(Input.mousePosition).y, (ushort)EnumClass.LayerIDEnum.BACKLAYER, this);
    }

    private void PlaceTileFrontLayer(ushort id)
    {
        if(terrainManagerScript.PlaceTile((int)Camera.main.ScreenToWorldPoint(Input.mousePosition).x, (int)Camera.main.ScreenToWorldPoint(Input.mousePosition).y, id, (ushort)EnumClass.LayerIDEnum.FRONTLAYER)) { }
    }

    private void PlaceTileBackLayer(ushort id)
    {
        terrainManagerScript.PlaceTile((int)Camera.main.ScreenToWorldPoint(Input.mousePosition).x, (int)Camera.main.ScreenToWorldPoint(Input.mousePosition).y, id, (ushort)EnumClass.LayerIDEnum.BACKLAYER);
    }

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
