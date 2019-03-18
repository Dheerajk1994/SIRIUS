using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingPanelScript : MonoBehaviour {

    public GameObject gameManager;
    public GameObject craftableItemsPanel;
    public GameObject craftingPanelItemPrefab;

    public GameObject craftCamfireButton;
    public GameObject craftAxeButton;
    public GameObject craftPickButton;

    public GameObject campfire;
    public GameObject axe;
    public GameObject pickaxe;


    UIScript uiScript;

    public void SetCraftingPanel(UIScript uScript)
    {
        uiScript = uScript;
    }

    private void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    public void UpdateCraftingPanel()
    {

    }

    public void CraftCampfire()
    {
    }

    public void CraftAxe()
    {

    }

    public void CraftPickAxe()
    {

    }


}
