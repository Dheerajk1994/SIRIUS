using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EngineScript : GenericInvoPanelScript
{
    //for engine inventory
    public ItemHolder engineItemHolder;
    public GameObject slot;

    public AudioManagerScript audiomanager;
    public Engine engineObjectScript;

    private Animator enginePanelAnimator;
    public Button exitButn;
    public Button refuelButn;
    public Slider currentFuel;
    private bool isOpen = false;

    UIScript uiScript;

    public void SetEnginePanel(UIScript uScript)
    {
        uiScript = uScript;
        audiomanager = uiScript.audioManager.GetComponent<AudioManagerScript>();

        slots = new GameObject[1]; //NEED OPTIMIZATION
        slot.transform.SetParent(genericInvoPanel.transform, false);
        slot.GetComponent<InventorySlot>().slotID = 0;
        slots[0] = slot;
        slot.GetComponent<InventorySlot>().inventoryReference = engineItemHolder;
        slot.GetComponent<InventorySlot>().genericInvoHandler = this.genericInvoHandler;

        engineObjectScript = uiScript.gameManagerScript.ship.GetComponent<ShipScript>().GetEngineReference().GetComponent<Engine>();
    
        genericInvoHandler.slots = slots;

        refuelButn.onClick.AddListener(RefuelButtonClicked);
        exitButn.onClick.AddListener(ClosePanelClicked);
    }

    private void Start()
    {
        enginePanelAnimator = this.GetComponent<Animator>();
        //enginePanelToggleButn.onClick.AddListener(ToggleEnginePanel);
    }

    public void RefuelButtonClicked()
    {
        //transform.GetChild(0).GetComponent<InventoryItem>().completeItem.itemDescription.type
        if (slot.GetComponent<InventorySlot>().isHoldingAnItem && slot.transform.GetChild(0).GetComponent<InventoryItem>().completeItem.itemDescription.type == 5)
        {
            engineObjectScript.Refuel(slot.transform.GetChild(0).GetComponent<InventoryItem>().stackCount);
            UpdateFuel();
            slot.GetComponent<InventorySlot>().isHoldingAnItem = false;
            Destroy(slot.transform.GetChild(0).gameObject);

        }
        else
        {
            Debug.Log("Can't refuel this. Item must be of fuel type");
            audiomanager.Play("btn-deny");
        }
    }

    public void ClosePanelClicked(){
        ToggleEnginePanel(false);
    }

    public void ToggleEnginePanel(bool toggle)
    {
        isOpen = toggle;
        enginePanelAnimator.SetBool("isOpen", toggle);
        audiomanager.Play("ui-animation");
        if (isOpen)
        {
            UpdateFuel();
        }
    }

    public void ToggleEnginePanel()
    {
        isOpen = !isOpen;
        enginePanelAnimator.SetBool("isOpen", isOpen);
        audiomanager.Play("ui-animation");
        if (isOpen)
        {
            UpdateFuel();
        }
    }

    public void UpdateFuel(){
        currentFuel.value = engineObjectScript.currentFuel;
    }
}
