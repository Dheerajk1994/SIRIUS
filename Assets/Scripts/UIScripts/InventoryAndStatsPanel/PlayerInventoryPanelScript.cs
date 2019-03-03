using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryPanelScript : MonoBehaviour {

    [SerializeField]
    private GameObject slotPrefab;
    [SerializeField]
    private GameObject invoPanel;
    

	// Use this for initialization
	void Start () {
        for (int i = 0; i < 40; i++)
        {
            GameObject slot = Instantiate(slotPrefab);
            slot.transform.SetParent(invoPanel.transform, false);
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
