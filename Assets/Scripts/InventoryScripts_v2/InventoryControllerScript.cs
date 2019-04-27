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

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            playerHotbarPanelScript.genericInvoHandler.AddItemToGenericInventory(2, 10);
        }
    }
}
