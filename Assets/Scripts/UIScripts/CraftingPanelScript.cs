using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingPanelScript : MonoBehaviour {

    #region PANEL_OBJECTS
    [SerializeField]
    private GameObject craftableItemsPanel;
    [SerializeField]
    private Button[] itemTierButtons;
    [SerializeField]
    private Button closeCraftinPanelButton;
    [SerializeField]
    private Button craftingPanelItemsButtonPrefab;
    #endregion

    #region REFERENCES
    private PlayerInventoryPanelScript playerInventoryPanelScript;
    private PlayerHotbarPanelScript playerHotbarPanelScript;
    #endregion

    #region LOCAL_VARIABLES
    private bool craftingPanelIsDisplaying;
    private enum itemTier { TIER1 = 0, TIER2 = 1, TIER3 = 2, TIER4 = 3, TIER5 = 4};
    private ushort currentSelectedTier;
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

    }

    public void ToggleCraftingPanel()
    {
        craftingPanelIsDisplaying = !craftingPanelIsDisplaying;

        if (craftingPanelIsDisplaying)
        {
            GenerateItemsPanel(currentSelectedTier);
        }
    }

    public void SwitchTier(ushort tier)
    {
        //Debug.Log("switch tier called " + tier);
        Color newColor = new Color(255f, 255f, 255f, 0f);
        itemTierButtons[currentSelectedTier].GetComponent<Image>().color = newColor;
        currentSelectedTier = tier;
        newColor = new Color(255f, 255f, 255f, 255f);
        itemTierButtons[currentSelectedTier].GetComponent<Image>().color = newColor;
    }


    private void GenerateItemsPanel(ushort tier)
    {

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

}


