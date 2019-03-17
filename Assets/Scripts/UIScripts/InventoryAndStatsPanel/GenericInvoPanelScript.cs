using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericInvoPanelScript : MonoBehaviour {


    [SerializeField]
    protected GameObject inventorySlotPrefab;
    public GameObject[] slots;
    [SerializeField]
    protected GameObject genericInvoPanel;
    //public Hotbar playerHotbarReference;
    public GenericInvoHandlerScript genericInvoHandler;


    // Use this for initialization

    private void Update()
    {

    }
}
