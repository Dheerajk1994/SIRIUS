using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EngineScript : GenericInvoPanelScript
{
    //for engine inventory
    public ItemHolder engineItemHolder;
    public GameObject slot;

    //


    private Animator enginePanelAnimator;
    public Button exitButn;
    public Button refuelButn;
    public Slider currentFuel;
    private bool isOpen = false;

    UIScript uiScript;

    public void SetEnginePanel(UIScript uScript)
    {
        uiScript = uScript;

        slots = new GameObject[1]; //NEED OPTIMIZATION
        slot.transform.SetParent(genericInvoPanel.transform, false);
        slot.GetComponent<InventorySlot>().slotID = 0;
        slots[0] = slot;
        slot.GetComponent<InventorySlot>().inventoryReference = engineItemHolder;
        slot.GetComponent<InventorySlot>().genericInvoHandler = this.genericInvoHandler;

    
        genericInvoHandler.slots = slots;

    }

    private void Start()
    {
        enginePanelAnimator = this.GetComponent<Animator>();
        //enginePanelToggleButn.onClick.AddListener(ToggleEnginePanel);
    }

    public void ToggleEnginePanel(bool toggle)
    {
        isOpen = toggle;
        enginePanelAnimator.SetBool("isOpen", toggle);
    }

    public void ToggleEnginePanel()
    {
        isOpen = !isOpen;
        enginePanelAnimator.SetBool("isOpen", isOpen);
    }
}
