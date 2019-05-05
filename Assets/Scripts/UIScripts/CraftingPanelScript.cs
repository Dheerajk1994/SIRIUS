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
    private AudioManagerScript audiomanager;
    #endregion

    #region LOCAL_VARIABLES
    private bool craftingPanelIsDisplaying;
    private enum itemTier { TIER1 = 0, TIER2 = 1, TIER3 = 2, TIER4 = 3, TIER5 = 4};
    private ushort currentSelectedTier;
    private int craftAmount;
    List<GameObject> listOfRecipes;
    CompleteItem itemInItemDescription;
    #endregion



    UIScript uiScript;

    public void SetCraftingPanel(UIScript uScript, PlayerInventoryPanelScript pips, PlayerHotbarPanelScript phps)
    {                                              
        uiScript = uScript;
        audiomanager = uScript.audioManager.GetComponent<AudioManagerScript>();
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
        craftButton.onClick.AddListener(CraftButtonClicked);

    }

    public void ToggleCraftingPanel()
    {
        //this.gameObject.SetActive(!this.gameObject.activeSelf);
        craftingPanelIsDisplaying = !craftingPanelIsDisplaying;
        this.gameObject.SetActive(craftingPanelIsDisplaying);
        audiomanager.Play("ui-animation");
        if (craftingPanelIsDisplaying)
        {
            if(itemInItemDescription != null)
            {
                SetItemDescription(itemInItemDescription.itemDescription.id);
            }
        }
    }

    public void ToggleCraftingPanel(bool toggle)
    {
        //this.gameObject.SetActive(!this.gameObject.activeSelf);
        craftingPanelIsDisplaying = toggle;
        this.gameObject.SetActive(craftingPanelIsDisplaying);
        audiomanager.Play("ui-animation");
        if (craftingPanelIsDisplaying)
        {
            if (itemInItemDescription != null)
            {
                SetItemDescription(itemInItemDescription.itemDescription.id);
            }
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
        itemInItemDescription = ItemDictionary.GetItem(id);
        if(itemInItemDescription != null)
        {
            craftAmount = 1;
            craftAmountText.text = craftAmount.ToString();
            string description = itemInItemDescription.itemDescription.itemName + " \n\n" + itemInItemDescription.itemDescription.description;
            itemDescriptionTxt.text = description;

            ClearPanel(recipePanel);
            listOfRecipes = new List<GameObject>();

            ushort[] recipe = itemInItemDescription.itemDescription.recipe;
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
                    RecipeScript script = recipeIcon.gameObject.AddComponent<RecipeScript>();
                    script.SetRecipe(type,recipeItem.id, amount);
                    listOfRecipes.Add(recipeIcon);
                }
            }
            Debug.Log(listOfRecipes.Count);
            foreach(GameObject obj in listOfRecipes)
            {
                obj.transform.SetParent(recipePanel, false);
            }
            SeeIfItemCanBeCrafted();
        }
    }

    private void CraftButtonClicked()
    {
        if (SeeIfItemCanBeCrafted())
        {
            InventoryControllerScript.instance.AddItemToInventory(itemInItemDescription.itemDescription.id, (ushort)craftAmount);
            foreach (GameObject obj in listOfRecipes)
            {
                RecipeScript script = obj.GetComponent<RecipeScript>();
                InventoryControllerScript.instance.RemoveItemFromInventoryWithType(script.type, script.currentAmnt);
            }
            UpdateRecipeAmnt();
            audiomanager.Play("btn-refuel");
        }
    }

    private void IncreaseCraftAmount()
    {
        craftAmount++;
        craftAmount = Mathf.Clamp(craftAmount, 1, 1000);
        craftAmountText.text = craftAmount.ToString();
        UpdateRecipeAmnt();
    }

    private void DecreaseCraftAmount()
    {
        craftAmount--;
        craftAmount = Mathf.Clamp(craftAmount, 1, 1000);
        craftAmountText.text = craftAmount.ToString();
        UpdateRecipeAmnt();
    }

    private void UpdateRecipeAmnt()
    {
        craftButton.interactable = true;
        foreach (GameObject obj in listOfRecipes)
        {
            if (!obj.GetComponent<RecipeScript>().AdjustCurrentAmnt((ushort)craftAmount))
            {
                craftButton.interactable = false;
            }
        }
    }

    private bool SeeIfItemCanBeCrafted()
    {
        craftButton.interactable = true;
        foreach (GameObject obj in listOfRecipes)
        {
            if (!obj.GetComponent<RecipeScript>().UpdateCraftStatus())
            {
                craftButton.interactable = false;
                return false;
            }
        }
        return true;
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
        audiomanager.Play("btn-quick-ui");
    }
    public void Tier2Clicked()
    {
        //SwitchTier((ushort)itemTier.TIER2);
        SwitchTier(1);
        audiomanager.Play("btn-quick-ui");
    }
    public void Tier3Clicked()
    {
        SwitchTier((ushort)itemTier.TIER3);
        audiomanager.Play("btn-quick-ui");
    }
    public void Tier4Clicked()
    {
        SwitchTier((ushort)itemTier.TIER4);
        audiomanager.Play("btn-quick-ui");
    }
    public void Tier5Clicked()
    {
        SwitchTier((ushort)itemTier.TIER5);
        audiomanager.Play("btn-quick-ui");
    }
    #endregion

    public void CraftingPanelItemsButtonClicked()
    {
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject.gameObject;
        try
        {
            SetItemDescription(clickedButton.GetComponent<CraftingPanelItemScript>().id);
            audiomanager.Play("btn-quick-ui");
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public void ClosePanelClicked()
    {
        ToggleCraftingPanel(false);
        audiomanager.Play("btn-confirm");
    }
}

public class CraftingPanelItemScript : MonoBehaviour
{
    public ushort id;
}

public class RecipeScript : MonoBehaviour
{
    public ushort type;
    public ushort baseAmnt;
    public ushort currentAmnt;
    public bool craftStatus = false;

    public void SetRecipe(ushort _type, ushort _id, ushort _baseAmnt)
    {
        this.type= _type;
        this.baseAmnt = _baseAmnt;
        currentAmnt = baseAmnt;

        this.GetComponent<Image>().sprite = InventorySpritesScript.instance.GetSprite(_id);
        this.GetComponentInChildren<Text>().text = currentAmnt.ToString();
        UpdateCraftStatus();
    }

    public bool AdjustCurrentAmnt(ushort amnt)
    {
        currentAmnt = (ushort)(baseAmnt * amnt);
        this.GetComponentInChildren<Text>().text = currentAmnt.ToString();
        return UpdateCraftStatus();
    }

    public bool UpdateCraftStatus()
    {
        craftStatus = InventoryControllerScript.instance.CheckForItemInInventoryWithType(type, currentAmnt);
        if (craftStatus == true)
        {
            this.GetComponentInChildren<Text>().color = Color.green;
        }
        else
        {
            this.GetComponentInChildren<Text>().color = Color.red;
        }
        return craftStatus;
    }
}
