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

    public ushort stackCount;

    [SerializeField]
    public Image spriteRenderer;
    [SerializeField]
    public Text stackText;


    public void UpdateInventoryItem(CompleteItem item, ushort amount)
    {
        spriteRenderer.sprite = item.icon;
        stackText.text = amount.ToString();
        stackCount = amount;
        completeItem = item;
    }

    public void UpdateInventoryItem(ushort amount)
    {
        stackText.text = amount.ToString();
        stackCount = amount;
    }

    private void UpdateStackCount(ushort amount){

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

        //needs optimization
        this.parentSlot.GetComponent<InventorySlot>().genericInvoHandler.UpdatePanelSlots();
        Destroy(this.gameObject);

    }

}
