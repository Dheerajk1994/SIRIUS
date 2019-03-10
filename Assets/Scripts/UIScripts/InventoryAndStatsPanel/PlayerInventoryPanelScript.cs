using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryPanelScript : MonoBehaviour {

    [SerializeField]
    private GameObject slotPrefab;
    public GameObject[] slots;
    [SerializeField]
    private GameObject invoPanel;
    

	// Use this for initialization
	void Start () {
        slots = new GameObject[40]; //NEED OPTIMIZATION
        for (ushort i = 0; i < 40; i++)
        {
            GameObject slot = Instantiate(slotPrefab);
            slot.transform.SetParent(invoPanel.transform, false);
            slot.GetComponent<InventorySlot>().slotID = i;
            slots[i] = slot;
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
