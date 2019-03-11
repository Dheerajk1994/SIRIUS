using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : Interactable {
    private readonly float maxRepair = 100f;
    private readonly float maxFuel = 100f;
    private float currentRepair;
    private float currentFuel;

    public override void Interact()
    {
        base.Interact();
        isInteracting = !isInteracting;
        Debug.Log("Engine: " + currentRepair.ToString() + "%");
        Debug.Log("Fuel: " + currentFuel.ToString() + "%");
    }

    public void Repair(float value)
    {
        if (currentRepair + value >= maxRepair)
            currentRepair = maxRepair;
        else
            currentRepair += value;
    }

    public void Refuel(float value)
    {
        //if (currentFuel + value >= maxFeul)
        //    currentFuel = maxFeul;
        //else
        //    currentFuel += value;
        currentFuel += value;
        Mathf.Clamp(currentFuel, 0, maxFuel);
    }

	
}
