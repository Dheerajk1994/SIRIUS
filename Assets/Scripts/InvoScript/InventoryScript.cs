using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{

    public InventoryItem[] inventoryItems = new InventoryItem[40];
    public int inventorySize = 40;
    public int itemMaxStackAmount = 100;
    public GameObject gameManager;
    private GameObject playerInventoryPanel;
    private GameObject craftingPanel;
    private PlayerInventoryPanelScript playerInvoPanelScript;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager");
        playerInventoryPanel = gameManager.GetComponent<GameManagerScript>().playerInvoPanel;
        playerInvoPanelScript = playerInventoryPanel.GetComponent<PlayerInventoryPanelScript>();
        craftingPanel = gameManager.GetComponent<GameManagerScript>().craftingPanel;
    }

    public void AddItemToInventory(GameObject item, int amount)
    {
        if (item == null) return;
        for (int i = 0; i < inventorySize; i++)
        {
            if(inventoryItems[i] != null && inventoryItems[i].itemID == item.GetComponent<TileScript>().tileId)
            {
                inventoryItems[i].itemAmount += amount;
                UpdateUIPanels(i);
                return;
            }
        }
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventoryItems[i] == null)
            {
                inventoryItems[i] = new InventoryItem(item.GetComponent<TileScript>().tileId, item.GetComponent<SpriteRenderer>().sprite, amount, true);
                UpdateUIPanels(i);
                return;
            }
        }

    }

    public void RemoveItemFromInventory(GameObject item, int amount)
    {
        if (item == null) return;
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventoryItems[i] != null && inventoryItems[i].itemID == item.GetComponent<TileScript>().tileId)
            {
                inventoryItems[i].itemAmount -= amount;
                if(inventoryItems[i].itemAmount <= 0)
                {
                    inventoryItems[i] = null;
                }
                UpdateUIPanels(i);
            }
        }
    }

    public void RemoveItemFromInventory(int id, int amount)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventoryItems[i] != null && inventoryItems[i].itemID == id)
            {
                inventoryItems[i].itemAmount -= amount;
                if (inventoryItems[i].itemAmount <= 0)
                {
                    inventoryItems[i] = null;
                }
                UpdateUIPanels(i);
            }
        }
    }

    public bool CheckItemInInventory(GameObject item, int amount)
    {
        int totalSum = 0;
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventoryItems[i] != null && inventoryItems[i].itemID == item.GetComponent<TileScript>().tileId)
            {
                totalSum += inventoryItems[i].itemAmount;
            }
        }
        return (totalSum >= amount);
    }

    public bool CheckItemInInventory(int id, int amount)
    {
        int totalSum = 0;
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventoryItems[i] != null && inventoryItems[i].itemID == id)
            {
                totalSum += inventoryItems[i].itemAmount;
            }
        }
        return (totalSum >= amount);
    }

    public bool CheckInventorySpace()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventoryItems[i] == null)
            {
                return true;
            }
        }
        return false;
    }

    void UpdateUIPanels(int i)
    {
        if (playerInventoryPanel.gameObject.activeInHierarchy)
        {
            playerInvoPanelScript.UpdatePlayerInventoryPanel(i);
        }

        if (craftingPanel.gameObject.activeInHierarchy)
        {
            craftingPanel.GetComponent<CraftingPanelScript>().UpdateCraftingPanel();
        }
       
    }
}

public class InventoryItem
{
    public InventoryItem(ushort id, Sprite img, int amnt, bool stack)
    {
        itemID = id;
        itemImage = img;
        itemAmount = amnt;
        stackable = stack;
    }
    public ushort itemID;
    public Sprite itemImage;
    public int itemAmount;
    public bool stackable;
}