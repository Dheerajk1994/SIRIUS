using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class EngineSlot : MonoBehaviour, IDropHandler
{
    public GameObject itemInSlot;

    public bool isHoldingAnItem;


    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem.itemBeingDragged.GetComponent<InventoryItem>().parentSlot = this.transform.gameObject;
        isHoldingAnItem = true;
    }
}
