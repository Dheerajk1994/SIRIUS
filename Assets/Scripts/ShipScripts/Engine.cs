using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : Interactable
{
    private readonly float maxRepairGenerator = 100f, maxRepairCrystals = 100f, maxFuel = 100f;
    private float currentRepairGenerator, currentRepairCrystals, currentFuel;

    public override void Interact()
    {
        base.Interact();
        isInteracting = !isInteracting;
        Debug.Log("Generator: " + currentRepairGenerator.ToString() + "%");
        Debug.Log("Crystals: " + currentRepairCrystals.ToString() + "%");
        Debug.Log("Fuel: " + currentFuel.ToString() + "%");
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
            Interact();
    }

    public void RepairGenerator(float value)
    {
        if (currentRepairGenerator + value >= maxRepairGenerator)
            currentRepairGenerator = maxRepairGenerator;
        else
            currentRepairGenerator += value;
    }

    public void RepairCrystals(float value)
    {
        if (currentRepairCrystals + value >= maxRepairCrystals)
            currentRepairCrystals = maxRepairCrystals;
        else
            currentRepairCrystals += value;
    }

    public void Refuel(float value)
    {
        currentFuel += value;
        Mathf.Clamp(currentFuel, 0, maxFuel);
    }
}
