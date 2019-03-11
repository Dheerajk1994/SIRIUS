using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool isInteracting = false;

    public virtual void Interact()
    {
        Debug.Log("Interacting");
    }
}
