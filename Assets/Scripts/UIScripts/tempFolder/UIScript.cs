using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    #region REFERENCES
    public GameObject PlayerInventoryAndStatsPanel;
    public GameObject PlayerHotBarPanel;
    public GameObject PlayerAttributePanel;
    public GameObject PlayerCraftingPanel;
    public GameObject QuestPanel;
    public GameObject BottomDialoguePanel;
    public GameObject MainMenuPanel;
    public GameObject ExitShipPanel;
    public GameObject EnginePanel;
    public GameObject NavPanel;
    public GameObject ChestPanel;

    public GameManagerScript gameManagerScript;
    public InputManagerScript inputManagerScript;
    public GameObject player;
    #endregion

    //should be called by the gamemanager
    public void SetUIPanel(GameManagerScript gManagerS, InputManagerScript iManagerS, GameObject p)
    {
        gameManagerScript = gManagerS;
        inputManagerScript = iManagerS;
        player = p;

        //PlayerCraftingPanel.SetActive(false);
        SetUIReferences();
    }

    private void SetUIReferences()
    {
        PlayerInventoryAndStatsPanel.GetComponent<InventoryHandlerScript>().genericInventory = player.GetComponent<Inventory>();
        PlayerInventoryAndStatsPanel.GetComponent<PlayerInventoryPanelScript>().SetPlayerInventoryPanel(player.GetComponent<Inventory>());

        PlayerHotBarPanel.GetComponent<HotbarHandlerScript>().genericInventory = player.GetComponent<Hotbar>();
        PlayerHotBarPanel.GetComponent<PlayerHotbarPanelScript>().SetPlayerHotbarPanel(player.GetComponent<Hotbar>(), gameManagerScript.playerScript);

        PlayerAttributePanel.GetComponent<PlayerAttributesPanelScript>().SetPlayerAttributesPanel(this);

        PlayerCraftingPanel.GetComponent<CraftingPanelScript>().SetCraftingPanel(this, PlayerInventoryAndStatsPanel.GetComponent<PlayerInventoryPanelScript>(), PlayerHotBarPanel.GetComponent<PlayerHotbarPanelScript>());

        QuestPanel.GetComponent<QuestPanelScript>().SetQuestPanel(gameManagerScript);

        BottomDialoguePanel.GetComponent<DialoguePanelScript>().SetDialoguePanel(this);

        MainMenuPanel.GetComponent<MainMenuScript>().SetMainMenuPanel(this);

        ExitShipPanel.GetComponent<ExitShipScript>().SetExitShipPanel(this);

        EnginePanel.GetComponent<EngineScript>().SetEnginePanel(this);

        NavPanel.GetComponent<NavigationScript>().SetNavPanel(this);

        //ChestPanel.GetComponent<ChestScript>().SetChestPanel(this);

    }


}
