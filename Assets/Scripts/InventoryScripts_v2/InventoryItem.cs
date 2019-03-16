using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject itemBeingDragged;
    public GameObject parentSlot;

    public CompleteItem completeItem;
    [SerializeField]
    public ushort stackCount;

    public void UpdateStackCount(ushort amount){
        stackCount = amount;
        //this.transform.gameObject.GetComponent<Text>().text = stackCount.ToString();
        this.transform.gameObject.GetComponentInChildren<Text>().text = stackCount.ToString();
    }

    //BEGINDRAG IMPLEMENTATION
    public void OnBeginDrag(PointerEventData eventData)
    {
        itemBeingDragged = this.gameObject;
        parentSlot = this.gameObject.transform.parent.gameObject;
        this.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        itemBeingDragged.transform.SetParent(parentSlot.transform.parent);
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
        if (transform.parent != parentSlot.transform.parent && transform.parent != parentSlot)
        {
            parentSlot = transform.parent.gameObject;
            transform.SetParent(parentSlot.transform, false);
            transform.position = parentSlot.transform.position;
            Debug.Log("slot found");
        }
        else
        {
            transform.SetParent(parentSlot.transform, false);
            transform.position = parentSlot.transform.position;
            Debug.Log("slot not-found");
        }
    }

}
