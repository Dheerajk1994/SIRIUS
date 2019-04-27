using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour {

    public GameObject Teleporter;
    public GameObject Engine;

    public void SetShip(UIScript uiscript)
    {
        Teleporter.GetComponent<Teleporter>().ExitShipPanel = uiscript.ExitShipPanel;

        Engine.GetComponent<Engine>().EnginePanel = uiscript.EnginePanel;
    }


}
