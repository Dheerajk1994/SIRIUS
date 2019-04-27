using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : Interactable
{
    private readonly float maxFuel = 100f;
    public float currentFuel { get; set; }
    public GameObject EnginePanel;

    public override void Interact()
    {
        base.Interact();
        isInteracting = !isInteracting;
        EnginePanel.GetComponent<Animator>().SetBool("isOpen", isInteracting);
        Debug.Log("Fuel: " + currentFuel.ToString() + "%");
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
            Interact();
        else if (!canInteract)
            EnginePanel.GetComponent<Animator>().SetBool("isOpen", false);
    }

    public void Refuel(float value)
    {
        currentFuel += value;
        Mathf.Clamp(currentFuel, 0, maxFuel);
    }
}
