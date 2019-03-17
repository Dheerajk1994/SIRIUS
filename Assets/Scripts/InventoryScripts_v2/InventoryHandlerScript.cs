using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  class InventoryHandlerScript : GenericInvoHandlerScript { 

/*
    private static InventoryHandlerScript instance;

    //INTERFACES
    [SerializeField]
    private  Inventory playerInventory;
    [SerializeField]
    private  Hotbar playerHotbar; 

    //PANEL REFERENCES
    private  GameObject inventoryPanel;
    private  GameObject hotbarPanel;

    public  GameObject inventoryItemPrefab;
    
    //reference to the invo slots - game start
    private  GameObject[] pInventorySlots;

    //reference to the hotbar slots - game start
    private GameObject[] pHotbarSlots;

    //reference to chest slots - instantiated whenver a chest is opened


    //SINGLETON MAKE SURE ONLY ONE OF THE INSTANCES EXIST
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        ItemDictionary.GenerateDictionary();

        pInventorySlots = GetComponent<PlayerInventoryPanelScript>().slots;
        pHotbarSlots = GameObject.Find("HotBar").GetComponent<PlayerHotbarPanelScript>().slots;

        UpdateAllPlayerInventoryPanelSlots();
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

        //fetch reference to slots
        pInventorySlots = GetComponent<PlayerInventoryPanelScript>().slots;
        UpdateAllPlayerInventoryPanelSlots();
    }
    */
    //when mining and stuff

    //    private void Start()
    //{
    //    slots = genericInventoryPanel.GetComponent<GenericInvoPanelScript>().slots;
    //    UpdatePanelSlots();
    //}

    public void AddItemToPlayerInventory()
    {
        CompleteItem item = ItemDictionary.GetItem(1);
        genericInventory.AddItem(item.itemDescription, 1, this, 0);
        UpdatePanelSlots();
    }

    
    public void TestFunction_AddItemToRandomIndex()
    {
        genericInventory.TestFunctionAddToIndex(ItemDictionary.GetItem(1).itemDescription, 5, 23);
        UpdatePanelSlots();
    }

    public void TestFunction_AddStone()
    {
        genericInventory.TestFunctionAddToIndex(ItemDictionary.GetItem(2).itemDescription, 5, 25);
        UpdatePanelSlots();
    }

    //called when a item is dropped and stuff

    //what to do if we drag an item from p invo into p hotbar? and vise versa


        /*
    public void HandleItemDrop(InventorySlot parentSlot, InventorySlot newSlot, InventoryItem itemBeingDragged){

        //if new slot is empty just place item there and update inventory index
        if(!newSlot.isHoldingAnItem) { 
            newSlot.inventoryReference.AddItemToIndex(itemBeingDragged.completeItem.itemDescription, itemBeingDragged, itemBeingDragged.stackCount, parentSlot.slotID, newSlot.slotID);
            //while (pInventorySlots[parentSlot.slotID].transform.childCount > 0) Destroy(pInventorySlots[parentSlot.slotID].transform.GetChild(0).gameObject);
            foreach (Transform child in pInventorySlots[parentSlot.slotID].transform) { Destroy(child.gameObject); }
            parentSlot.isHoldingAnItem = false;
            newSlot.itemInSlot = InventoryItem.itemBeingDragged;
            InventoryItem.itemBeingDragged.GetComponent<InventoryItem>().parentSlot.GetComponent<InventorySlot>().itemInSlot = null;
            InventoryItem.itemBeingDragged.transform.SetParent(newSlot.gameObject.transform, false);
        }
        else{
            //if new slot has same item then place item in index
            if (newSlot.transform.GetChild(0).GetComponent<InventoryItem>().completeItem.itemDescription.id == itemBeingDragged.GetComponent<InventoryItem>().completeItem.itemDescription.id)
            {
                //Debug.Log("same item detected in new slot");
                ushort remainingAmount = newSlot.inventoryReference.AddItemToIndex(itemBeingDragged.completeItem.itemDescription, itemBeingDragged, itemBeingDragged.stackCount, parentSlot.slotID, newSlot.slotID);
                Debug.Log("remaining amount: " + remainingAmount);
                if(remainingAmount == 0)
                {
                    //InventoryItem.itemBeingDragged = null;
                    Destroy(itemBeingDragged.gameObject);
                }
                UpdateAllPlayerInventoryPanelSlots();
            }

            //if new slot has different item then swap
            else
            {
                playerInventory.SwapInventoryItems(parentSlot.slotID, newSlot.slotID);
                UpdateAllPlayerInventoryPanelSlots();
            }
        }
        UpdateAllPlayerInventoryPanelSlots();
    }
    

    public void UpdateAllPlayerInventoryPanelSlots()
    { 
        //go through each inventory index
        for (ushort i = 0; i < playerInventory.GetInventorySize(); ++i)
        {
            ushort id = playerInventory.FetchItemIdInInventorySlot(i);
            if(id != 0)
            {
                foreach (Transform child in pInventorySlots[i].transform) { Destroy(child.gameObject); }

                ushort amnt = playerInventory.FetchItemAmountInInventorySlot(i);
                CompleteItem item = ItemDictionary.GetItem(id);
                GameObject invItem = Instantiate(inventoryItemPrefab);
                invItem.GetComponent<InventoryItem>().UpdateInventoryItem(item, amnt);
               
                invItem.transform.SetParent(pInventorySlots[i].transform, false);
                pInventorySlots[i].GetComponent<InventorySlot>().isHoldingAnItem = true;
            }
            else
            {
                foreach (Transform child in pInventorySlots[i].transform) { Destroy(child.gameObject); }
            }
        }
    }
     
    public void UpdatePlayerInventorySlot(ushort slotIndex)
    {
        ushort id = playerInventory.FetchItemIdInInventorySlot(slotIndex);
        ushort amnt = playerInventory.FetchItemAmountInInventorySlot(slotIndex);

        CompleteItem item = ItemDictionary.GetItem(id);
        GameObject invItem = Instantiate(inventoryItemPrefab);
        invItem.GetComponent<InventoryItem>().UpdateInventoryItem(item, amnt);
        foreach (Transform child in pInventorySlots[slotIndex].transform) { Destroy(child.gameObject); }
        invItem.transform.SetParent(pInventorySlots[slotIndex].transform, false);
        pInventorySlots[slotIndex].GetComponent<InventorySlot>().isHoldingAnItem = true;

    }


    public void UpdatePlayerHotbar()
    {

    }

    //SHOULD CHECK BOTH INVO AND HOTBAR
    public bool RemoveItemFromPlayer()
    {
        return true;
    }
    */
}
