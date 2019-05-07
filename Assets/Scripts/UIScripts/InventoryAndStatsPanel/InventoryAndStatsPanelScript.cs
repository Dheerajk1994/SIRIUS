using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryAndStatsPanelScript : MonoBehaviour {

    [SerializeField]
    private Button closeButton;

    private bool isOpen = false;
    private Animator inventoryAndStatsPanelAnimator;
    public AudioManagerScript audiomanager;

    UIScript uiScript;

    public void SetInventoryAndStatsPanel(UIScript uScript, GameObject player)
    {
        uiScript = uScript;
        audiomanager = uScript.audioManager.GetComponent<AudioManagerScript>();
        this.transform.gameObject.GetComponent<InventoryHandlerScript>().genericInventory = player.GetComponent<Inventory>();
        closeButton.onClick.AddListener(ClosePanelClicked);
    }

    private void Start()
    {
        inventoryAndStatsPanelAnimator = GetComponent<Animator>();
        //closeButton.onClick.AddListener(ToggleInventoryAndStatsPanel);
    }

    public void ToggleInventoryAndStatsPanel()
    {
        isOpen = !isOpen;
        inventoryAndStatsPanelAnimator.SetBool("isOpen", isOpen);
        audiomanager.Play("ui-animation");
    }

    public void ToggleInventoryAndStatsPanel(bool toggle)
    {
        isOpen = toggle;
        inventoryAndStatsPanelAnimator.SetBool("isOpen", isOpen);
        audiomanager.Play("ui-animation");
    }

    public void ClosePanelClicked()
    {
        ToggleInventoryAndStatsPanel(false);
        audiomanager.Play("btn-confirm");
    }

}
