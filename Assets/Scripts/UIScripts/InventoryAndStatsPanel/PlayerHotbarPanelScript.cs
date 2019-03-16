using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHotbarPanelScript : MonoBehaviour {


    [SerializeField]
    private GameObject slotPrefab;
    public GameObject[] slots;
    [SerializeField]
    private GameObject hotbarPanel;
    public Hotbar playerHotbarReference;
    public InventoryHandlerScript inventoryHandler;


    // Use this for initialization
    void Start()
    {
        slots = new GameObject[10]; //NEED OPTIMIZATION
        for (ushort i = 0; i < slots.Length; i++)
        {
            GameObject slot = Instantiate(slotPrefab);
            slot.name = "invoSlot_" + i;
            slot.transform.SetParent(hotbarPanel.transform, false);
            slot.GetComponent<InventorySlot>().slotID = i;
            slots[i] = slot;
            //slots in the PLAYER INVENTORY panel
            slot.GetComponent<InventorySlot>().inventoryReference = playerHotbarReference;
            slot.GetComponent<InventorySlot>().inventoryHandler = this.inventoryHandler;
        }
    }

    private void Update()
    {

    }
}
