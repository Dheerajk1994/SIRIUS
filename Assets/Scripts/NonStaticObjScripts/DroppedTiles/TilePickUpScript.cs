using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePickUpScript : MonoBehaviour {

    private InventoryControllerScript inventoryController;

    public ushort contentId = 0;
    public ushort currentStackAmount = 0;


    internal bool isMerging = false;
    internal bool hasMerged = false;

    public void SetTilePickup(InventoryControllerScript ics, ushort cid, ushort camnt, Sprite img)
    {
        this.GetComponentInChildren<SpriteRenderer>().sprite = img;
        inventoryController = ics;
        contentId = cid;
        currentStackAmount = camnt;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TilePickUpScript pickUpScript = collision.gameObject.GetComponent<TilePickUpScript>();
        if (pickUpScript)
        {
            if (pickUpScript.contentId == this.contentId && !hasMerged)
            {
                pickUpScript.hasMerged = true;
                this.currentStackAmount += pickUpScript.currentStackAmount;
                Destroy(pickUpScript.gameObject);
                hasMerged = true;
            }
        }
        else
        {
            PlayerScript player = collision.gameObject.GetComponent<PlayerScript>();
            if (player)
            {
                if((currentStackAmount = inventoryController.PickeupTile(this)) <= 0)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
