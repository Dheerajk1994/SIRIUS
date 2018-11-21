using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public GameObject gameManager;
    private GameObject playerInvoPanel;
    private GameObject craftingPanel;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager");

        playerInvoPanel = gameManager.GetComponent<GameManagerScript>().playerInvoPanel;
        playerInvoPanel.gameObject.SetActive(false);

        craftingPanel = gameManager.GetComponent<GameManagerScript>().craftingPanel;
        craftingPanel.gameObject.SetActive(false);

    }

    public void TogglePlayerInventory()
    {
        playerInvoPanel.gameObject.SetActive(!playerInvoPanel.gameObject.activeInHierarchy);
        playerInvoPanel.GetComponent<PlayerInventoryPanelScript>().UpdatePlayerInventoryPanel();
    }

    public void ToggleCraftingPanel()
    {
        craftingPanel.gameObject.SetActive(!craftingPanel.gameObject.activeInHierarchy);
        craftingPanel.GetComponent<CraftingPanelScript>().UpdateCraftingPanel();
    }

}
