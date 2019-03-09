using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHandlerScript : MonoBehaviour {

    private static InventoryHandlerScript instance;

    //INTERFACES
    [SerializeField]
    private Inventory playerInventory;
    [SerializeField]
    private Hotbar playerHotbar;

    //PANEL REFERENCES
    private GameObject inventoryPanel;
    private GameObject hotbarPanel;

    private GameObject[] pInventorySlots;
    public GameObject inventorySlotItemPrefab;

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
        ItemDictionary.GenerateDictionary();
        pInventorySlots = GetComponent<PlayerInventoryPanelScript>().slots;
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

    public void AddItemToPlayerInventory()
    {
        CompleteItem item = ItemDictionary.GetItem(1);
        playerInventory.AddItem(item.itemDescription, 1000, this, 0);
        UpdateAllPlayerInventoryPanelSlots();
    }

    public void AddItemToPlayerHotBar()
    {

    }

    public void UpdateAllPlayerInventoryPanelSlots()
    {
        for (ushort i = 0; i < playerInventory.GetInventorySize(); ++i)
        {
            if(playerInventory.FetchItemIdInInventorySlot(i) != 0){
                //check if existing slotitem has same id
                if (pInventorySlots[i].transform.childCount > 0 && pInventorySlots[i].transform.GetChild(0).GetComponent<InventoryItem>().completeItem.itemDescription.id == playerInventory.FetchItemIdInInventorySlot(i)){
                    //just update the text
                    pInventorySlots[i].transform.GetChild(0).GetComponent<InventoryItem>().GetComponentInChildren<Text>().text = playerInventory.FetchItemAmountInInventorySlot(i).ToString();
                }
                else{
                    if(pInventorySlots[i].transform.childCount > 0)Destroy(pInventorySlots[i].transform.GetChild(0));
                    CompleteItem item = ItemDictionary.GetItem(playerInventory.FetchItemIdInInventorySlot(i));
                    if(item != null){
                        GameObject invSlotItem = Instantiate(inventorySlotItemPrefab);
                        invSlotItem.GetComponent<InventoryItem>().completeItem = item;
                        invSlotItem.GetComponentInChildren<Text>().text = playerInventory.FetchItemAmountInInventorySlot(i).ToString();
                        invSlotItem.GetComponent<Image>().sprite = item.icon;
                        invSlotItem.transform.SetParent(pInventorySlots[i].transform, false);
                    }
                }
            }
        }
    }
     
    public void UpdatePlayerInventorySlot(ushort slotIndex)
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
