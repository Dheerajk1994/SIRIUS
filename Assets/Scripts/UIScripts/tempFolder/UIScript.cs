using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    #region REFERENCES
    public GameObject PlayerInventoryAndStatsPanel;
    public GameObject InventoryAndStatsPanel;
    public GameObject PlayerHotBarPanel;
    public GameObject PlayerAttributePanel;
    public GameObject PlayerCraftingPanel;
    public GameObject QuestPanel;
    public GameObject BottomDialoguePanel;
    public GameObject ExitShipPanel;
    public GameObject EnginePanel;
    public GameObject NavPanel;
    public GameObject ChestPanel;
    public Button teleportButton;
    public Button questButton;
    public Button inventoryButton;
    public GameObject EscapePanel;
    public GameObject ErrorPanel;
    public Transform loadingScreen;

    public GameManagerScript gameManagerScript;
    public InputManagerScript inputManagerScript;
    public AudioManagerScript audioManager;
    public GameObject player;
    #endregion

    //should be called by the gamemanager
    public void SetUIPanel(GameManagerScript gManagerS, InputManagerScript iManagerS, AudioManagerScript aManager, GameObject p)
    {
        gameManagerScript = gManagerS;
        inputManagerScript = iManagerS;
        audioManager = aManager;

        player = p;

        //PlayerCraftingPanel.SetActive(false);

        teleportButton.onClick.AddListener(TeleportButtonClicked);
        SetUIReferences();
    }

    private void SetUIReferences()
    {
        PlayerInventoryAndStatsPanel.GetComponent<InventoryHandlerScript>().genericInventory = player.GetComponent<Inventory>();
        PlayerInventoryAndStatsPanel.GetComponent<PlayerInventoryPanelScript>().SetPlayerInventoryPanel(player.GetComponent<Inventory>());

        InventoryAndStatsPanel.GetComponent<InventoryAndStatsPanelScript>().SetInventoryAndStatsPanel(this, player);

        PlayerHotBarPanel.GetComponent<HotbarHandlerScript>().genericInventory = player.GetComponent<Hotbar>();
        PlayerHotBarPanel.GetComponent<PlayerHotbarPanelScript>().SetPlayerHotbarPanel(player.GetComponent<Hotbar>(), gameManagerScript.playerScript);

        PlayerAttributePanel.GetComponent<PlayerAttributesPanelScript>().SetPlayerAttributesPanel(this);

        PlayerCraftingPanel.GetComponent<CraftingPanelScript>().SetCraftingPanel(this, PlayerInventoryAndStatsPanel.GetComponent<PlayerInventoryPanelScript>(), PlayerHotBarPanel.GetComponent<PlayerHotbarPanelScript>());

        QuestPanel.GetComponent<QuestPanelScript>().SetQuestPanel(gameManagerScript);

        BottomDialoguePanel.GetComponent<DialoguePanelScript>().SetDialoguePanel(this);

        //MainMenuPanel.GetComponent<MainMenuScript>().SetMainMenuPanel(this);

        EscapePanel.GetComponent<EscapePanelScript>().SetEscapePanel(gameManagerScript);

        ErrorPanel.GetComponent<ErrorPanelScript>().SetErrorPanel();

        inventoryButton.onClick.AddListener(TogglePlayerInvoPanel);
        questButton.onClick.AddListener(ToggleQuestPanel);

        if(gameManagerScript.ship != null)
        {
            ExitShipPanel.GetComponent<ExitShipScript>().SetExitShipPanel(this);

            EnginePanel.GetComponent<EngineScript>().SetEnginePanel(this);

            NavPanel.GetComponent<NavigationScript>().SetNavPanel(this);

            ChestPanel.GetComponent<ChestPanelScript>().SetChestPanel(this);

            teleportButton.gameObject.SetActive(false);
        }

    }

    public void TeleportButtonClicked()
    {
        gameManagerScript.TeleportToShip();
    }

    public void TogglePlayerInvoPanel()
    {
        InventoryAndStatsPanel.GetComponent<InventoryAndStatsPanelScript>().ToggleInventoryAndStatsPanel();
    }

    public void ToggleQuestPanel()
    {
        QuestPanel.GetComponent<QuestPanelScript>().ToggleQuestPanel();
    }

}
