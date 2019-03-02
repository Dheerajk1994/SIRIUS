using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandlerScript : MonoBehaviour {

    private static InventoryHandlerScript instance;

    //INTERFACES
    private Inventory playerInventory;
    private Hotbar playerHotbar;

    //PANEL REFERENCES
    private GameObject inventoryPanel;
    private GameObject hotbarPanel;

    //SINGLETON MAKE SURE ONLY ONE OF THE INSTANCES EXIST
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        
    }

    //CALLED BY GAMEMANAGER TO SET ALL REFERENCES
    public void SetInventoryHandler(
        Inventory pInv,
        Hotbar pHbar,
        GameObject iPanel,
        GameObject hbarPanel
        )
    {
        playerInventory = pInv;
        playerHotbar = pHbar;
        inventoryPanel = iPanel;
        hotbarPanel = hbarPanel;
    }

    public void AddItemToPlayerInventory()
    {

    }

    public void AddItemToPlayerHotBar()
    {

    }

    public void UpdatePlayerInventory()
    {

    }

    public void UpdatePlayerHotbar()
    {

    }

    //SHOULD CHECK BOTH INVO AND HOTBAR
    public bool RemoveItemFromPlayer()
    {
        return true;
    }
}
