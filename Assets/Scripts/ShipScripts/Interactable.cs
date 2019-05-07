using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject player;
    public string thisItemsName;
    public bool canInteract = false;
    public bool isInteracting = false;
    public bool panelOpen = false;
    public string interactText = "Press E to Interact";

    public virtual void Interact()
    {
        if (isInteracting)
        {
            QuestManagerScript.instance.InteractedWithItem(thisItemsName);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canInteract = false;
            isInteracting = false;
        }
    }
}