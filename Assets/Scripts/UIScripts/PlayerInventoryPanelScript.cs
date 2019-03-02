using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryPanelScript : MonoBehaviour {

    public Button[] inventoryItemsButtons;
    public GameObject player;

    public void UpdatePlayerInventoryPanel()
    {
        //InventoryScript playerInventoryScript = player.GetComponent<InventoryScript>();

        //if(inventoryItemsButtons.Length <= playerInventoryScript.inventorySize)
        //{
        //    for (int i = 0; i < inventoryItemsButtons.Length; i++)
        //    {
        //        if(playerInventoryScript.inventoryItems[i] != null)
        //        {
        //            inventoryItemsButtons[i].GetComponentInChildren<Text>().text = playerInventoryScript.inventoryItems[i].itemAmount.ToString();
        //            inventoryItemsButtons[i].GetComponentsInChildren<Image>()[1].enabled = true;
        //            inventoryItemsButtons[i].GetComponentsInChildren<Image>()[1].sprite = playerInventoryScript.inventoryItems[i].itemImage;
        //        }
        //        else
        //        {
        //            inventoryItemsButtons[i].GetComponentInChildren<Text>().text = " ";
        //            inventoryItemsButtons[i].GetComponentsInChildren<Image>()[1].enabled = false;
        //        }
        //    }
        //}
    }

    public void UpdatePlayerInventoryPanel(int i)
    {
        //InventoryScript playerInventoryScript = player.GetComponent<InventoryScript>();

        //if (playerInventoryScript.inventoryItems[i] != null)
        //{
        //    inventoryItemsButtons[i].GetComponentInChildren<Text>().text = playerInventoryScript.inventoryItems[i].itemAmount.ToString();
        //    inventoryItemsButtons[i].GetComponentsInChildren<Image>()[1].enabled = true;
        //    inventoryItemsButtons[i].GetComponentsInChildren<Image>()[1].sprite = playerInventoryScript.inventoryItems[i].itemImage;
        //}
        //else
        //{
        //    inventoryItemsButtons[i].GetComponentInChildren<Text>().text = " ";
        //    inventoryItemsButtons[i].GetComponentsInChildren<Image>()[1].enabled = false;
        //}
    }


}
