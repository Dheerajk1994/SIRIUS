using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryPanelScript : MonoBehaviour {

    [SerializeField]
    private GameObject slotPrefab;
    public GameObject[] slots;
    [SerializeField]
    private GameObject invoPanel;
    public Inventory playerInventoryReference;
    public InventoryHandlerScript inventoryHandler;
    

	// Use this for initialization
	void Start () {
        slots = new GameObject[40]; //NEED OPTIMIZATION
        for (ushort i = 0; i < 40; i++)
        {
            GameObject slot = Instantiate(slotPrefab);
            slot.name = "InventorySlot (" + i + ")";
            slot.transform.SetParent(invoPanel.transform, false);
            slot.GetComponent<InventorySlot>().slotID = i;
            slots[i] = slot;
            //slots in the PLAYER INVENTORY panel
            slot.GetComponent<InventorySlot>().inventoryReference = playerInventoryReference;
            slot.GetComponent<InventorySlot>().inventoryHandler = this.inventoryHandler;
        }
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            GetComponent<InventoryAndStatsPanelScript>().ToggleInventoryAndStatsPanel();
        }
    }

}
