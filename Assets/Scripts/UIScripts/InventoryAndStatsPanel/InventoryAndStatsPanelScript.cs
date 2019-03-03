using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryAndStatsPanelScript : MonoBehaviour {

    [SerializeField]
    private Button closeButton;

    private bool isOpen = false;
    private Animator inventoryAndStatsPanelAnimator;

    private void Start()
    {
        inventoryAndStatsPanelAnimator = GetComponent<Animator>();
        closeButton.onClick.AddListener(ToggleInventoryAndStatsPanel);
    }

    public void ToggleInventoryAndStatsPanel()
    {
        isOpen = !isOpen;
        inventoryAndStatsPanelAnimator.SetBool("isOpen", isOpen);
    }

    public void ToggleInventoryAndStatsPanel(bool toggle)
    {
        isOpen = toggle;
        inventoryAndStatsPanelAnimator.SetBool("isOpen", isOpen);
    }


}
