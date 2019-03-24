using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryControllerScript : MonoBehaviour {

    GameManagerScript gameManagerScript;
    PlayerInventoryPanelScript playerInventoryPanelScript;
    PlayerHotbarPanelScript playerHotbarPanelScript;
    //need chest panel here

    public void SetInventoryController(GameManagerScript gms, UIScript uiScript)
    {
        this.gameManagerScript = gms;
        this.playerInventoryPanelScript = uiScript.PlayerInventoryAndStatsPanel.GetComponent<PlayerInventoryPanelScript>();
        this.playerHotbarPanelScript = uiScript.PlayerHotBarPanel.GetComponent<PlayerHotbarPanelScript>();
    }


    public ushort PickeupTile(TilePickUpScript tile)
    {
        Debug.Log("Picked up a tile");
        ushort remaining = playerHotbarPanelScript.genericInvoHandler.AddItemToGenericInventory(tile.contentId, tile.currentStackAmount);
        if (remaining > 0) remaining = playerInventoryPanelScript.genericInvoHandler.AddItemToGenericInventory(tile.contentId, remaining);
        return remaining;
    }
}
