using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryControllerScript : MonoBehaviour {

    GameManagerScript gameManagerScript;
    PlayerInventoryPanelScript playerInventoryPanelScript;
    PlayerHotbarPanelScript playerHotbarPanelScript;
    public AudioManagerScript audioManagerScript;
    //need chest panel here

    public static InventoryControllerScript instance;

    private void Awake()
    {
        instance = this;
    }

    public void SetInventoryController(GameManagerScript gms, UIScript uiScript, AudioManagerScript aManager)
    {
        this.gameManagerScript = gms;
        this.playerInventoryPanelScript = uiScript.PlayerInventoryAndStatsPanel.GetComponent<PlayerInventoryPanelScript>();
        this.playerHotbarPanelScript = uiScript.PlayerHotBarPanel.GetComponent<PlayerHotbarPanelScript>();
        audioManagerScript = aManager;
    }

    public bool CheckForItemInInventoryWithId(ushort id, ushort neededAmount)
    {
        ushort tempAmnt = playerHotbarPanelScript.genericInvoHandler.CheckItemAmountInGenericInventory(id, neededAmount);
        if(tempAmnt < neededAmount)
        {
            tempAmnt += playerInventoryPanelScript.genericInvoHandler.CheckItemAmountInGenericInventory(id, neededAmount);
            if (tempAmnt >= neededAmount) return true;
            return false;
        }
        return true;
    }

    public bool CheckForItemInInventoryWithType(ushort type, ushort neededAmount)
    {
        ushort tempAmnt = 0;
        List<ItemDescription> itemsOfType = ItemDictionary.GetItemsOfType(type);

        foreach(ItemDescription items in itemsOfType)
        {
            tempAmnt += playerHotbarPanelScript.genericInvoHandler.CheckItemAmountInGenericInventory(items.id, neededAmount);
            tempAmnt += playerInventoryPanelScript.genericInvoHandler.CheckItemAmountInGenericInventory(items.id, neededAmount);
            if(tempAmnt >= neededAmount)
            {
                return true;
            }
        }
        return false;
    }

    public ushort AddItemToInventory(ushort id, ushort amnt)
    {
        ushort tempVar1 = 0;
        ushort tempVar2 = 0;

        ushort remaining = playerHotbarPanelScript.genericInvoHandler.AddItemToGenericInventory(id, amnt);
        tempVar1 += (ushort)(amnt - remaining);
        tempVar2 += remaining;

        if (remaining > 0) remaining = playerInventoryPanelScript.genericInvoHandler.AddItemToGenericInventory(id, remaining);
        tempVar2 += (ushort)(tempVar2 - remaining);

        QuestManagerScript.instance.PickedUpItem(id, tempVar1 + tempVar2);

        return remaining;
    }

    public ushort PickeupTile(TilePickUpScript tile)
    {
        //Debug.Log("Picked up a tile");

        ushort tempVar1 = 0;
        ushort tempVar2 = 0;

        ushort remaining = playerHotbarPanelScript.genericInvoHandler.AddItemToGenericInventory(tile.contentId, tile.currentStackAmount);
        tempVar1 += (ushort)(tile.currentStackAmount - remaining);
        tempVar2 += remaining;

        if (remaining > 0) remaining = playerInventoryPanelScript.genericInvoHandler.AddItemToGenericInventory(tile.contentId, remaining);
        tempVar2 += (ushort)(tempVar2 - remaining);

        QuestManagerScript.instance.PickedUpItem(tile.contentId, tempVar1 + tempVar2);

        return remaining;
    }

    public void RemoveItemFromInventoryWithType(ushort type, ushort removeAmount)
    {
        ushort tempAmnt = removeAmount;
        List<ItemDescription> itemsOfType = ItemDictionary.GetItemsOfType(type);

        foreach (ItemDescription items in itemsOfType)
        {
            if(tempAmnt > 0)
            {
                tempAmnt = playerHotbarPanelScript.genericInvoHandler.RemoveItemsFromGenericInventory(items.id, tempAmnt);
            }
            if(tempAmnt > 0)
            {
                tempAmnt = playerInventoryPanelScript.genericInvoHandler.RemoveItemsFromGenericInventory(items.id, tempAmnt);
            }
        }
    }

    public void RemoveItemFromInventoryWithID(ushort id, ushort removeAmount)
    {

    }

    //populate playerinventory with items
    public void PopulatePlayerInventory(ushort [,] items)
    {
        Debug.Log("populate inventory called");
        playerInventoryPanelScript.genericInvoHandler.PopulateInventory(items);
    }
    //fetch items in player inventory
    public ushort[,] FetchItemsInPlayerInventory()
    {
        Debug.Log("fetch inventory called");
        return playerInventoryPanelScript.genericInvoHandler.FetchAllItemsInInventory();
    }

    //populate player hotbar with items
    public void PopulatePlayerHotbar(ushort[,] items)
    {
        playerHotbarPanelScript.genericInvoHandler.PopulateInventory(items);
    }
    //fetch items in player hotbar
    public ushort[,] FetchItemsInPlayerHotbar()
    {
        return playerHotbarPanelScript.genericInvoHandler.FetchAllItemsInInventory();
    }



    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            playerHotbarPanelScript.genericInvoHandler.AddItemToGenericInventory(2, 10);
        }
    }
}
