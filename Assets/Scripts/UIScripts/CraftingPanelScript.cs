using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingPanelScript : MonoBehaviour {

    public GameObject gameManager;
    //private InventoryScript playerInvo;
    public GameObject craftableItemsPanel;
    public GameObject craftingPanelItemPrefab;

    public GameObject craftCamfireButton;
    public GameObject craftAxeButton;
    public GameObject craftPickButton;

    public GameObject campfire;
    public GameObject axe;
    public GameObject pickaxe;


    private void Start()
    {
        gameManager = GameObject.Find("GameManager");
        //playerInvo = gameManager.GetComponent<GameManagerScript>().player.GetComponent<InventoryScript>();
    }

    public void UpdateCraftingPanel()
    {
        //CAMPFIRE
        //craftCamfireButton.GetComponentInChildren<Button>().interactable = (playerInvo.CheckItemInInventory(2, 10));

        //AXE
        //craftAxeButton.GetComponentInChildren<Button>().interactable = (playerInvo.CheckItemInInventory(1, 10) && playerInvo.CheckItemInInventory(2, 10));

        //PICK
        //craftPickButton.GetComponentInChildren<Button>().interactable = (playerInvo.CheckItemInInventory(1, 10) && playerInvo.CheckItemInInventory(2, 10));
    }

    public void CraftCampfire()
    {
        GameObject cf = Instantiate(campfire);
        //playerInvo.AddItemToInventory(cf, 1);
        //playerInvo.RemoveItemFromInventory(2, 10);
    }

    public void CraftAxe()
    {

    }

    public void CraftPickAxe()
    {

    }


}
