using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class CraftingPanelScript : MonoBehaviour {

    #region PANEL_OBJECTS
    [SerializeField] private Button craftingPanelItemsButtonPrefab;
    [SerializeField] private GameObject recipeIconPrefab;

    [SerializeField] private Transform craftableItemsPanel;
    [SerializeField] private Transform recipePanel;
    [SerializeField] private Button[] itemTierButtons;
    [SerializeField] private Button closeCraftinPanelButton;
    [SerializeField] private Button craftButton;
    [SerializeField] private Button decreaseAmountButton;
    [SerializeField] private Button increaseAmountButton;
    [SerializeField] private Text itemDescriptionTxt;
    [SerializeField] private Text craftAmountText;
    #endregion

    #region REFERENCES
    private PlayerInventoryPanelScript playerInventoryPanelScript;
    private PlayerHotbarPanelScript playerHotbarPanelScript;
    #endregion

    #region LOCAL_VARIABLES
    private bool craftingPanelIsDisplaying;
    private enum itemTier { TIER1 = 0, TIER2 = 1, TIER3 = 2, TIER4 = 3, TIER5 = 4};
    private ushort currentSelectedTier;
    private int craftAmount;
    List<GameObject> listOfRecipes;
    #endregion



    UIScript uiScript;

    public void SetCraftingPanel(UIScript uScript, PlayerInventoryPanelScript pips, PlayerHotbarPanelScript phps)
    {                                              
        uiScript = uScript;
        playerInventoryPanelScript = pips;
        playerHotbarPanelScript = phps;

        currentSelectedTier = (ushort)itemTier.TIER1;
        SwitchTier(currentSelectedTier);
        craftingPanelIsDisplaying = false;

        //setbutton listeners
        itemTierButtons[0].onClick.AddListener(Tier1Clicked);
        itemTierButtons[1].onClick.AddListener(Tier2Clicked);
        itemTierButtons[2].onClick.AddListener(Tier3Clicked);
        itemTierButtons[3].onClick.AddListener(Tier4Clicked);
        itemTierButtons[4].onClick.AddListener(Tier5Clicked);

        increaseAmountButton.onClick.AddListener(IncreaseCraftAmount);
        decreaseAmountButton.onClick.AddListener(DecreaseCraftAmount);

    }

    public void ToggleCraftingPanel()
    {
        craftingPanelIsDisplaying = !craftingPanelIsDisplaying;

        if (craftingPanelIsDisplaying)
        {
            //GenerateItemsPanel(currentSelectedTier);
        }
    }

    public void SwitchTier(ushort tier)
    {
        Color newColor = new Color(255f, 255f, 255f, 0f);
        itemTierButtons[currentSelectedTier].GetComponent<Image>().color = newColor;
        currentSelectedTier = tier;
        newColor = new Color(255f, 255f, 255f, 255f);
        itemTierButtons[currentSelectedTier].GetComponent<Image>().color = newColor;

        ClearPanel(craftableItemsPanel);

        List<ItemDescription> testList = ItemDictionary.GetItemsOfTier((ushort)(currentSelectedTier + 1));
        foreach(ItemDescription item in testList)
        {
            Button but = Instantiate(craftingPanelItemsButtonPrefab);
            but.transform.GetChild(0).GetComponent<Image>().sprite = InventorySpritesScript.instance.GetSprite(item.id);
            but.transform.SetParent(craftableItemsPanel, false);
            CraftingPanelItemScript script = but.gameObject.AddComponent<CraftingPanelItemScript>();
            script.id = item.id;
            but.onClick.AddListener(CraftingPanelItemsButtonClicked);
        }
    }

    private void SetItemDescription(ushort id)
    {
        CompleteItem item = ItemDictionary.GetItem(id);
        if(item != null)
        {
            craftAmount = 1;
            craftAmountText.text = craftAmount.ToString();
            string description = item.itemDescription.itemName + " \n\n" + item.itemDescription.description;
            itemDescriptionTxt.text = description;

            ClearPanel(recipePanel);

            ushort[] recipe = item.itemDescription.recipe;
            ushort type, amount;
            for(int i = 0; i < recipe.Length; ++i)
            {
                type = recipe[i];
                i++;
                amount = recipe[i];
                ItemDescription recipeItem = ItemDictionary.GetItemOfType(type);
                if(recipeItem != null)
                {
                    GameObject recipeIcon = Instantiate(recipeIconPrefab);
                    recipeIcon.GetComponent<Image>().sprite = InventorySpritesScript.instance.GetSprite(recipeItem.id);
                    recipeIcon.GetComponentInChildren<Text>().text = amount.ToString();
                    listOfRecipes.Add(recipeIcon);
                }
            }

            foreach(GameObject obj in listOfRecipes)
            {
                obj.transform.SetParent(recipePanel, false);
            }

        }
    }

    private void IncreaseCraftAmount()
    {
        craftAmount++;
        craftAmount = Mathf.Clamp(craftAmount, 1, 1000);
        craftAmountText.text = craftAmount.ToString();
    }

    private void DecreaseCraftAmount()
    {
        craftAmount--;
        craftAmount = Mathf.Clamp(craftAmount, 1, 1000);
        craftAmountText.text = craftAmount.ToString();
    }

    private void ClearPanel(Transform panel)
    {
        foreach(Transform children in panel)
        {
            Destroy(children.gameObject);
        }
    }

    #region TIER_BUTTON_LISTENERS
    public void Tier1Clicked()
    {
        SwitchTier((ushort)itemTier.TIER1);
    }
    public void Tier2Clicked()
    {
        //SwitchTier((ushort)itemTier.TIER2);
        SwitchTier(1);
    }
    public void Tier3Clicked()
    {
        SwitchTier((ushort)itemTier.TIER3);
    }
    public void Tier4Clicked()
    {
        SwitchTier((ushort)itemTier.TIER4);
    }
    public void Tier5Clicked()
    {
        SwitchTier((ushort)itemTier.TIER5);
    }
    #endregion

    public void CraftingPanelItemsButtonClicked()
    {
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject.gameObject;
        try
        {
            SetItemDescription(clickedButton.GetComponent<CraftingPanelItemScript>().id);
        }
        catch (Exception e)
        {
            throw e;
        }
    }

}

public class CraftingPanelItemScript : MonoBehaviour
{
    public ushort id;
}

