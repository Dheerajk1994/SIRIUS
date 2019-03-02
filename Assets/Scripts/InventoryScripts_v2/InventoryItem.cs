using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject itemBeingDragged;
    public GameObject parentSlot;

    //BEGINDRAG IMPLEMENTATION
    public void OnBeginDrag(PointerEventData eventData)
    {
        itemBeingDragged = this.gameObject;
        parentSlot = this.gameObject.transform.parent.gameObject;
        this.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    //DRAG IMPLEMENTATION
    public void OnDrag(PointerEventData eventData)
    {
        itemBeingDragged.transform.position = Input.mousePosition;
    }

    //ENDDRAG IMPLEMENTATION
    public void OnEndDrag(PointerEventData eventData)
    {
        itemBeingDragged = null;
        this.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (transform.parent != parentSlot)
        {
            parentSlot = transform.parent.gameObject;
            transform.position = parentSlot.transform.position;
        }
        else
        {
            transform.position = parentSlot.transform.position;
        }
    }

}
