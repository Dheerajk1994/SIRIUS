using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool canInteract = false;
    public bool isInteracting = false;
    public string interactText = "Press E to Interact";
    Collider2D interactArea;

    public virtual void Interact()
    {
        Debug.Log("Interacting");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E))
            Interact();
    }
}