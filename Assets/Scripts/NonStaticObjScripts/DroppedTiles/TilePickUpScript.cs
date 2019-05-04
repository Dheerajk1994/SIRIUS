using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePickUpScript : MonoBehaviour {

    private InventoryControllerScript inventoryController;
    private AudioSource tilePickupSound;

    public ushort contentId = 0;
    public ushort currentStackAmount = 0;


    internal bool isMerging = false;
    internal bool hasMerged = false;

    public void Start()
    {
        tilePickupSound = this.GetComponent<AudioSource>();
    }

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
            Player player = collision.gameObject.GetComponent<Player>();
            if (player)
            {
                tilePickupSound.Play();
                //Debug.Log("pickup touched by player");
                if ((currentStackAmount = inventoryController.PickeupTile(this)) <= 0)
                {
                    if (!tilePickupSound.isPlaying)
                        Destroy(this.gameObject);
                    else
                    {
                        //this.gameObject.GetComponent<Renderer>().enabled = false;
                        StartCoroutine("Wait1Second");
                        // i had to do this cus theres an error when u delete a gameobject while its playing a sound
                    }
                }
            }
        }
    }

    IEnumerator Wait1Second()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
