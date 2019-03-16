using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  class InventoryHandlerScript : MonoBehaviour {

    private static InventoryHandlerScript instance;

    //INTERFACES
    [SerializeField]
    private  Inventory playerInventory;
    [SerializeField]
    private  Hotbar playerHotbar; 

    //PANEL REFERENCES
    private  GameObject inventoryPanel;
    private  GameObject hotbarPanel;

    private  GameObject[] pInventorySlots;
    public  GameObject inventorySlotItemPrefab;

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
        //playerInventory.AddItemToIndex(ItemDictionary.GetItem(1).itemDescription, 1, 0, 15);   //TEST
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

    //when mining and stuff
    public void AddItemToPlayerInventory()
    {
        CompleteItem item = ItemDictionary.GetItem(1);
        playerInventory.AddItem(item.itemDescription, 1, this, 0);
        UpdateAllPlayerInventoryPanelSlots();
        //for(int i = 0; i < 40; ++i){
        //    Debug.Log(playerInventory.FetchItemAmountInInventorySlot((ushort)i));
        //}
    }

    public void AddItemToPlayerHotBar()
    {

    }

    public void TestFunction_AddItemToRandomIndex()
    {
        playerInventory.TestFunctionAddToIndex(ItemDictionary.GetItem(1).itemDescription, 5, 23);
        UpdateAllPlayerInventoryPanelSlots();
    }

    public void TestFunction_AddStone()
    {
        playerInventory.TestFunctionAddToIndex(ItemDictionary.GetItem(2).itemDescription, 5, 25);
        UpdateAllPlayerInventoryPanelSlots();
    }

    //called when a item is dropped and stuff
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
                GameObject invItem = Instantiate(inventorySlotItemPrefab);
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
        GameObject invItem = Instantiate(inventorySlotItemPrefab);
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
}
