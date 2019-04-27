using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour {

    [SerializeField] private GameObject Teleporter;
    [SerializeField] private GameObject Engine;
    [SerializeField] private GameObject Navigation;
    [SerializeField] private GameObject Bed;
    [SerializeField] private GameObject Chest;

    public void SetShip(UIScript uiscript)
    {
        Teleporter.GetComponent<Teleporter>().ExitShipPanel = uiscript.ExitShipPanel;
        Engine.GetComponent<Engine>().EnginePanel = uiscript.EnginePanel;
        Navigation.GetComponent<Navigation>().NavigationPanel = uiscript.NavPanel;
        Chest.GetComponent<Chest>().ChestPanel = uiscript.ChestPanel;
    }


}
